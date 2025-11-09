#nullable enable

using DWORD = int;
using LONG = int;
using WORD = ushort;

namespace ContentDotNet.Image.Formats.Bmp
{
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct BitmapCoreHeader : IEquatable<BitmapCoreHeader?>, IBitmapHeader
    {
        /// <summary>
        ///   The field BcWidth
        /// </summary>
        public WORD BcWidth;

        /// <summary>
        ///   The field BcHeight
        /// </summary>
        public WORD BcHeight;

        /// <summary>
        ///   The field BcPlanes
        /// </summary>
        public WORD BcPlanes;

        /// <summary>
        ///   The field BcBitCount
        /// </summary>
        public WORD BcBitCount;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BitmapCoreHeader" /> struct.
        /// </summary>
        /// <param name="BcWidth">The parameter</param>
        /// <param name="BcHeight">The parameter</param>
        /// <param name="BcPlanes">The parameter</param>
        /// <param name="BcBitCount">The parameter</param>
        public BitmapCoreHeader(WORD BcWidth, WORD BcHeight, WORD BcPlanes, WORD BcBitCount)
        {
            this.BcWidth = BcWidth;
            this.BcHeight = BcHeight;
            this.BcPlanes = BcPlanes;
            this.BcBitCount = BcBitCount;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapCoreHeader" />.
        /// </returns>
        public static BitmapCoreHeader Read(BinaryReader reader)
        {
            var BcWidth = CommonBitmapIO.ReadWORD(reader);
            var BcHeight = CommonBitmapIO.ReadWORD(reader);
            var BcPlanes = CommonBitmapIO.ReadWORD(reader);
            var BcBitCount = CommonBitmapIO.ReadWORD(reader);
            return new BitmapCoreHeader(BcWidth, BcHeight, BcPlanes, BcBitCount);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapCoreHeader" />.
        /// </returns>
        public static async Task<BitmapCoreHeader> ReadAsync(BinaryReader reader)
        {
            var BcWidth = await CommonBitmapIO.ReadWORDAsync(reader);
            var BcHeight = await CommonBitmapIO.ReadWORDAsync(reader);
            var BcPlanes = await CommonBitmapIO.ReadWORDAsync(reader);
            var BcBitCount = await CommonBitmapIO.ReadWORDAsync(reader);
            return new BitmapCoreHeader(BcWidth, BcHeight, BcPlanes, BcBitCount);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteWORD(writer, this.BcWidth);
            CommonBitmapIO.WriteWORD(writer, this.BcHeight);
            CommonBitmapIO.WriteWORD(writer, this.BcPlanes);
            CommonBitmapIO.WriteWORD(writer, this.BcBitCount);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteWORDAsync(writer, this.BcWidth);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BcHeight);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BcPlanes);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BcBitCount);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(BitmapCoreHeader? other)
        {
            return other is not null
                && EqualityComparer<WORD>.Default.Equals(other.Value.BcWidth, this.BcWidth)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BcHeight, this.BcHeight)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BcPlanes, this.BcPlanes)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BcBitCount, this.BcBitCount)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is BitmapCoreHeader value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + BcWidth.GetHashCode();
                hash = hash * 23 + BcHeight.GetHashCode();
                hash = hash * 23 + BcPlanes.GetHashCode();
                hash = hash * 23 + BcBitCount.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("BcWidth: " + BcWidth);
            sb.AppendLine("BcHeight: " + BcHeight);
            sb.AppendLine("BcPlanes: " + BcPlanes);
            sb.AppendLine("BcBitCount: " + BcBitCount);
            return sb.ToString();
        }
    }
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct BitmapFileHeader : IEquatable<BitmapFileHeader?>
    {
        /// <summary>
        ///   The field BfType
        /// </summary>
        public WORD BfType;

        /// <summary>
        ///   The field BfSize
        /// </summary>
        public DWORD BfSize;

        /// <summary>
        ///   The field BfReserved1
        /// </summary>
        public WORD BfReserved1;

        /// <summary>
        ///   The field BfReserved2
        /// </summary>
        public WORD BfReserved2;

        /// <summary>
        ///   The field BfOffBits
        /// </summary>
        public DWORD BfOffBits;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BitmapFileHeader" /> struct.
        /// </summary>
        /// <param name="BfType">The parameter</param>
        /// <param name="BfSize">The parameter</param>
        /// <param name="BfReserved1">The parameter</param>
        /// <param name="BfReserved2">The parameter</param>
        /// <param name="BfOffBits">The parameter</param>
        public BitmapFileHeader(WORD BfType, DWORD BfSize, WORD BfReserved1, WORD BfReserved2, DWORD BfOffBits)
        {
            this.BfType = BfType;
            this.BfSize = BfSize;
            this.BfReserved1 = BfReserved1;
            this.BfReserved2 = BfReserved2;
            this.BfOffBits = BfOffBits;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapFileHeader" />.
        /// </returns>
        public static BitmapFileHeader Read(BinaryReader reader)
        {
            var BfType = CommonBitmapIO.ReadWORD(reader);
            var BfSize = CommonBitmapIO.ReadDWORD(reader);
            var BfReserved1 = CommonBitmapIO.ReadWORD(reader);
            var BfReserved2 = CommonBitmapIO.ReadWORD(reader);
            var BfOffBits = CommonBitmapIO.ReadDWORD(reader);
            return new BitmapFileHeader(BfType, BfSize, BfReserved1, BfReserved2, BfOffBits);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapFileHeader" />.
        /// </returns>
        public static async Task<BitmapFileHeader> ReadAsync(BinaryReader reader)
        {
            var BfType = await CommonBitmapIO.ReadWORDAsync(reader);
            var BfSize = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BfReserved1 = await CommonBitmapIO.ReadWORDAsync(reader);
            var BfReserved2 = await CommonBitmapIO.ReadWORDAsync(reader);
            var BfOffBits = await CommonBitmapIO.ReadDWORDAsync(reader);
            return new BitmapFileHeader(BfType, BfSize, BfReserved1, BfReserved2, BfOffBits);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteWORD(writer, this.BfType);
            CommonBitmapIO.WriteDWORD(writer, this.BfSize);
            CommonBitmapIO.WriteWORD(writer, this.BfReserved1);
            CommonBitmapIO.WriteWORD(writer, this.BfReserved2);
            CommonBitmapIO.WriteDWORD(writer, this.BfOffBits);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteWORDAsync(writer, this.BfType);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BfSize);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BfReserved1);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BfReserved2);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BfOffBits);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(BitmapFileHeader? other)
        {
            return other is not null
                && EqualityComparer<WORD>.Default.Equals(other.Value.BfType, this.BfType)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BfSize, this.BfSize)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BfReserved1, this.BfReserved1)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BfReserved2, this.BfReserved2)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BfOffBits, this.BfOffBits)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is BitmapFileHeader value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + BfType.GetHashCode();
                hash = hash * 23 + BfSize.GetHashCode();
                hash = hash * 23 + BfReserved1.GetHashCode();
                hash = hash * 23 + BfReserved2.GetHashCode();
                hash = hash * 23 + BfOffBits.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("BfType: " + BfType);
            sb.AppendLine("BfSize: " + BfSize);
            sb.AppendLine("BfReserved1: " + BfReserved1);
            sb.AppendLine("BfReserved2: " + BfReserved2);
            sb.AppendLine("BfOffBits: " + BfOffBits);
            return sb.ToString();
        }
    }
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct BitmapInfoHeader : IEquatable<BitmapInfoHeader?>, IBitmapHeader
    {
        /// <summary>
        ///   The field BiWidth
        /// </summary>
        public LONG BiWidth;

        /// <summary>
        ///   The field BiHeight
        /// </summary>
        public LONG BiHeight;

        /// <summary>
        ///   The field BiPlanes
        /// </summary>
        public WORD BiPlanes;

        /// <summary>
        ///   The field BiBitCount
        /// </summary>
        public WORD BiBitCount;

        /// <summary>
        ///   The field BiCompression
        /// </summary>
        public DWORD BiCompression;

        /// <summary>
        ///   The field BiSizeImage
        /// </summary>
        public DWORD BiSizeImage;

        /// <summary>
        ///   The field BiXPelsPerMeter
        /// </summary>
        public LONG BiXPelsPerMeter;

        /// <summary>
        ///   The field BiYPelsPerMeter
        /// </summary>
        public LONG BiYPelsPerMeter;

        /// <summary>
        ///   The field BiClrUsed
        /// </summary>
        public DWORD BiClrUsed;

        /// <summary>
        ///   The field BiClrImportant
        /// </summary>
        public DWORD BiClrImportant;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BitmapInfoHeader" /> struct.
        /// </summary>
        /// <param name="BiWidth">The parameter</param>
        /// <param name="BiHeight">The parameter</param>
        /// <param name="BiPlanes">The parameter</param>
        /// <param name="BiBitCount">The parameter</param>
        /// <param name="BiCompression">The parameter</param>
        /// <param name="BiSizeImage">The parameter</param>
        /// <param name="BiXPelsPerMeter">The parameter</param>
        /// <param name="BiYPelsPerMeter">The parameter</param>
        /// <param name="BiClrUsed">The parameter</param>
        /// <param name="BiClrImportant">The parameter</param>
        public BitmapInfoHeader(LONG BiWidth, LONG BiHeight, WORD BiPlanes, WORD BiBitCount, DWORD BiCompression, DWORD BiSizeImage, LONG BiXPelsPerMeter, LONG BiYPelsPerMeter, DWORD BiClrUsed, DWORD BiClrImportant)
        {
            this.BiWidth = BiWidth;
            this.BiHeight = BiHeight;
            this.BiPlanes = BiPlanes;
            this.BiBitCount = BiBitCount;
            this.BiCompression = BiCompression;
            this.BiSizeImage = BiSizeImage;
            this.BiXPelsPerMeter = BiXPelsPerMeter;
            this.BiYPelsPerMeter = BiYPelsPerMeter;
            this.BiClrUsed = BiClrUsed;
            this.BiClrImportant = BiClrImportant;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapInfoHeader" />.
        /// </returns>
        public static BitmapInfoHeader Read(BinaryReader reader)
        {
            var BiWidth = CommonBitmapIO.ReadLONG(reader);
            var BiHeight = CommonBitmapIO.ReadLONG(reader);
            var BiPlanes = CommonBitmapIO.ReadWORD(reader);
            var BiBitCount = CommonBitmapIO.ReadWORD(reader);
            var BiCompression = CommonBitmapIO.ReadDWORD(reader);
            var BiSizeImage = CommonBitmapIO.ReadDWORD(reader);
            var BiXPelsPerMeter = CommonBitmapIO.ReadLONG(reader);
            var BiYPelsPerMeter = CommonBitmapIO.ReadLONG(reader);
            var BiClrUsed = CommonBitmapIO.ReadDWORD(reader);
            var BiClrImportant = CommonBitmapIO.ReadDWORD(reader);
            return new BitmapInfoHeader(BiWidth, BiHeight, BiPlanes, BiBitCount, BiCompression, BiSizeImage, BiXPelsPerMeter, BiYPelsPerMeter, BiClrUsed, BiClrImportant);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapInfoHeader" />.
        /// </returns>
        public static async Task<BitmapInfoHeader> ReadAsync(BinaryReader reader)
        {
            var BiWidth = await CommonBitmapIO.ReadLONGAsync(reader);
            var BiHeight = await CommonBitmapIO.ReadLONGAsync(reader);
            var BiPlanes = await CommonBitmapIO.ReadWORDAsync(reader);
            var BiBitCount = await CommonBitmapIO.ReadWORDAsync(reader);
            var BiCompression = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BiSizeImage = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BiXPelsPerMeter = await CommonBitmapIO.ReadLONGAsync(reader);
            var BiYPelsPerMeter = await CommonBitmapIO.ReadLONGAsync(reader);
            var BiClrUsed = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BiClrImportant = await CommonBitmapIO.ReadDWORDAsync(reader);
            return new BitmapInfoHeader(BiWidth, BiHeight, BiPlanes, BiBitCount, BiCompression, BiSizeImage, BiXPelsPerMeter, BiYPelsPerMeter, BiClrUsed, BiClrImportant);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteLONG(writer, this.BiWidth);
            CommonBitmapIO.WriteLONG(writer, this.BiHeight);
            CommonBitmapIO.WriteWORD(writer, this.BiPlanes);
            CommonBitmapIO.WriteWORD(writer, this.BiBitCount);
            CommonBitmapIO.WriteDWORD(writer, this.BiCompression);
            CommonBitmapIO.WriteDWORD(writer, this.BiSizeImage);
            CommonBitmapIO.WriteLONG(writer, this.BiXPelsPerMeter);
            CommonBitmapIO.WriteLONG(writer, this.BiYPelsPerMeter);
            CommonBitmapIO.WriteDWORD(writer, this.BiClrUsed);
            CommonBitmapIO.WriteDWORD(writer, this.BiClrImportant);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteLONGAsync(writer, this.BiWidth);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BiHeight);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BiPlanes);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BiBitCount);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BiCompression);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BiSizeImage);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BiXPelsPerMeter);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BiYPelsPerMeter);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BiClrUsed);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BiClrImportant);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(BitmapInfoHeader? other)
        {
            return other is not null
                && EqualityComparer<LONG>.Default.Equals(other.Value.BiWidth, this.BiWidth)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BiHeight, this.BiHeight)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BiPlanes, this.BiPlanes)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BiBitCount, this.BiBitCount)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BiCompression, this.BiCompression)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BiSizeImage, this.BiSizeImage)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BiXPelsPerMeter, this.BiXPelsPerMeter)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BiYPelsPerMeter, this.BiYPelsPerMeter)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BiClrUsed, this.BiClrUsed)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BiClrImportant, this.BiClrImportant)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is BitmapInfoHeader value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + BiWidth.GetHashCode();
                hash = hash * 23 + BiHeight.GetHashCode();
                hash = hash * 23 + BiPlanes.GetHashCode();
                hash = hash * 23 + BiBitCount.GetHashCode();
                hash = hash * 23 + BiCompression.GetHashCode();
                hash = hash * 23 + BiSizeImage.GetHashCode();
                hash = hash * 23 + BiXPelsPerMeter.GetHashCode();
                hash = hash * 23 + BiYPelsPerMeter.GetHashCode();
                hash = hash * 23 + BiClrUsed.GetHashCode();
                hash = hash * 23 + BiClrImportant.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("BiWidth: " + BiWidth);
            sb.AppendLine("BiHeight: " + BiHeight);
            sb.AppendLine("BiPlanes: " + BiPlanes);
            sb.AppendLine("BiBitCount: " + BiBitCount);
            sb.AppendLine("BiCompression: " + BiCompression);
            sb.AppendLine("BiSizeImage: " + BiSizeImage);
            sb.AppendLine("BiXPelsPerMeter: " + BiXPelsPerMeter);
            sb.AppendLine("BiYPelsPerMeter: " + BiYPelsPerMeter);
            sb.AppendLine("BiClrUsed: " + BiClrUsed);
            sb.AppendLine("BiClrImportant: " + BiClrImportant);
            return sb.ToString();
        }
    }
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct BitmapV4Header : IEquatable<BitmapV4Header?>, IBitmapHeader
    {
        /// <summary>
        ///   The field BV4Width
        /// </summary>
        public LONG BV4Width;

        /// <summary>
        ///   The field BV4Height
        /// </summary>
        public LONG BV4Height;

        /// <summary>
        ///   The field BV4Planes
        /// </summary>
        public WORD BV4Planes;

        /// <summary>
        ///   The field BV4BitCount
        /// </summary>
        public WORD BV4BitCount;

        /// <summary>
        ///   The field BV4V4Compression
        /// </summary>
        public DWORD BV4V4Compression;

        /// <summary>
        ///   The field BV4SizeImage
        /// </summary>
        public DWORD BV4SizeImage;

        /// <summary>
        ///   The field BV4XPelsPerMeter
        /// </summary>
        public LONG BV4XPelsPerMeter;

        /// <summary>
        ///   The field BV4YPelsPerMeter
        /// </summary>
        public LONG BV4YPelsPerMeter;

        /// <summary>
        ///   The field BV4ClrUsed
        /// </summary>
        public DWORD BV4ClrUsed;

        /// <summary>
        ///   The field BV4ClrImportant
        /// </summary>
        public DWORD BV4ClrImportant;

        /// <summary>
        ///   The field BV4RedMask
        /// </summary>
        public DWORD BV4RedMask;

        /// <summary>
        ///   The field BV4GreenMask
        /// </summary>
        public DWORD BV4GreenMask;

        /// <summary>
        ///   The field BV4BlueMask
        /// </summary>
        public DWORD BV4BlueMask;

        /// <summary>
        ///   The field BV4AlphaMask
        /// </summary>
        public DWORD BV4AlphaMask;

        /// <summary>
        ///   The field BV4CSType
        /// </summary>
        public DWORD BV4CSType;

        /// <summary>
        ///   The field BV4Endpoints
        /// </summary>
        public BmpCieXyzTriple BV4Endpoints;

        /// <summary>
        ///   The field BV4GammaRed
        /// </summary>
        public DWORD BV4GammaRed;

        /// <summary>
        ///   The field BV4GammaGreen
        /// </summary>
        public DWORD BV4GammaGreen;

        /// <summary>
        ///   The field BV4GammaBlue
        /// </summary>
        public DWORD BV4GammaBlue;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BitmapV4Header" /> struct.
        /// </summary>
        /// <param name="BV4Width">The parameter</param>
        /// <param name="BV4Height">The parameter</param>
        /// <param name="BV4Planes">The parameter</param>
        /// <param name="BV4BitCount">The parameter</param>
        /// <param name="BV4V4Compression">The parameter</param>
        /// <param name="BV4SizeImage">The parameter</param>
        /// <param name="BV4XPelsPerMeter">The parameter</param>
        /// <param name="BV4YPelsPerMeter">The parameter</param>
        /// <param name="BV4ClrUsed">The parameter</param>
        /// <param name="BV4ClrImportant">The parameter</param>
        /// <param name="BV4RedMask">The parameter</param>
        /// <param name="BV4GreenMask">The parameter</param>
        /// <param name="BV4BlueMask">The parameter</param>
        /// <param name="BV4AlphaMask">The parameter</param>
        /// <param name="BV4CSType">The parameter</param>
        /// <param name="BV4Endpoints">The parameter</param>
        /// <param name="BV4GammaRed">The parameter</param>
        /// <param name="BV4GammaGreen">The parameter</param>
        /// <param name="BV4GammaBlue">The parameter</param>
        public BitmapV4Header(LONG BV4Width, LONG BV4Height, WORD BV4Planes, WORD BV4BitCount, DWORD BV4V4Compression, DWORD BV4SizeImage, LONG BV4XPelsPerMeter, LONG BV4YPelsPerMeter, DWORD BV4ClrUsed, DWORD BV4ClrImportant, DWORD BV4RedMask, DWORD BV4GreenMask, DWORD BV4BlueMask, DWORD BV4AlphaMask, DWORD BV4CSType, BmpCieXyzTriple BV4Endpoints, DWORD BV4GammaRed, DWORD BV4GammaGreen, DWORD BV4GammaBlue)
        {
            this.BV4Width = BV4Width;
            this.BV4Height = BV4Height;
            this.BV4Planes = BV4Planes;
            this.BV4BitCount = BV4BitCount;
            this.BV4V4Compression = BV4V4Compression;
            this.BV4SizeImage = BV4SizeImage;
            this.BV4XPelsPerMeter = BV4XPelsPerMeter;
            this.BV4YPelsPerMeter = BV4YPelsPerMeter;
            this.BV4ClrUsed = BV4ClrUsed;
            this.BV4ClrImportant = BV4ClrImportant;
            this.BV4RedMask = BV4RedMask;
            this.BV4GreenMask = BV4GreenMask;
            this.BV4BlueMask = BV4BlueMask;
            this.BV4AlphaMask = BV4AlphaMask;
            this.BV4CSType = BV4CSType;
            this.BV4Endpoints = BV4Endpoints;
            this.BV4GammaRed = BV4GammaRed;
            this.BV4GammaGreen = BV4GammaGreen;
            this.BV4GammaBlue = BV4GammaBlue;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapV4Header" />.
        /// </returns>
        public static BitmapV4Header Read(BinaryReader reader)
        {
            var BV4Width = CommonBitmapIO.ReadLONG(reader);
            var BV4Height = CommonBitmapIO.ReadLONG(reader);
            var BV4Planes = CommonBitmapIO.ReadWORD(reader);
            var BV4BitCount = CommonBitmapIO.ReadWORD(reader);
            var BV4V4Compression = CommonBitmapIO.ReadDWORD(reader);
            var BV4SizeImage = CommonBitmapIO.ReadDWORD(reader);
            var BV4XPelsPerMeter = CommonBitmapIO.ReadLONG(reader);
            var BV4YPelsPerMeter = CommonBitmapIO.ReadLONG(reader);
            var BV4ClrUsed = CommonBitmapIO.ReadDWORD(reader);
            var BV4ClrImportant = CommonBitmapIO.ReadDWORD(reader);
            var BV4RedMask = CommonBitmapIO.ReadDWORD(reader);
            var BV4GreenMask = CommonBitmapIO.ReadDWORD(reader);
            var BV4BlueMask = CommonBitmapIO.ReadDWORD(reader);
            var BV4AlphaMask = CommonBitmapIO.ReadDWORD(reader);
            var BV4CSType = CommonBitmapIO.ReadDWORD(reader);
            var BV4Endpoints = CommonBitmapIO.ReadBmpCieXyzTriple(reader);
            var BV4GammaRed = CommonBitmapIO.ReadDWORD(reader);
            var BV4GammaGreen = CommonBitmapIO.ReadDWORD(reader);
            var BV4GammaBlue = CommonBitmapIO.ReadDWORD(reader);
            return new BitmapV4Header(BV4Width, BV4Height, BV4Planes, BV4BitCount, BV4V4Compression, BV4SizeImage, BV4XPelsPerMeter, BV4YPelsPerMeter, BV4ClrUsed, BV4ClrImportant, BV4RedMask, BV4GreenMask, BV4BlueMask, BV4AlphaMask, BV4CSType, BV4Endpoints, BV4GammaRed, BV4GammaGreen, BV4GammaBlue);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapV4Header" />.
        /// </returns>
        public static async Task<BitmapV4Header> ReadAsync(BinaryReader reader)
        {
            var BV4Width = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV4Height = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV4Planes = await CommonBitmapIO.ReadWORDAsync(reader);
            var BV4BitCount = await CommonBitmapIO.ReadWORDAsync(reader);
            var BV4V4Compression = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4SizeImage = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4XPelsPerMeter = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV4YPelsPerMeter = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV4ClrUsed = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4ClrImportant = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4RedMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4GreenMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4BlueMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4AlphaMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4CSType = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4Endpoints = await CommonBitmapIO.ReadBmpCieXyzTripleAsync(reader);
            var BV4GammaRed = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4GammaGreen = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV4GammaBlue = await CommonBitmapIO.ReadDWORDAsync(reader);
            return new BitmapV4Header(BV4Width, BV4Height, BV4Planes, BV4BitCount, BV4V4Compression, BV4SizeImage, BV4XPelsPerMeter, BV4YPelsPerMeter, BV4ClrUsed, BV4ClrImportant, BV4RedMask, BV4GreenMask, BV4BlueMask, BV4AlphaMask, BV4CSType, BV4Endpoints, BV4GammaRed, BV4GammaGreen, BV4GammaBlue);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteLONG(writer, this.BV4Width);
            CommonBitmapIO.WriteLONG(writer, this.BV4Height);
            CommonBitmapIO.WriteWORD(writer, this.BV4Planes);
            CommonBitmapIO.WriteWORD(writer, this.BV4BitCount);
            CommonBitmapIO.WriteDWORD(writer, this.BV4V4Compression);
            CommonBitmapIO.WriteDWORD(writer, this.BV4SizeImage);
            CommonBitmapIO.WriteLONG(writer, this.BV4XPelsPerMeter);
            CommonBitmapIO.WriteLONG(writer, this.BV4YPelsPerMeter);
            CommonBitmapIO.WriteDWORD(writer, this.BV4ClrUsed);
            CommonBitmapIO.WriteDWORD(writer, this.BV4ClrImportant);
            CommonBitmapIO.WriteDWORD(writer, this.BV4RedMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV4GreenMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV4BlueMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV4AlphaMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV4CSType);
            CommonBitmapIO.WriteBmpCieXyzTriple(writer, this.BV4Endpoints);
            CommonBitmapIO.WriteDWORD(writer, this.BV4GammaRed);
            CommonBitmapIO.WriteDWORD(writer, this.BV4GammaGreen);
            CommonBitmapIO.WriteDWORD(writer, this.BV4GammaBlue);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV4Width);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV4Height);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BV4Planes);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BV4BitCount);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4V4Compression);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4SizeImage);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV4XPelsPerMeter);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV4YPelsPerMeter);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4ClrUsed);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4ClrImportant);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4RedMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4GreenMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4BlueMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4AlphaMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4CSType);
            await CommonBitmapIO.WriteBmpCieXyzTripleAsync(writer, this.BV4Endpoints);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4GammaRed);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4GammaGreen);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV4GammaBlue);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(BitmapV4Header? other)
        {
            return other is not null
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV4Width, this.BV4Width)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV4Height, this.BV4Height)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BV4Planes, this.BV4Planes)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BV4BitCount, this.BV4BitCount)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4V4Compression, this.BV4V4Compression)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4SizeImage, this.BV4SizeImage)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV4XPelsPerMeter, this.BV4XPelsPerMeter)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV4YPelsPerMeter, this.BV4YPelsPerMeter)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4ClrUsed, this.BV4ClrUsed)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4ClrImportant, this.BV4ClrImportant)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4RedMask, this.BV4RedMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4GreenMask, this.BV4GreenMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4BlueMask, this.BV4BlueMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4AlphaMask, this.BV4AlphaMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4CSType, this.BV4CSType)
                && EqualityComparer<BmpCieXyzTriple>.Default.Equals(other.Value.BV4Endpoints, this.BV4Endpoints)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4GammaRed, this.BV4GammaRed)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4GammaGreen, this.BV4GammaGreen)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV4GammaBlue, this.BV4GammaBlue)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is BitmapV4Header value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + BV4Width.GetHashCode();
                hash = hash * 23 + BV4Height.GetHashCode();
                hash = hash * 23 + BV4Planes.GetHashCode();
                hash = hash * 23 + BV4BitCount.GetHashCode();
                hash = hash * 23 + BV4V4Compression.GetHashCode();
                hash = hash * 23 + BV4SizeImage.GetHashCode();
                hash = hash * 23 + BV4XPelsPerMeter.GetHashCode();
                hash = hash * 23 + BV4YPelsPerMeter.GetHashCode();
                hash = hash * 23 + BV4ClrUsed.GetHashCode();
                hash = hash * 23 + BV4ClrImportant.GetHashCode();
                hash = hash * 23 + BV4RedMask.GetHashCode();
                hash = hash * 23 + BV4GreenMask.GetHashCode();
                hash = hash * 23 + BV4BlueMask.GetHashCode();
                hash = hash * 23 + BV4AlphaMask.GetHashCode();
                hash = hash * 23 + BV4CSType.GetHashCode();
                hash = hash * 23 + BV4Endpoints.GetHashCode();
                hash = hash * 23 + BV4GammaRed.GetHashCode();
                hash = hash * 23 + BV4GammaGreen.GetHashCode();
                hash = hash * 23 + BV4GammaBlue.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("BV4Width: " + BV4Width);
            sb.AppendLine("BV4Height: " + BV4Height);
            sb.AppendLine("BV4Planes: " + BV4Planes);
            sb.AppendLine("BV4BitCount: " + BV4BitCount);
            sb.AppendLine("BV4V4Compression: " + BV4V4Compression);
            sb.AppendLine("BV4SizeImage: " + BV4SizeImage);
            sb.AppendLine("BV4XPelsPerMeter: " + BV4XPelsPerMeter);
            sb.AppendLine("BV4YPelsPerMeter: " + BV4YPelsPerMeter);
            sb.AppendLine("BV4ClrUsed: " + BV4ClrUsed);
            sb.AppendLine("BV4ClrImportant: " + BV4ClrImportant);
            sb.AppendLine("BV4RedMask: " + BV4RedMask);
            sb.AppendLine("BV4GreenMask: " + BV4GreenMask);
            sb.AppendLine("BV4BlueMask: " + BV4BlueMask);
            sb.AppendLine("BV4AlphaMask: " + BV4AlphaMask);
            sb.AppendLine("BV4CSType: " + BV4CSType);
            sb.AppendLine("BV4Endpoints: " + BV4Endpoints);
            sb.AppendLine("BV4GammaRed: " + BV4GammaRed);
            sb.AppendLine("BV4GammaGreen: " + BV4GammaGreen);
            sb.AppendLine("BV4GammaBlue: " + BV4GammaBlue);
            return sb.ToString();
        }
    }
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct BitmapV5Header : IEquatable<BitmapV5Header?>, IBitmapHeader
    {
        /// <summary>
        ///   The field BV5Width
        /// </summary>
        public LONG BV5Width;

        /// <summary>
        ///   The field BV5Height
        /// </summary>
        public LONG BV5Height;

        /// <summary>
        ///   The field BV5Planes
        /// </summary>
        public WORD BV5Planes;

        /// <summary>
        ///   The field BV5BitCount
        /// </summary>
        public WORD BV5BitCount;

        /// <summary>
        ///   The field BV5Compression
        /// </summary>
        public DWORD BV5Compression;

        /// <summary>
        ///   The field BV5SizeImage
        /// </summary>
        public DWORD BV5SizeImage;

        /// <summary>
        ///   The field BV5XPelsPerMeter
        /// </summary>
        public LONG BV5XPelsPerMeter;

        /// <summary>
        ///   The field BV5YPelsPerMeter
        /// </summary>
        public LONG BV5YPelsPerMeter;

        /// <summary>
        ///   The field BV5ClrUsed
        /// </summary>
        public DWORD BV5ClrUsed;

        /// <summary>
        ///   The field BV5ClrImportant
        /// </summary>
        public DWORD BV5ClrImportant;

        /// <summary>
        ///   The field BV5RedMask
        /// </summary>
        public DWORD BV5RedMask;

        /// <summary>
        ///   The field BV5GreenMask
        /// </summary>
        public DWORD BV5GreenMask;

        /// <summary>
        ///   The field BV5BlueMask
        /// </summary>
        public DWORD BV5BlueMask;

        /// <summary>
        ///   The field BV5AlphaMask
        /// </summary>
        public DWORD BV5AlphaMask;

        /// <summary>
        ///   The field BV5CSType
        /// </summary>
        public DWORD BV5CSType;

        /// <summary>
        ///   The field BV5Endpoints
        /// </summary>
        public BmpCieXyzTriple BV5Endpoints;

        /// <summary>
        ///   The field BV5GammaRed
        /// </summary>
        public DWORD BV5GammaRed;

        /// <summary>
        ///   The field BV5GammaGreen
        /// </summary>
        public DWORD BV5GammaGreen;

        /// <summary>
        ///   The field BV5GammaBlue
        /// </summary>
        public DWORD BV5GammaBlue;

        /// <summary>
        ///   The field BV5Intent
        /// </summary>
        public DWORD BV5Intent;

        /// <summary>
        ///   The field BV5ProfileData
        /// </summary>
        public DWORD BV5ProfileData;

        /// <summary>
        ///   The field BV5ProfileSize
        /// </summary>
        public DWORD BV5ProfileSize;

        /// <summary>
        ///   The field BV5Reserved
        /// </summary>
        public DWORD BV5Reserved;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BitmapV5Header" /> struct.
        /// </summary>
        /// <param name="BV5Width">The parameter</param>
        /// <param name="BV5Height">The parameter</param>
        /// <param name="BV5Planes">The parameter</param>
        /// <param name="BV5BitCount">The parameter</param>
        /// <param name="BV5Compression">The parameter</param>
        /// <param name="BV5SizeImage">The parameter</param>
        /// <param name="BV5XPelsPerMeter">The parameter</param>
        /// <param name="BV5YPelsPerMeter">The parameter</param>
        /// <param name="BV5ClrUsed">The parameter</param>
        /// <param name="BV5ClrImportant">The parameter</param>
        /// <param name="BV5RedMask">The parameter</param>
        /// <param name="BV5GreenMask">The parameter</param>
        /// <param name="BV5BlueMask">The parameter</param>
        /// <param name="BV5AlphaMask">The parameter</param>
        /// <param name="BV5CSType">The parameter</param>
        /// <param name="BV5Endpoints">The parameter</param>
        /// <param name="BV5GammaRed">The parameter</param>
        /// <param name="BV5GammaGreen">The parameter</param>
        /// <param name="BV5GammaBlue">The parameter</param>
        /// <param name="BV5Intent">The parameter</param>
        /// <param name="BV5ProfileData">The parameter</param>
        /// <param name="BV5ProfileSize">The parameter</param>
        /// <param name="BV5Reserved">The parameter</param>
        public BitmapV5Header(LONG BV5Width, LONG BV5Height, WORD BV5Planes, WORD BV5BitCount, DWORD BV5Compression, DWORD BV5SizeImage, LONG BV5XPelsPerMeter, LONG BV5YPelsPerMeter, DWORD BV5ClrUsed, DWORD BV5ClrImportant, DWORD BV5RedMask, DWORD BV5GreenMask, DWORD BV5BlueMask, DWORD BV5AlphaMask, DWORD BV5CSType, BmpCieXyzTriple BV5Endpoints, DWORD BV5GammaRed, DWORD BV5GammaGreen, DWORD BV5GammaBlue, DWORD BV5Intent, DWORD BV5ProfileData, DWORD BV5ProfileSize, DWORD BV5Reserved)
        {
            this.BV5Width = BV5Width;
            this.BV5Height = BV5Height;
            this.BV5Planes = BV5Planes;
            this.BV5BitCount = BV5BitCount;
            this.BV5Compression = BV5Compression;
            this.BV5SizeImage = BV5SizeImage;
            this.BV5XPelsPerMeter = BV5XPelsPerMeter;
            this.BV5YPelsPerMeter = BV5YPelsPerMeter;
            this.BV5ClrUsed = BV5ClrUsed;
            this.BV5ClrImportant = BV5ClrImportant;
            this.BV5RedMask = BV5RedMask;
            this.BV5GreenMask = BV5GreenMask;
            this.BV5BlueMask = BV5BlueMask;
            this.BV5AlphaMask = BV5AlphaMask;
            this.BV5CSType = BV5CSType;
            this.BV5Endpoints = BV5Endpoints;
            this.BV5GammaRed = BV5GammaRed;
            this.BV5GammaGreen = BV5GammaGreen;
            this.BV5GammaBlue = BV5GammaBlue;
            this.BV5Intent = BV5Intent;
            this.BV5ProfileData = BV5ProfileData;
            this.BV5ProfileSize = BV5ProfileSize;
            this.BV5Reserved = BV5Reserved;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapV5Header" />.
        /// </returns>
        public static BitmapV5Header Read(BinaryReader reader)
        {
            var BV5Width = CommonBitmapIO.ReadLONG(reader);
            var BV5Height = CommonBitmapIO.ReadLONG(reader);
            var BV5Planes = CommonBitmapIO.ReadWORD(reader);
            var BV5BitCount = CommonBitmapIO.ReadWORD(reader);
            var BV5Compression = CommonBitmapIO.ReadDWORD(reader);
            var BV5SizeImage = CommonBitmapIO.ReadDWORD(reader);
            var BV5XPelsPerMeter = CommonBitmapIO.ReadLONG(reader);
            var BV5YPelsPerMeter = CommonBitmapIO.ReadLONG(reader);
            var BV5ClrUsed = CommonBitmapIO.ReadDWORD(reader);
            var BV5ClrImportant = CommonBitmapIO.ReadDWORD(reader);
            var BV5RedMask = CommonBitmapIO.ReadDWORD(reader);
            var BV5GreenMask = CommonBitmapIO.ReadDWORD(reader);
            var BV5BlueMask = CommonBitmapIO.ReadDWORD(reader);
            var BV5AlphaMask = CommonBitmapIO.ReadDWORD(reader);
            var BV5CSType = CommonBitmapIO.ReadDWORD(reader);
            var BV5Endpoints = CommonBitmapIO.ReadBmpCieXyzTriple(reader);
            var BV5GammaRed = CommonBitmapIO.ReadDWORD(reader);
            var BV5GammaGreen = CommonBitmapIO.ReadDWORD(reader);
            var BV5GammaBlue = CommonBitmapIO.ReadDWORD(reader);
            var BV5Intent = CommonBitmapIO.ReadDWORD(reader);
            var BV5ProfileData = CommonBitmapIO.ReadDWORD(reader);
            var BV5ProfileSize = CommonBitmapIO.ReadDWORD(reader);
            var BV5Reserved = CommonBitmapIO.ReadDWORD(reader);
            return new BitmapV5Header(BV5Width, BV5Height, BV5Planes, BV5BitCount, BV5Compression, BV5SizeImage, BV5XPelsPerMeter, BV5YPelsPerMeter, BV5ClrUsed, BV5ClrImportant, BV5RedMask, BV5GreenMask, BV5BlueMask, BV5AlphaMask, BV5CSType, BV5Endpoints, BV5GammaRed, BV5GammaGreen, BV5GammaBlue, BV5Intent, BV5ProfileData, BV5ProfileSize, BV5Reserved);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BitmapV5Header" />.
        /// </returns>
        public static async Task<BitmapV5Header> ReadAsync(BinaryReader reader)
        {
            var BV5Width = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV5Height = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV5Planes = await CommonBitmapIO.ReadWORDAsync(reader);
            var BV5BitCount = await CommonBitmapIO.ReadWORDAsync(reader);
            var BV5Compression = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5SizeImage = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5XPelsPerMeter = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV5YPelsPerMeter = await CommonBitmapIO.ReadLONGAsync(reader);
            var BV5ClrUsed = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5ClrImportant = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5RedMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5GreenMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5BlueMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5AlphaMask = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5CSType = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5Endpoints = await CommonBitmapIO.ReadBmpCieXyzTripleAsync(reader);
            var BV5GammaRed = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5GammaGreen = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5GammaBlue = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5Intent = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5ProfileData = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5ProfileSize = await CommonBitmapIO.ReadDWORDAsync(reader);
            var BV5Reserved = await CommonBitmapIO.ReadDWORDAsync(reader);
            return new BitmapV5Header(BV5Width, BV5Height, BV5Planes, BV5BitCount, BV5Compression, BV5SizeImage, BV5XPelsPerMeter, BV5YPelsPerMeter, BV5ClrUsed, BV5ClrImportant, BV5RedMask, BV5GreenMask, BV5BlueMask, BV5AlphaMask, BV5CSType, BV5Endpoints, BV5GammaRed, BV5GammaGreen, BV5GammaBlue, BV5Intent, BV5ProfileData, BV5ProfileSize, BV5Reserved);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteLONG(writer, this.BV5Width);
            CommonBitmapIO.WriteLONG(writer, this.BV5Height);
            CommonBitmapIO.WriteWORD(writer, this.BV5Planes);
            CommonBitmapIO.WriteWORD(writer, this.BV5BitCount);
            CommonBitmapIO.WriteDWORD(writer, this.BV5Compression);
            CommonBitmapIO.WriteDWORD(writer, this.BV5SizeImage);
            CommonBitmapIO.WriteLONG(writer, this.BV5XPelsPerMeter);
            CommonBitmapIO.WriteLONG(writer, this.BV5YPelsPerMeter);
            CommonBitmapIO.WriteDWORD(writer, this.BV5ClrUsed);
            CommonBitmapIO.WriteDWORD(writer, this.BV5ClrImportant);
            CommonBitmapIO.WriteDWORD(writer, this.BV5RedMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV5GreenMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV5BlueMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV5AlphaMask);
            CommonBitmapIO.WriteDWORD(writer, this.BV5CSType);
            CommonBitmapIO.WriteBmpCieXyzTriple(writer, this.BV5Endpoints);
            CommonBitmapIO.WriteDWORD(writer, this.BV5GammaRed);
            CommonBitmapIO.WriteDWORD(writer, this.BV5GammaGreen);
            CommonBitmapIO.WriteDWORD(writer, this.BV5GammaBlue);
            CommonBitmapIO.WriteDWORD(writer, this.BV5Intent);
            CommonBitmapIO.WriteDWORD(writer, this.BV5ProfileData);
            CommonBitmapIO.WriteDWORD(writer, this.BV5ProfileSize);
            CommonBitmapIO.WriteDWORD(writer, this.BV5Reserved);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV5Width);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV5Height);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BV5Planes);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BV5BitCount);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5Compression);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5SizeImage);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV5XPelsPerMeter);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BV5YPelsPerMeter);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5ClrUsed);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5ClrImportant);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5RedMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5GreenMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5BlueMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5AlphaMask);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5CSType);
            await CommonBitmapIO.WriteBmpCieXyzTripleAsync(writer, this.BV5Endpoints);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5GammaRed);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5GammaGreen);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5GammaBlue);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5Intent);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5ProfileData);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5ProfileSize);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.BV5Reserved);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(BitmapV5Header? other)
        {
            return other is not null
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV5Width, this.BV5Width)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV5Height, this.BV5Height)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BV5Planes, this.BV5Planes)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BV5BitCount, this.BV5BitCount)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5Compression, this.BV5Compression)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5SizeImage, this.BV5SizeImage)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV5XPelsPerMeter, this.BV5XPelsPerMeter)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BV5YPelsPerMeter, this.BV5YPelsPerMeter)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5ClrUsed, this.BV5ClrUsed)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5ClrImportant, this.BV5ClrImportant)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5RedMask, this.BV5RedMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5GreenMask, this.BV5GreenMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5BlueMask, this.BV5BlueMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5AlphaMask, this.BV5AlphaMask)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5CSType, this.BV5CSType)
                && EqualityComparer<BmpCieXyzTriple>.Default.Equals(other.Value.BV5Endpoints, this.BV5Endpoints)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5GammaRed, this.BV5GammaRed)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5GammaGreen, this.BV5GammaGreen)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5GammaBlue, this.BV5GammaBlue)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5Intent, this.BV5Intent)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5ProfileData, this.BV5ProfileData)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5ProfileSize, this.BV5ProfileSize)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.BV5Reserved, this.BV5Reserved)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is BitmapV5Header value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + BV5Width.GetHashCode();
                hash = hash * 23 + BV5Height.GetHashCode();
                hash = hash * 23 + BV5Planes.GetHashCode();
                hash = hash * 23 + BV5BitCount.GetHashCode();
                hash = hash * 23 + BV5Compression.GetHashCode();
                hash = hash * 23 + BV5SizeImage.GetHashCode();
                hash = hash * 23 + BV5XPelsPerMeter.GetHashCode();
                hash = hash * 23 + BV5YPelsPerMeter.GetHashCode();
                hash = hash * 23 + BV5ClrUsed.GetHashCode();
                hash = hash * 23 + BV5ClrImportant.GetHashCode();
                hash = hash * 23 + BV5RedMask.GetHashCode();
                hash = hash * 23 + BV5GreenMask.GetHashCode();
                hash = hash * 23 + BV5BlueMask.GetHashCode();
                hash = hash * 23 + BV5AlphaMask.GetHashCode();
                hash = hash * 23 + BV5CSType.GetHashCode();
                hash = hash * 23 + BV5Endpoints.GetHashCode();
                hash = hash * 23 + BV5GammaRed.GetHashCode();
                hash = hash * 23 + BV5GammaGreen.GetHashCode();
                hash = hash * 23 + BV5GammaBlue.GetHashCode();
                hash = hash * 23 + BV5Intent.GetHashCode();
                hash = hash * 23 + BV5ProfileData.GetHashCode();
                hash = hash * 23 + BV5ProfileSize.GetHashCode();
                hash = hash * 23 + BV5Reserved.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("BV5Width: " + BV5Width);
            sb.AppendLine("BV5Height: " + BV5Height);
            sb.AppendLine("BV5Planes: " + BV5Planes);
            sb.AppendLine("BV5BitCount: " + BV5BitCount);
            sb.AppendLine("BV5Compression: " + BV5Compression);
            sb.AppendLine("BV5SizeImage: " + BV5SizeImage);
            sb.AppendLine("BV5XPelsPerMeter: " + BV5XPelsPerMeter);
            sb.AppendLine("BV5YPelsPerMeter: " + BV5YPelsPerMeter);
            sb.AppendLine("BV5ClrUsed: " + BV5ClrUsed);
            sb.AppendLine("BV5ClrImportant: " + BV5ClrImportant);
            sb.AppendLine("BV5RedMask: " + BV5RedMask);
            sb.AppendLine("BV5GreenMask: " + BV5GreenMask);
            sb.AppendLine("BV5BlueMask: " + BV5BlueMask);
            sb.AppendLine("BV5AlphaMask: " + BV5AlphaMask);
            sb.AppendLine("BV5CSType: " + BV5CSType);
            sb.AppendLine("BV5Endpoints: " + BV5Endpoints);
            sb.AppendLine("BV5GammaRed: " + BV5GammaRed);
            sb.AppendLine("BV5GammaGreen: " + BV5GammaGreen);
            sb.AppendLine("BV5GammaBlue: " + BV5GammaBlue);
            sb.AppendLine("BV5Intent: " + BV5Intent);
            sb.AppendLine("BV5ProfileData: " + BV5ProfileData);
            sb.AppendLine("BV5ProfileSize: " + BV5ProfileSize);
            sb.AppendLine("BV5Reserved: " + BV5Reserved);
            return sb.ToString();
        }
    }
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct Bitmap : IEquatable<Bitmap?>, IBitmapHeader
    {
        /// <summary>
        ///   The field BmType
        /// </summary>
        public LONG BmType;

        /// <summary>
        ///   The field BmWidth
        /// </summary>
        public LONG BmWidth;

        /// <summary>
        ///   The field BmHeight
        /// </summary>
        public LONG BmHeight;

        /// <summary>
        ///   The field BmWidthBytes
        /// </summary>
        public LONG BmWidthBytes;

        /// <summary>
        ///   The field BmPlanes
        /// </summary>
        public WORD BmPlanes;

        /// <summary>
        ///   The field BmBitsPixel
        /// </summary>
        public WORD BmBitsPixel;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Bitmap" /> struct.
        /// </summary>
        /// <param name="BmType">The parameter</param>
        /// <param name="BmWidth">The parameter</param>
        /// <param name="BmHeight">The parameter</param>
        /// <param name="BmWidthBytes">The parameter</param>
        /// <param name="BmPlanes">The parameter</param>
        /// <param name="BmBitsPixel">The parameter</param>
        public Bitmap(LONG BmType, LONG BmWidth, LONG BmHeight, LONG BmWidthBytes, WORD BmPlanes, WORD BmBitsPixel)
        {
            this.BmType = BmType;
            this.BmWidth = BmWidth;
            this.BmHeight = BmHeight;
            this.BmWidthBytes = BmWidthBytes;
            this.BmPlanes = BmPlanes;
            this.BmBitsPixel = BmBitsPixel;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="Bitmap" />.
        /// </returns>
        public static Bitmap Read(BinaryReader reader)
        {
            var BmType = CommonBitmapIO.ReadLONG(reader);
            var BmWidth = CommonBitmapIO.ReadLONG(reader);
            var BmHeight = CommonBitmapIO.ReadLONG(reader);
            var BmWidthBytes = CommonBitmapIO.ReadLONG(reader);
            var BmPlanes = CommonBitmapIO.ReadWORD(reader);
            var BmBitsPixel = CommonBitmapIO.ReadWORD(reader);
            return new Bitmap(BmType, BmWidth, BmHeight, BmWidthBytes, BmPlanes, BmBitsPixel);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="Bitmap" />.
        /// </returns>
        public static async Task<Bitmap> ReadAsync(BinaryReader reader)
        {
            var BmType = await CommonBitmapIO.ReadLONGAsync(reader);
            var BmWidth = await CommonBitmapIO.ReadLONGAsync(reader);
            var BmHeight = await CommonBitmapIO.ReadLONGAsync(reader);
            var BmWidthBytes = await CommonBitmapIO.ReadLONGAsync(reader);
            var BmPlanes = await CommonBitmapIO.ReadWORDAsync(reader);
            var BmBitsPixel = await CommonBitmapIO.ReadWORDAsync(reader);
            return new Bitmap(BmType, BmWidth, BmHeight, BmWidthBytes, BmPlanes, BmBitsPixel);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteLONG(writer, this.BmType);
            CommonBitmapIO.WriteLONG(writer, this.BmWidth);
            CommonBitmapIO.WriteLONG(writer, this.BmHeight);
            CommonBitmapIO.WriteLONG(writer, this.BmWidthBytes);
            CommonBitmapIO.WriteWORD(writer, this.BmPlanes);
            CommonBitmapIO.WriteWORD(writer, this.BmBitsPixel);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteLONGAsync(writer, this.BmType);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BmWidth);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BmHeight);
            await CommonBitmapIO.WriteLONGAsync(writer, this.BmWidthBytes);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BmPlanes);
            await CommonBitmapIO.WriteWORDAsync(writer, this.BmBitsPixel);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(Bitmap? other)
        {
            return other is not null
                && EqualityComparer<LONG>.Default.Equals(other.Value.BmType, this.BmType)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BmWidth, this.BmWidth)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BmHeight, this.BmHeight)
                && EqualityComparer<LONG>.Default.Equals(other.Value.BmWidthBytes, this.BmWidthBytes)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BmPlanes, this.BmPlanes)
                && EqualityComparer<WORD>.Default.Equals(other.Value.BmBitsPixel, this.BmBitsPixel)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is Bitmap value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + BmType.GetHashCode();
                hash = hash * 23 + BmWidth.GetHashCode();
                hash = hash * 23 + BmHeight.GetHashCode();
                hash = hash * 23 + BmWidthBytes.GetHashCode();
                hash = hash * 23 + BmPlanes.GetHashCode();
                hash = hash * 23 + BmBitsPixel.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("BmType: " + BmType);
            sb.AppendLine("BmWidth: " + BmWidth);
            sb.AppendLine("BmHeight: " + BmHeight);
            sb.AppendLine("BmWidthBytes: " + BmWidthBytes);
            sb.AppendLine("BmPlanes: " + BmPlanes);
            sb.AppendLine("BmBitsPixel: " + BmBitsPixel);
            return sb.ToString();
        }
    }
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct BmpCieXyzTriple : IEquatable<BmpCieXyzTriple?>, IBitmapHeader
    {
        /// <summary>
        ///   The field CiexyzRed
        /// </summary>
        public BmpCieXyz CiexyzRed;

        /// <summary>
        ///   The field CiexyzGreen
        /// </summary>
        public BmpCieXyz CiexyzGreen;

        /// <summary>
        ///   The field CiexyzBlue
        /// </summary>
        public BmpCieXyz CiexyzBlue;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BmpCieXyzTriple" /> struct.
        /// </summary>
        /// <param name="CiexyzRed">The parameter</param>
        /// <param name="CiexyzGreen">The parameter</param>
        /// <param name="CiexyzBlue">The parameter</param>
        public BmpCieXyzTriple(BmpCieXyz CiexyzRed, BmpCieXyz CiexyzGreen, BmpCieXyz CiexyzBlue)
        {
            this.CiexyzRed = CiexyzRed;
            this.CiexyzGreen = CiexyzGreen;
            this.CiexyzBlue = CiexyzBlue;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BmpCieXyzTriple" />.
        /// </returns>
        public static BmpCieXyzTriple Read(BinaryReader reader)
        {
            var CiexyzRed = CommonBitmapIO.ReadBmpCieXyz(reader);
            var CiexyzGreen = CommonBitmapIO.ReadBmpCieXyz(reader);
            var CiexyzBlue = CommonBitmapIO.ReadBmpCieXyz(reader);
            return new BmpCieXyzTriple(CiexyzRed, CiexyzGreen, CiexyzBlue);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BmpCieXyzTriple" />.
        /// </returns>
        public static async Task<BmpCieXyzTriple> ReadAsync(BinaryReader reader)
        {
            var CiexyzRed = await CommonBitmapIO.ReadBmpCieXyzAsync(reader);
            var CiexyzGreen = await CommonBitmapIO.ReadBmpCieXyzAsync(reader);
            var CiexyzBlue = await CommonBitmapIO.ReadBmpCieXyzAsync(reader);
            return new BmpCieXyzTriple(CiexyzRed, CiexyzGreen, CiexyzBlue);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteBmpCieXyz(writer, this.CiexyzRed);
            CommonBitmapIO.WriteBmpCieXyz(writer, this.CiexyzGreen);
            CommonBitmapIO.WriteBmpCieXyz(writer, this.CiexyzBlue);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteBmpCieXyzAsync(writer, this.CiexyzRed);
            await CommonBitmapIO.WriteBmpCieXyzAsync(writer, this.CiexyzGreen);
            await CommonBitmapIO.WriteBmpCieXyzAsync(writer, this.CiexyzBlue);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(BmpCieXyzTriple? other)
        {
            return other is not null
                && EqualityComparer<BmpCieXyz>.Default.Equals(other.Value.CiexyzRed, this.CiexyzRed)
                && EqualityComparer<BmpCieXyz>.Default.Equals(other.Value.CiexyzGreen, this.CiexyzGreen)
                && EqualityComparer<BmpCieXyz>.Default.Equals(other.Value.CiexyzBlue, this.CiexyzBlue)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is BmpCieXyzTriple value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + CiexyzRed.GetHashCode();
                hash = hash * 23 + CiexyzGreen.GetHashCode();
                hash = hash * 23 + CiexyzBlue.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("CiexyzRed: " + CiexyzRed);
            sb.AppendLine("CiexyzGreen: " + CiexyzGreen);
            sb.AppendLine("CiexyzBlue: " + CiexyzBlue);
            return sb.ToString();
        }
    }
    /// <summary>
    ///   One of BMP file's headers or structures.
    /// </summary>
    public struct BmpCieXyz : IEquatable<BmpCieXyz?>, IBitmapHeader
    {
        /// <summary>
        ///   The field X
        /// </summary>
        public DWORD X;

        /// <summary>
        ///   The field Y
        /// </summary>
        public DWORD Y;

        /// <summary>
        ///   The field Z
        /// </summary>
        public DWORD Z;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BmpCieXyz" /> struct.
        /// </summary>
        /// <param name="X">The parameter</param>
        /// <param name="Y">The parameter</param>
        /// <param name="Z">The parameter</param>
        public BmpCieXyz(DWORD X, DWORD Y, DWORD Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BmpCieXyz" />.
        /// </returns>
        public static BmpCieXyz Read(BinaryReader reader)
        {
            var X = CommonBitmapIO.ReadDWORD(reader);
            var Y = CommonBitmapIO.ReadDWORD(reader);
            var Z = CommonBitmapIO.ReadDWORD(reader);
            return new BmpCieXyz(X, Y, Z);
        }

        /// <summary>
        ///   Reads the structure from <paramref name="reader" />.
        /// </summary>
        /// <param name="reader">The source binary reader</param>
        /// <returns>
        ///   The parsed <see cref="BmpCieXyz" />.
        /// </returns>
        public static async Task<BmpCieXyz> ReadAsync(BinaryReader reader)
        {
            var X = await CommonBitmapIO.ReadDWORDAsync(reader);
            var Y = await CommonBitmapIO.ReadDWORDAsync(reader);
            var Z = await CommonBitmapIO.ReadDWORDAsync(reader);
            return new BmpCieXyz(X, Y, Z);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly void Write(BinaryWriter writer)
        {
            CommonBitmapIO.WriteDWORD(writer, this.X);
            CommonBitmapIO.WriteDWORD(writer, this.Y);
            CommonBitmapIO.WriteDWORD(writer, this.Z);
        }

        /// <summary>
        ///   Writes this structure to the specified <paramref name="writer" />.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await CommonBitmapIO.WriteDWORDAsync(writer, this.X);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.Y);
            await CommonBitmapIO.WriteDWORDAsync(writer, this.Z);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals" />
        public readonly bool Equals(BmpCieXyz? other)
        {
            return other is not null
                && EqualityComparer<DWORD>.Default.Equals(other.Value.X, this.X)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.Y, this.Y)
                && EqualityComparer<DWORD>.Default.Equals(other.Value.Z, this.Z)
                ;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object? other)
            => other is BmpCieXyz value && Equals(value);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("X: " + X);
            sb.AppendLine("Y: " + Y);
            sb.AppendLine("Z: " + Z);
            return sb.ToString();
        }
    }
}

