﻿namespace ContentDotNet.Extensions.Video.H264.Components.Common.Derivative
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Extensions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Models.Internal;
    using ContentDotNet.Extensions.Video.H264.Models.Internal.AddressesAndBlockIndices;
    using ContentDotNet.Primitives;
    using System;
    using System.Runtime.CompilerServices;
    using static MacroblockTypes;

    internal record struct LumaLocations(int X, int Y);
    internal record struct ChromaLocations(int X, int Y);

    internal enum ABCD
    {
        A,
        B,
        C,
        D
    }

    internal record struct MaxWH
    {
        public int W { get; set; }
        public int H { get; set; }

        public MaxWH(int w, int h)
        {
            W = w;
            H = h;
        }
    }

    internal record struct XnYn(int Xn, int Yn);

    internal record struct XwYw(int Xw, int Yw);

    internal record struct XpYp(int Xp, int Yp);

    internal record struct XsYs(int Xs, int Ys)
    {
        public static XsYs From(XY xy) => new(xy.X, xy.Y);
    }

    /// <summary>
    ///   Allows derivation of locations and macroblocks.
    /// </summary>
    internal static partial class H264Derivative
    {
        private static readonly LumaLocations LocationsA = new(-1, 0);
        private static readonly LumaLocations LocationsB = new(0, -1);
        private static readonly LumaLocations LocationsD = new(-1, -1);

        private static readonly ChromaLocations CLocationsA = new(-1, 0);
        private static readonly ChromaLocations CLocationsB = new(0, -1);
        private static readonly ChromaLocations CLocationsD = new(-1, -1);

        /// <summary>
        ///   Performs the inverse raster scan.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int InverseRasterScan(int a, int b, int c, int d, int e) => e == 0 ? a % (d / b) * b : a / (d / b) * c;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XY InverseMacroblockScan(int mbAddr, H264State state)
        {
            int PicWidthInSamplesL = (int)state.DerivePicWidthInSamplesL();
            if (!state.DeriveMbaffFrameFlag())
            {
                return new(InverseRasterScan(mbAddr, 16, 16, PicWidthInSamplesL, 0), InverseRasterScan(mbAddr, 16, 16, PicWidthInSamplesL, 0));
            }
            else
            {
                XY xyO = new(InverseRasterScan(mbAddr / 2, 16, 32, PicWidthInSamplesL, 0), InverseRasterScan(mbAddr / 2, 16, 32, PicWidthInSamplesL, 1));

                if (state.MacroblockUtility.IsFrame(state.CurrMbAddr))
                {
                    return new(xyO.X, xyO.Y + (mbAddr % 2) * 16);
                }
                else
                {
                    return new(xyO.X, xyO.Y + (mbAddr % 2));
                }
            }
        }

        public static XY InverseMacroblockPartitionScan(H264MacroblockInfo macroblock, int mbPartIdx)
        {
            return new XY(InverseRasterScan(mbPartIdx, MacroblockTraits.MbPartWidth(macroblock), MacroblockTraits.MbPartHeight(macroblock), 16, 0),
                          InverseRasterScan(mbPartIdx, MacroblockTraits.MbPartWidth(macroblock), MacroblockTraits.MbPartHeight(macroblock), 16, 1));
        }

        public static XY InverseSubMacroblockPartitionScan(H264MacroblockInfo macroblock, int mbPartIdx, int subMbPartIdx)
        {
            if (macroblock == P_8x8 || macroblock == P_8x8ref0 || macroblock == B_8x8)
            {
                int x = InverseRasterScan(subMbPartIdx, MacroblockTraits.SubMbPartWidth(macroblock, (int)macroblock.Rbsp.SubMbPred!.SubMbType[mbPartIdx]), MacroblockTraits.SubMbPartHeight(macroblock, (int)macroblock.Rbsp.SubMbPred!.SubMbType[mbPartIdx]), 8, 0);
                int y = InverseRasterScan(subMbPartIdx, MacroblockTraits.SubMbPartWidth(macroblock, (int)macroblock.Rbsp.SubMbPred!.SubMbType[mbPartIdx]), MacroblockTraits.SubMbPartHeight(macroblock, (int)macroblock.Rbsp.SubMbPred!.SubMbType[mbPartIdx]), 8, 1);
                return new XY(x, y);
            }
            else
            {
                int x = InverseRasterScan(subMbPartIdx, 4, 4, 8, 0);
                int y = InverseRasterScan(subMbPartIdx, 4, 4, 8, 1);
                return new XY(x, y);
            }
        }

        public static XY Inverse4x4LumaBlockScan(int luma4x4BlkIdx)
        {
            int x = InverseRasterScan(luma4x4BlkIdx / 4, 8, 8, 16, 0) + InverseRasterScan(luma4x4BlkIdx % 4, 4, 4, 8, 0);
            int y = InverseRasterScan(luma4x4BlkIdx / 4, 8, 8, 16, 1) + InverseRasterScan(luma4x4BlkIdx % 4, 4, 4, 8, 1);
            return new XY(x, y);
        }

        public static XY Inverse4x4CbBlockScan(int luma4x4BlkIdx) => Inverse4x4LumaBlockScan(luma4x4BlkIdx);
        public static XY Inverse4x4CrBlockScan(int luma4x4BlkIdx) => Inverse4x4LumaBlockScan(luma4x4BlkIdx);

        public static XY Inverse8x8LumaBlockScan(int luma8x8BlkIdx)
        {
            int x = InverseRasterScan(luma8x8BlkIdx, 8, 8, 16, 0);
            int y = InverseRasterScan(luma8x8BlkIdx, 8, 8, 16, 1);
            return new XY(x, y);
        }

        public static XY Inverse8x8CbBlockScanCat3(int luma8x8BlkIdx) => Inverse8x8LumaBlockScan(luma8x8BlkIdx);
        public static XY Inverse8x8CrBlockScanCat3(int luma8x8BlkIdx) => Inverse8x8LumaBlockScan(luma8x8BlkIdx);

        public static XY Inverse4x4ChromaBlockScan(int chroma4x4BlkIdx)
        {
            int x = InverseRasterScan(chroma4x4BlkIdx, 4, 4, 8, 0);
            int y = InverseRasterScan(chroma4x4BlkIdx, 4, 4, 8, 1);
            return new XY(x, y);
        }

        public static bool DeriveAvailabilityForMacroblockAddresses(int mbAddr, int CurrMbAddr)
        {
            return !(mbAddr < 0 || mbAddr > CurrMbAddr);
        }

        public static void DeriveNeighboringMacroblocksAndTheirAvailability(
            H264State h264,
            out AddressAndAvailability a,
            out AddressAndAvailability b,
            out AddressAndAvailability c,
            out AddressAndAvailability d)
        {
            int mbAddrA = h264.CurrMbAddr - 1;
            bool aAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrA, h264.CurrMbAddr);
            if (aAvail)
                aAvail = !(h264.CurrMbAddr % h264.DerivePicWidthInMbs() == 0);

            int mbAddrB = h264.CurrMbAddr - (int)h264.DerivePicWidthInMbs();
            bool bAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrB, h264.CurrMbAddr);

            int mbAddrC = h264.CurrMbAddr - (int)h264.DerivePicWidthInMbs() + 1;
            bool cAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrC, h264.CurrMbAddr);
            if (aAvail)
                cAvail = !((h264.CurrMbAddr + 1) % h264.DerivePicWidthInMbs() == 0);

            int mbAddrD = h264.CurrMbAddr - (int)h264.DerivePicWidthInMbs() - 1;
            bool dAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrD, h264.CurrMbAddr);
            if (dAvail)
                dAvail = !(h264.CurrMbAddr % h264.DerivePicWidthInMbs() == 0);

            a = new(mbAddrA, aAvail);
            b = new(mbAddrB, bAvail);
            c = new(mbAddrC, cAvail);
            d = new(mbAddrD, dAvail);
        }

        public static void DeriveNeighboringMacroblocksAndTheirAvailabilityMbaff(
            H264State h264,
            out AddressAndAvailability a,
            out AddressAndAvailability b,
            out AddressAndAvailability c,
            out AddressAndAvailability d)
        {
            int CurrMbAddr = h264.CurrMbAddr;
            int PicWidthInMbs = (int)h264.DerivePicWidthInMbs();

            int mbAddrA = 2 * (CurrMbAddr / 2 - 1);
            bool aAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrA, CurrMbAddr);
            if (aAvail)
                aAvail = !((CurrMbAddr / 2) % PicWidthInMbs == 0);

            int mbAddrB = 2 * (CurrMbAddr / 2 - PicWidthInMbs);
            bool bAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrB, CurrMbAddr);

            int mbAddrC = 2 * (CurrMbAddr / 2 - PicWidthInMbs + 1);
            bool cAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrC, CurrMbAddr);
            if (aAvail)
                cAvail = !((CurrMbAddr / 2 + 1) % PicWidthInMbs == 0);

            int mbAddrD = 2 * (CurrMbAddr / 2 - PicWidthInMbs - 1);
            bool dAvail = DeriveAvailabilityForMacroblockAddresses(mbAddrD, CurrMbAddr);
            if (dAvail)
                dAvail = !((CurrMbAddr / 2) % PicWidthInMbs == 0);

            a = new(mbAddrA, aAvail);
            b = new(mbAddrB, bAvail);
            c = new(mbAddrC, cAvail);
            d = new(mbAddrD, dAvail);
        }

        private static LumaLocations GetBuiltInLumaLocations(ABCD abcd) => abcd switch
        {
            ABCD.A => LocationsA,
            ABCD.B => LocationsB,
            ABCD.D => LocationsD,
            _ => default
        };

        private static ChromaLocations GetBuiltInChromaLocations(ABCD abcd) => abcd switch
        {
            ABCD.A => CLocationsA,
            ABCD.B => CLocationsB,
            ABCD.D => CLocationsD,
            _ => default
        };

        public static void DeriveNeighboringMacroblocks(
            H264State h264,
            out AddressAndAvailability a,
            out AddressAndAvailability b)
        {
            Derive(out a, ABCD.A);
            Derive(out b, ABCD.B);

            void Derive(out AddressAndAvailability n, ABCD abcd)
            {
                LumaLocations ll = GetBuiltInLumaLocations(abcd);
                DeriveNeighboringLocations(h264, true, new XnYn(ll.X, ll.Y), out n, out _);
            }
        }

        public static void DeriveNeighboringLocations(
            H264State h264,
            bool neighboringLumaLocations,
            XnYn xnyn,
            out AddressAndAvailability mb,
            out XwYw xwYw)
        {
            MaxWH wh = default;

            if (neighboringLumaLocations)
            {
                wh.W = 16;
                wh.H = 16;
            }
            else
            {
                wh.W = h264.ChromaSizes.MbWidthC;
                wh.H = h264.ChromaSizes.MbHeightC;
            }

            if (!h264.DeriveMbaffFrameFlag())
            {
                DeriveNeighboringLocationsInFieldsAndNonMbaffFrames(h264, wh, xnyn, out mb, out xwYw);
            }
            else
            {
                DeriveNeighboringLocationsInMbaffFrames(h264, wh, xnyn, out mb, out xwYw);
            }
        }

        public static void DeriveNeighboringLocationsInFieldsAndNonMbaffFrames(
            H264State state,
            MaxWH wh,
            XnYn xnYn,
            out AddressAndAvailability addr,
            out XwYw xwYw)
        {
            DeriveNeighboringMacroblocksAndTheirAvailability(state,
                out AddressAndAvailability a,
                out AddressAndAvailability b,
                out AddressAndAvailability c,
                out AddressAndAvailability d);

            int mbAddrN;
            bool mbAddrNAvailable;

            if (xnYn.Yn > wh.H - 1)
            {
                // TODO: Meaningful exception?
                throw new InvalidOperationException();
            }

            if (xnYn.Xn < 0)
            {
                if (xnYn.Yn < 0)
                {
                    mbAddrN = d.Address;
                    mbAddrNAvailable = d.Availability;
                }
                else
                {
                    mbAddrN = a.Address;
                    mbAddrNAvailable = a.Availability;
                }
            }
            else if (xnYn.Xn >= 0 && xnYn.Xn <= wh.W - 1)
            {
                if (xnYn.Yn < 0)
                {
                    mbAddrN = b.Address;
                    mbAddrNAvailable = b.Availability;
                }
                else
                {
                    mbAddrN = state.CurrMbAddr;
                    mbAddrNAvailable = true;
                }
            }
            else
            {
                if (xnYn.Yn < 0)
                {
                    mbAddrN = c.Address;
                    mbAddrNAvailable = c.Availability;
                }
                else
                {
                    // TODO: Meaningful exception?
                    throw new InvalidOperationException();
                }
            }

            addr = new(mbAddrN, mbAddrNAvailable);

            xwYw = default;

            xwYw.Xw = (xnYn.Xn + wh.W) % wh.W;
            xwYw.Yw = (xnYn.Yn + wh.H) % wh.H;
        }

        public static void DeriveNeighboringLocationsInMbaffFrames(
            H264State state,
            MaxWH wh,
            XnYn xnYn,
            out AddressAndAvailability addr,
            out XwYw xwYw)
        {
            DeriveNeighboringMacroblocksAndTheirAvailabilityMbaff(state,
                out AddressAndAvailability a,
                out AddressAndAvailability b,
                out _,
                out AddressAndAvailability d);

            bool currMbFrameFlag = state.MacroblockUtility.IsFrame(state.CurrMbAddr);
            bool mbIsTopMbFlag = state.CurrMbAddr % 2 == 0;

            int xN = xnYn.Xn;
            int yN = xnYn.Yn;

            int mbAddrX = 0;

            int yM = 0;

            bool mbAddrXFrameFlag = false;
            int mbAddrN = 0;

            int maxW = wh.W;
            int maxH = wh.H;

            if (xN < 0)
            {
                if (yN < 0)
                {
                    if (currMbFrameFlag)
                    {
                        if (mbIsTopMbFlag)
                        {
                            mbAddrX = d.Address;
                            mbAddrN = d.Address + 1;
                            yM = yN;
                        }
                        else
                        {
                            mbAddrX = a.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = a.Address;
                                yM = yN;
                            }
                            else
                            {
                                mbAddrN = a.Address + 1;
                                yM = yN + maxH >> 1;
                            }
                        }
                    }
                    else
                    {
                        if (mbIsTopMbFlag)
                        {
                            mbAddrX = d.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = d.Address + 1;
                                yM = 2 * yN;
                            }
                            else
                            {
                                mbAddrN = d.Address;
                                yM = yN;
                            }
                        }
                        else
                        {
                            mbAddrX = d.Address;
                            mbAddrN = d.Address;
                            yM = yN;
                        }
                    }
                }
                else if (yN >= 0 && yN <= maxH - 1)
                {
                    if (currMbFrameFlag)
                    {
                        if (mbIsTopMbFlag)
                        {
                            mbAddrX = a.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = a.Address;
                                yM = yN;
                            }
                            else
                            {
                                if (yN % 2 == 0)
                                {
                                    mbAddrN = a.Address;
                                    yM = yN >> 1;
                                }
                                else
                                {
                                    mbAddrN = a.Address + 1;
                                    yM = yN >> 1;
                                }
                            }
                        }
                        else
                        {
                            mbAddrX = a.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = a.Address + 1;
                                yM = yN;
                            }
                            else
                            {
                                if (yN % 2 == 0)
                                {
                                    mbAddrN = a.Address;
                                    yM = yN >> 1;
                                }
                                else
                                {
                                    mbAddrN = a.Address + 1;
                                    yM = yN >> 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (mbIsTopMbFlag)
                        {
                            mbAddrX = a.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = a.Address;
                                yM = yN;
                            }
                            else
                            {
                                if (yN < maxH / 2)
                                {
                                    mbAddrN = a.Address;
                                    yM = yN << 1;
                                }
                                else
                                {
                                    mbAddrN = a.Address + 1;
                                    yM = (yN << 1) - maxH;
                                }
                            }
                        }
                        else
                        {
                            mbAddrX = a.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = a.Address + 1;
                                yM = yN;
                            }
                            else
                            {
                                if (yN < maxH / 2)
                                {
                                    mbAddrN = a.Address;
                                    yM = (yN << 1) + 1;
                                }
                                else
                                {
                                    mbAddrN = a.Address + 1;
                                    yM = (yN << 1) + 1 - maxH;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (currMbFrameFlag)
                    {
                        if (mbIsTopMbFlag)
                        {
                            mbAddrX = b.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = b.Address;
                                yM = yN - maxH;
                            }
                            else
                            {
                                if ((yN - maxH) % 2 == 0)
                                {
                                    mbAddrN = b.Address;
                                    yM = yN - maxH >> 1;
                                }
                                else
                                {
                                    mbAddrN = b.Address + 1;
                                    yM = yN - maxH >> 1;
                                }
                            }
                        }
                        else
                        {
                            mbAddrX = b.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = b.Address + 1;
                                yM = yN - maxH;
                            }
                            else
                            {
                                if ((yN - maxH) % 2 == 0)
                                {
                                    mbAddrN = b.Address;
                                    yM = yN - maxH >> 1;
                                }
                                else
                                {
                                    mbAddrN = b.Address + 1;
                                    yM = yN - maxH >> 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (mbIsTopMbFlag)
                        {
                            mbAddrX = b.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = b.Address + 1;
                                yM = 2 * (yN - maxH);
                            }
                            else
                            {
                                mbAddrN = b.Address;
                                yM = yN - maxH;
                            }
                        }
                        else
                        {
                            mbAddrX = b.Address;
                            if (mbAddrXFrameFlag)
                            {
                                mbAddrN = b.Address + 1;
                                yM = yN - maxH;
                            }
                            else
                            {
                                if (yN - maxH < maxH / 2)
                                {
                                    mbAddrN = b.Address;
                                    yM = (yN - maxH << 1) + 1;
                                }
                                else
                                {
                                    mbAddrN = b.Address + 1;
                                    yM = (yN - maxH << 1) + 1 - maxH;
                                }
                            }
                        }
                    }
                }
            }

            addr = default;
            addr.Availability = state.MacroblockUtility.IsMacroblock(mbAddrX);
            addr.Address = mbAddrN;

            xwYw = default;
            xwYw.Xw = (xN + maxW) % maxW;
            xwYw.Yw = (yM + maxH) % maxH;
        }

        public static void DeriveNeighboring8x8ChromaBlocksCat3(
            H264State h264,
            int chroma8x8BlkIdx,
            out AddressAnd8x8ChromaBlockIndex addrA,
            out AddressAnd8x8ChromaBlockIndex addrB)
        {
            DeriveNeighboring8x8LumaBlock(h264, chroma8x8BlkIdx, out var addrAL, out var addrBL);
            addrA = new(addrAL.AddressAndAvailability, addrAL.Luma8x8BlkIdx.AsChroma());
            addrB = new(addrBL.AddressAndAvailability, addrBL.Luma8x8BlkIdx.AsChroma());
        }

        public static void DeriveNeighboring8x8LumaBlock(
            H264State h264,
            int luma8x8BlkIdx,
            out AddressAnd8x8LumaBlockIndex addrA,
            out AddressAnd8x8LumaBlockIndex addrB)
        {
            Derive(LocationsA, out addrA);
            Derive(LocationsB, out addrB);

            void Derive(LumaLocations ll, out AddressAnd8x8LumaBlockIndex addr)
            {
                int xN = (luma8x8BlkIdx % 2) * 8 + ll.X;
                int yN = (luma8x8BlkIdx / 2) * 8 + ll.Y;
                DeriveNeighboringLocations(h264, true, new(xN, yN), out var addrX, out XwYw xwyw);

                if (!addrX.Availability)
                {
                    addr = new(addrX, new LumaBlockIndex(0, false));
                }
                else
                {
                    int blkIdxN = Derive8x8LumaBlockIndices(new(xwyw.Xw, xwyw.Yw));
                    addr = new(addrX, new LumaBlockIndex(blkIdxN, true));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Derive4x4LumaBlockIndices(XpYp peepPeep /*🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤*/)
        {
            // Sorry for the emoji spam guys I'm just really obsessed with baby chicks. 🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤🐤
            int xP = peepPeep.Xp;
            int yP = peepPeep.Yp;
            return 8 * (yP / 8) + 4 * (xP / 8) + 2 * ((yP % 8) / 4) + ((xP % 8) / 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Derive4x4ChromaBlockIndices(XpYp xpYp)
        {
            int xP = xpYp.Xp;
            int yP = xpYp.Yp;
            return 2 * (yP / 4) + (xP / 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Derive8x8LumaBlockIndices(XpYp xpYp)
        {
            int xP = xpYp.Xp;
            int yP = xpYp.Yp;
            return 2 * (yP / 8) + (xP / 8);
        }

        public static PartitionIndices DeriveMacroblockAndSubMacroblockPartitionIndices(H264MacroblockInfo mb, int mbType, XpYp xpYp, ReadOnlySpan<int> subMbType)
        {
            H264MacroblockInfo newMb = mb with
            {
                Rbsp = mb.Rbsp with
                {
                    MbType = (uint)mbType
                }
            };

            int xP = xpYp.Xp;
            int yP = xpYp.Yp;

            int mbPartIdx = 0;
            int subMbPartIdx = 0;

            if (mb.SliceType != H264SliceType.I)
            {
                mbPartIdx = (16 / MacroblockTraits.MbPartWidth(newMb)) * (yP / MacroblockTraits.MbPartHeight(newMb)) +
                            (xP / MacroblockTraits.MbPartWidth(newMb));
            }

            if (newMb != P_8x8 &&
                newMb != P_8x8ref0 &&
                newMb != B_8x8 &&
                newMb != B_Skip &&
                newMb != B_Direct_16x16)
            {
            }
            else if (newMb == B_Skip || newMb == B_Direct_16x16)
            {
                subMbPartIdx = 2 * ((yP % 8) / 4) + ((xP % 8) / 4);
            }
            else
            {
                subMbPartIdx = (8 / MacroblockTraits.SubMbPartWidth(newMb, subMbType[mbPartIdx])) *
                               ((yP % 8) / MacroblockTraits.SubMbPartHeight(newMb, subMbType[mbPartIdx])) +
                               ((xP % 8) / MacroblockTraits.SubMbPartWidth(newMb, subMbType[mbPartIdx]));
            }

            return new(mbPartIdx, subMbPartIdx);
        }

        public static void DeriveNeighboringPartitions(
            H264MacroblockInfo mb,
            H264State h264,
            int mbPartIdx,
            int currSubMbType,
            int subMbPartIdx,
            out AddressAndPartitionIndices a,
            out AddressAndPartitionIndices b,
            out AddressAndPartitionIndices c,
            out AddressAndPartitionIndices d)
        {
            Derive(ABCD.A, out a);
            Derive(ABCD.B, out b);
            Derive(ABCD.C, out c);
            Derive(ABCD.D, out d);

            void Derive(ABCD abcd, out AddressAndPartitionIndices addr)
            {
                XY xy = InverseMacroblockPartitionScan(mb, mbPartIdx);
                XsYs xsys = default;
                if (mb == P_8x8 || mb == P_8x8ref0 || mb == B_8x8)
                {
                    xsys = XsYs.From(InverseSubMacroblockPartitionScan(mb, mbPartIdx, subMbPartIdx));
                }

                int predPartWidth;
                if (mb == P_Skip || mb == B_Skip || mb == B_Direct_16x16)
                {
                    predPartWidth = 16;
                }
                else if (mb == B_8x8)
                {
                    if (currSubMbType == B_Direct_8x8.MacroblockTypeNumber)
                    {
                        predPartWidth = 16;
                    }
                    else
                    {
                        predPartWidth = MacroblockTraits.SubMbPartWidth(mb, (int)mb.Rbsp.SubMbPred!.SubMbType[mbPartIdx]);
                    }
                }
                else
                {
                    predPartWidth = MacroblockTraits.SubMbPartWidth(mb, (int)mb.Rbsp.SubMbPred!.SubMbType[mbPartIdx]);
                }

                LumaLocations ll = abcd switch
                {
                    ABCD.A => LocationsA,
                    ABCD.B => LocationsB,
                    ABCD.D => LocationsD,
                    ABCD.C => new(predPartWidth, -1),
                    _ => throw new InvalidOperationException()
                };

                XnYn xnyn = new(xy.X + xsys.Xs + ll.X, xy.Y + xsys.Ys + ll.Y);

                DeriveNeighboringLocations(h264, true, xnyn, out var addrX, out XwYw xwyw);

                if (!addrX.Availability)
                {
                    addr = new(new AddressAndAvailability(0, false), new PartitionIndices(0, 0));
                }
                else
                {
                    H264MacroblockInfo mb = h264.MacroblockUtility.GetMacroblock(addrX.Address);
                    int mbTypeN = (int)mb.Rbsp.MbType;
                    int[] subMbTypeN = [];
                    if (mb == P_8x8 || mb == P_8x8ref0 || mb == B_8x8)
                        subMbTypeN = mb.Rbsp.SubMbPred!.SubMbType.AsInt32Array();

                    PartitionIndices indices = DeriveMacroblockAndSubMacroblockPartitionIndices(mb, mbTypeN, new(xwyw.Xw, xwyw.Yw), subMbTypeN);

                    addr = new(addrX, indices);
                }
            }
        }

        public static void DeriveNeighboring4x4LumaBlocks(
            H264State h264,
            int luma4x4BlkIdx,
            out AddressAnd4x4LumaBlockIndex addrA,
            out AddressAnd4x4LumaBlockIndex addrB)
        {
            addrA = Derive(ABCD.A);
            addrB = Derive(ABCD.B);

            AddressAnd4x4LumaBlockIndex Derive(ABCD abcd)
            {
                LumaLocations ll = GetBuiltInLumaLocations(abcd);

                XY xy = Inverse4x4LumaBlockScan(luma4x4BlkIdx);

                XnYn xnyn = new(xy.X + ll.X, xy.Y + ll.Y);

                DeriveNeighboringLocations(h264, true, xnyn, out var addrX, out XwYw xwyw);

                if (!addrX.Availability)
                {
                    return new AddressAnd4x4LumaBlockIndex(default, new LumaBlockIndex(0, false));
                }
                else
                {
                    int idx = Derive4x4LumaBlockIndices(new XpYp(xwyw.Xw, xwyw.Yw));
                    return new AddressAnd4x4LumaBlockIndex(addrX, new LumaBlockIndex(idx, true));
                }
            }
        }

        public static void DeriveNeighboring4x4ChromaBlocks(
            H264State h264,
            int chroma4x4BlkIdx,
            out AddressAnd4x4ChromaBlockIndex addrA,
            out AddressAnd4x4ChromaBlockIndex addrB)
        {
            addrA = Derive(ABCD.A);
            addrB = Derive(ABCD.B);

            AddressAnd4x4ChromaBlockIndex Derive(ABCD abcd)
            {
                ChromaLocations cl = GetBuiltInChromaLocations(abcd);

                XY xy = Inverse4x4ChromaBlockScan(chroma4x4BlkIdx);

                XnYn xnyn = new(xy.X + cl.X, xy.Y + cl.Y);

                DeriveNeighboringLocations(h264, true, xnyn, out var addrX, out XwYw xwyw);

                if (!addrX.Availability)
                {
                    return new AddressAnd4x4ChromaBlockIndex(default, new ChromaBlockIndex(0, false));
                }
                else
                {
                    int idx = Derive4x4ChromaBlockIndices(new XpYp(xwyw.Xw, xwyw.Yw));
                    return new AddressAnd4x4ChromaBlockIndex(addrX, new ChromaBlockIndex(idx, true));
                }
            }
        }
    }
}
