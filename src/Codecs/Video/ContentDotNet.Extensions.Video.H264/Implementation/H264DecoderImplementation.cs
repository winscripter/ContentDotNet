namespace ContentDotNet.Extensions.Video.H264.Implementation
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Rbsp;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using ContentDotNet.Pictures;
    using System.Threading.Tasks;

    internal class H264DecoderImplementation : AbstractH264Decoder
    {
        private const int StartCodeFindingRecursionLimit = 4 * 1024 * 1024;

        public H264DecoderImplementation(BitStreamReader bsr) : base(bsr)
        {
            this.IOReader = new DefaultRbspReader();
            this.State = new H264State(null!) // MacroblockUtility will be set in the H264State constructor
            {
                H264RbspState = new H264RbspState()
            };
        }

        public override NalType DecodeNal(bool skipToNalStart = true)
        {
            if (skipToNalStart)
            {
                if (!SkipToNalStart())
                    return NalType.DidNotRead;
            }

            long nalLength = ProcessNalLength();
            this.State!.H264RbspState!.RbspEndOffset = this.BitStreamReader.BaseStream.Position + nalLength;

            RbspNalUnit nal = ParseNal((int)nalLength);
            using var bitstreamReader = new BitStreamReader(nal.RbspByte);

            NalType nalType = GetNalType((int)nal.NalUnitType);

            if (nalType == NalType.Sps)
            {
                RbspSequenceParameterSetData spsData = this.IOReader!.ReadSPSData(this.State!.H264RbspState!, bitstreamReader);
                this.State!.H264RbspState!.SequenceParameterSetData = spsData;
            }
            else if (nalType == NalType.Pps)
            {
                RbspPictureParameterSet ppsData = this.IOReader!.ReadPPSData(this.State!.H264RbspState!, bitstreamReader);
                this.State!.H264RbspState!.PictureParameterSet = ppsData;
            }
            else if (nalType == NalType.Aud)
            {
                RbspAccessUnitDelimiter aud = ReadAud(bitstreamReader);
                this.State!.H264RbspState!.AccessUnitDelimiter = aud;
            }
            else if (nalType == NalType.Idr || nalType == NalType.NonIdr)
            {
                RbspSliceHeader sliceHeader = this.IOReader!.ReadSliceHeader(this.State!.H264RbspState!, bitstreamReader);
                var slice = new H264Slice(bitstreamReader, State)
                {
                    IOReader = this.IOReader!,
                    SyntaxReaderFactory = DefaultSyntaxReaderFactory.Instance
                };
                slice.LoadSlice(DecoderFactories.SliceDecoderFactory.CreateSliceDecoder());
                this.Slice = slice;
                this.State!.H264RbspState!.SliceHeader = sliceHeader;
            }

            return nalType;
        }

        private static NalType GetNalType(int nt)
        {
            return nt switch
            {
                1 => NalType.NonIdr,
                5 => NalType.Idr,
                7 => NalType.Sps,
                8 => NalType.Pps,
                9 => NalType.Aud,
                _ => NalType.Unknown,
            };
        }

        public override Task<NalType> DecodeNalAsync(bool skipToNalStart = true)
        {
            throw new NotImplementedException();
        }

        public override long ProcessNalLength()
        {
            ReaderState originalState = this.BitStreamReader.GetState();
            if (!SkipToNalStart())
            {
                this.BitStreamReader.GoTo(originalState);
                return this.BitStreamReader.BaseStream.Length - this.BitStreamReader.BaseStream.Position;
            }

            this.BitStreamReader.Backtrack(3);

            ReaderState activeState = this.BitStreamReader.GetState();
            long result = activeState.ByteOffset - originalState.ByteOffset;

            this.BitStreamReader.GoTo(originalState);

            return result;
        }

        public override Picture<YCbCr> ReadPicture()
        {
            throw new NotImplementedException();
        }

        public override Task<Picture<YCbCr>> ReadPictureAsync()
        {
            throw new NotImplementedException();
        }

        public override bool SkipToNalStart()
        {
            RecursionCounter recursionCounter = new(StartCodeFindingRecursionLimit);
            int stream = 0;
            while (true)
            {
                try
                {
                    recursionCounter.Increment();

                    byte current = (byte)this.BitStreamReader.ReadByte();
                    if (stream != 2 && current == 0)
                    {
                        stream++;
                        continue;
                    }
                    else if (stream == 2 && current == 1)
                    {
                        return true;
                    }
                    else if (current != 0)
                    {
                        stream = 0;
                        continue;
                    }
                    // else { }
                }
                catch (EndOfStreamException)
                {
                    return false;
                }
                catch (InfiniteLoopException)
                {
                    throw;
                }
            }
        }

        private RbspNalUnit ParseNal(int nBytes)
        {
            bool forbidden_zero_bit = this.BitStreamReader.ReadBit();
            uint nal_ref_idc = this.BitStreamReader.ReadBits(2);
            uint nal_unit_type = this.BitStreamReader.ReadBits(5);

            int nalUnitHeaderBytes = -1;

            SvcRbspNalUnitHeaderSvcExtension? svcExt = null;
            MvcNalUnitHeaderMvcExtension? mvcExt = null;
            Avc3DNalUnitHeader3DAvcExtension? avc3D = null;

            var ituBuilder = this.RbspBuilderFactory!.CreateBuilder();

            bool svc_extension_flag = false;
            bool avc_3d_extension_flag = false;

            if (nal_unit_type is 14 or 20 or 21)
            {
                if (nal_unit_type != 21)
                {
                    svc_extension_flag = this.BitStreamReader.ReadBit();
                }
                else
                {
                    avc_3d_extension_flag = this.BitStreamReader.ReadBit();
                }

                if (svc_extension_flag)
                {
                    svcExt = ParseSvcExtension();
                    nalUnitHeaderBytes += 3;
                }
                else if (avc_3d_extension_flag)
                {
                    avc3D = ParseAvc3DExtension();
                    nalUnitHeaderBytes += 2;
                }
                else
                {
                    mvcExt = ParseMvcExtension();
                    nalUnitHeaderBytes += 3;
                }
            }

            for (int i = nalUnitHeaderBytes; i < nBytes; i++)
            {
                if (i + 2 < nBytes && this.BitStreamReader.PeekBits(24) == 0x000003)
                {
                    ituBuilder.FeedByte((byte)this.BitStreamReader.ReadBits(8));
                    ituBuilder.FeedByte((byte)this.BitStreamReader.ReadBits(8));
                    i += 2;
                    _ = this.BitStreamReader.ReadBits(8); // emulation_prevention_three_byte
                }
                else
                {
                    ituBuilder.FeedByte((byte)this.BitStreamReader.ReadBits(8));
                }
            }

            return new RbspNalUnit(forbidden_zero_bit, nal_ref_idc, nal_unit_type, svc_extension_flag, avc_3d_extension_flag, svcExt, avc3D, mvcExt, ituBuilder.CreateStream());
        }

        private SvcRbspNalUnitHeaderSvcExtension ParseSvcExtension()
        {
            return new SvcRbspNalUnitHeaderSvcExtension(
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBits(6),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBits(3),
                this.BitStreamReader.ReadBits(4),
                this.BitStreamReader.ReadBits(3),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBits(2));
        }

        private MvcNalUnitHeaderMvcExtension ParseMvcExtension()
        {
            return new MvcNalUnitHeaderMvcExtension(
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBits(6),
                this.BitStreamReader.ReadBits(10),
                this.BitStreamReader.ReadBits(3),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBit());
        }

        private Avc3DNalUnitHeader3DAvcExtension ParseAvc3DExtension()
        {
            return new Avc3DNalUnitHeader3DAvcExtension(
                this.BitStreamReader.ReadBits(8),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBits(3),
                this.BitStreamReader.ReadBit(),
                this.BitStreamReader.ReadBit());
        }

        private RbspAccessUnitDelimiter ReadAud(BitStreamReader bsi)
        {
            uint primary_pic_type = bsi.ReadBits(3);
            while (bsi.GetState().BitPosition != 0)
                _ = this.BitStreamReader.ReadBit();
            return new(primary_pic_type);
        }
    }
}
