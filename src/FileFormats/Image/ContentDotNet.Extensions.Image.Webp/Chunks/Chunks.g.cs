#nullable enable

namespace ContentDotNet.Extensions.Image.Webp.Chunks
{

	/// <summary>
	///   The WebP <c>VP8 </c> chunk data representation.
	/// </summary>
	[FourCC("VP8 ")]
	public class WebpVp8LossyChunk : WebpChunkBase, IEquatable<WebpVp8LossyChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, Vp8Data.
		/// </summary>
		public Stream? Vp8Data { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpVp8LossyChunk" /> class with the specified value.
		/// </summary>
		/// <param name="vp8Data">The parameter that assigns <see cref="Vp8Data" /> directly.</param>
		public WebpVp8LossyChunk(Stream vp8Data)
		{
			this.Vp8Data = vp8Data;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpVp8LossyChunk" /> class.
		/// </summary>
		public WebpVp8LossyChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpVp8LossyChunk val
				&& EqualityComparer<Stream?>.Default.Equals(this.Vp8Data, val.Vp8Data)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Vp8Data?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpVp8LossyChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>VP8L</c> chunk data representation.
	/// </summary>
	[FourCC("VP8L")]
	public class WebpVp8LosslessChunk : WebpChunkBase, IEquatable<WebpVp8LosslessChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, Vp8Data.
		/// </summary>
		public Stream? Vp8Data { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpVp8LosslessChunk" /> class with the specified value.
		/// </summary>
		/// <param name="vp8Data">The parameter that assigns <see cref="Vp8Data" /> directly.</param>
		public WebpVp8LosslessChunk(Stream vp8Data)
		{
			this.Vp8Data = vp8Data;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpVp8LosslessChunk" /> class.
		/// </summary>
		public WebpVp8LosslessChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpVp8LosslessChunk val
				&& EqualityComparer<Stream?>.Default.Equals(this.Vp8Data, val.Vp8Data)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Vp8Data?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpVp8LosslessChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>VP8 </c> chunk data representation.
	/// </summary>
	[FourCC("VP8 ")]
	public class WebpVp8ExtensionChunk : WebpChunkBase, IEquatable<WebpVp8ExtensionChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, ContainsIccp.
		/// </summary>
		public bool? ContainsIccp { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, ContainsAlpha.
		/// </summary>
		public bool? ContainsAlpha { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, ContainsExif.
		/// </summary>
		public bool? ContainsExif { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, ContainsXmp.
		/// </summary>
		public bool? ContainsXmp { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, ContainsAnimation.
		/// </summary>
		public bool? ContainsAnimation { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, CanvasWidthMinusOne.
		/// </summary>
		public uint? CanvasWidthMinusOne { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, CanvasHeightMinusOne.
		/// </summary>
		public uint? CanvasHeightMinusOne { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpVp8ExtensionChunk" /> class with the specified value.
		/// </summary>
		/// <param name="containsIccp">The parameter that assigns <see cref="ContainsIccp" /> directly.</param>
		/// <param name="containsAlpha">The parameter that assigns <see cref="ContainsAlpha" /> directly.</param>
		/// <param name="containsExif">The parameter that assigns <see cref="ContainsExif" /> directly.</param>
		/// <param name="containsXmp">The parameter that assigns <see cref="ContainsXmp" /> directly.</param>
		/// <param name="containsAnimation">The parameter that assigns <see cref="ContainsAnimation" /> directly.</param>
		/// <param name="canvasWidthMinusOne">The parameter that assigns <see cref="CanvasWidthMinusOne" /> directly.</param>
		/// <param name="canvasHeightMinusOne">The parameter that assigns <see cref="CanvasHeightMinusOne" /> directly.</param>
		public WebpVp8ExtensionChunk(bool containsIccp, bool containsAlpha, bool containsExif, bool containsXmp, bool containsAnimation, uint canvasWidthMinusOne, uint canvasHeightMinusOne)
		{
			this.ContainsIccp = containsIccp;
			this.ContainsAlpha = containsAlpha;
			this.ContainsExif = containsExif;
			this.ContainsXmp = containsXmp;
			this.ContainsAnimation = containsAnimation;
			this.CanvasWidthMinusOne = canvasWidthMinusOne;
			this.CanvasHeightMinusOne = canvasHeightMinusOne;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpVp8ExtensionChunk" /> class.
		/// </summary>
		public WebpVp8ExtensionChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpVp8ExtensionChunk val
				&& EqualityComparer<bool?>.Default.Equals(this.ContainsIccp, val.ContainsIccp)
				&& EqualityComparer<bool?>.Default.Equals(this.ContainsAlpha, val.ContainsAlpha)
				&& EqualityComparer<bool?>.Default.Equals(this.ContainsExif, val.ContainsExif)
				&& EqualityComparer<bool?>.Default.Equals(this.ContainsXmp, val.ContainsXmp)
				&& EqualityComparer<bool?>.Default.Equals(this.ContainsAnimation, val.ContainsAnimation)
				&& EqualityComparer<uint?>.Default.Equals(this.CanvasWidthMinusOne, val.CanvasWidthMinusOne)
				&& EqualityComparer<uint?>.Default.Equals(this.CanvasHeightMinusOne, val.CanvasHeightMinusOne)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (ContainsIccp?.GetHashCode() ?? 0);
				hash = hash * 23 + (ContainsAlpha?.GetHashCode() ?? 0);
				hash = hash * 23 + (ContainsExif?.GetHashCode() ?? 0);
				hash = hash * 23 + (ContainsXmp?.GetHashCode() ?? 0);
				hash = hash * 23 + (ContainsAnimation?.GetHashCode() ?? 0);
				hash = hash * 23 + (CanvasWidthMinusOne?.GetHashCode() ?? 0);
				hash = hash * 23 + (CanvasHeightMinusOne?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpVp8ExtensionChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>ANIM</c> chunk data representation.
	/// </summary>
	[FourCC("ANIM")]
	public class WebpAnimationChunk : WebpChunkBase, IEquatable<WebpAnimationChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, BackgroundColor.
		/// </summary>
		public uint? BackgroundColor { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, LoopCount.
		/// </summary>
		public ushort? LoopCount { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpAnimationChunk" /> class with the specified value.
		/// </summary>
		/// <param name="backgroundColor">The parameter that assigns <see cref="BackgroundColor" /> directly.</param>
		/// <param name="loopCount">The parameter that assigns <see cref="LoopCount" /> directly.</param>
		public WebpAnimationChunk(uint backgroundColor, ushort loopCount)
		{
			this.BackgroundColor = backgroundColor;
			this.LoopCount = loopCount;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpAnimationChunk" /> class.
		/// </summary>
		public WebpAnimationChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpAnimationChunk val
				&& EqualityComparer<uint?>.Default.Equals(this.BackgroundColor, val.BackgroundColor)
				&& EqualityComparer<ushort?>.Default.Equals(this.LoopCount, val.LoopCount)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BackgroundColor?.GetHashCode() ?? 0);
				hash = hash * 23 + (LoopCount?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpAnimationChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>ANMF</c> chunk data representation.
	/// </summary>
	[FourCC("ANMF")]
	public class WebpAnimationFrameChunk : WebpChunkBase, IEquatable<WebpAnimationFrameChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, FrameX.
		/// </summary>
		public uint? FrameX { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, FrameY.
		/// </summary>
		public uint? FrameY { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, FrameWidthMinusOne.
		/// </summary>
		public uint? FrameWidthMinusOne { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, FrameHeightMinusOne.
		/// </summary>
		public uint? FrameHeightMinusOne { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, FrameDuration.
		/// </summary>
		public uint? FrameDuration { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, BlendingMethod.
		/// </summary>
		public bool? BlendingMethod { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, DisposalMethod.
		/// </summary>
		public bool? DisposalMethod { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, FrameData.
		/// </summary>
		public Stream? FrameData { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpAnimationFrameChunk" /> class with the specified value.
		/// </summary>
		/// <param name="frameX">The parameter that assigns <see cref="FrameX" /> directly.</param>
		/// <param name="frameY">The parameter that assigns <see cref="FrameY" /> directly.</param>
		/// <param name="frameWidthMinusOne">The parameter that assigns <see cref="FrameWidthMinusOne" /> directly.</param>
		/// <param name="frameHeightMinusOne">The parameter that assigns <see cref="FrameHeightMinusOne" /> directly.</param>
		/// <param name="frameDuration">The parameter that assigns <see cref="FrameDuration" /> directly.</param>
		/// <param name="blendingMethod">The parameter that assigns <see cref="BlendingMethod" /> directly.</param>
		/// <param name="disposalMethod">The parameter that assigns <see cref="DisposalMethod" /> directly.</param>
		/// <param name="frameData">The parameter that assigns <see cref="FrameData" /> directly.</param>
		public WebpAnimationFrameChunk(uint frameX, uint frameY, uint frameWidthMinusOne, uint frameHeightMinusOne, uint frameDuration, bool blendingMethod, bool disposalMethod, Stream frameData)
		{
			this.FrameX = frameX;
			this.FrameY = frameY;
			this.FrameWidthMinusOne = frameWidthMinusOne;
			this.FrameHeightMinusOne = frameHeightMinusOne;
			this.FrameDuration = frameDuration;
			this.BlendingMethod = blendingMethod;
			this.DisposalMethod = disposalMethod;
			this.FrameData = frameData;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpAnimationFrameChunk" /> class.
		/// </summary>
		public WebpAnimationFrameChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpAnimationFrameChunk val
				&& EqualityComparer<uint?>.Default.Equals(this.FrameX, val.FrameX)
				&& EqualityComparer<uint?>.Default.Equals(this.FrameY, val.FrameY)
				&& EqualityComparer<uint?>.Default.Equals(this.FrameWidthMinusOne, val.FrameWidthMinusOne)
				&& EqualityComparer<uint?>.Default.Equals(this.FrameHeightMinusOne, val.FrameHeightMinusOne)
				&& EqualityComparer<uint?>.Default.Equals(this.FrameDuration, val.FrameDuration)
				&& EqualityComparer<bool?>.Default.Equals(this.BlendingMethod, val.BlendingMethod)
				&& EqualityComparer<bool?>.Default.Equals(this.DisposalMethod, val.DisposalMethod)
				&& EqualityComparer<Stream?>.Default.Equals(this.FrameData, val.FrameData)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (FrameX?.GetHashCode() ?? 0);
				hash = hash * 23 + (FrameY?.GetHashCode() ?? 0);
				hash = hash * 23 + (FrameWidthMinusOne?.GetHashCode() ?? 0);
				hash = hash * 23 + (FrameHeightMinusOne?.GetHashCode() ?? 0);
				hash = hash * 23 + (FrameDuration?.GetHashCode() ?? 0);
				hash = hash * 23 + (BlendingMethod?.GetHashCode() ?? 0);
				hash = hash * 23 + (DisposalMethod?.GetHashCode() ?? 0);
				hash = hash * 23 + (FrameData?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpAnimationFrameChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>ALPH</c> chunk data representation.
	/// </summary>
	[FourCC("ALPH")]
	public class WebpAlphaChunk : WebpChunkBase, IEquatable<WebpAlphaChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, Preprocessing.
		/// </summary>
		public uint? Preprocessing { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, FilterMethod.
		/// </summary>
		public uint? FilterMethod { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, CompressionMethod.
		/// </summary>
		public uint? CompressionMethod { get; set; }

	
		/// <summary>
		///   Represents one of WebP chunk properties, named, AlphaBitstream.
		/// </summary>
		public Stream? AlphaBitstream { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpAlphaChunk" /> class with the specified value.
		/// </summary>
		/// <param name="preprocessing">The parameter that assigns <see cref="Preprocessing" /> directly.</param>
		/// <param name="filterMethod">The parameter that assigns <see cref="FilterMethod" /> directly.</param>
		/// <param name="compressionMethod">The parameter that assigns <see cref="CompressionMethod" /> directly.</param>
		/// <param name="alphaBitstream">The parameter that assigns <see cref="AlphaBitstream" /> directly.</param>
		public WebpAlphaChunk(uint preprocessing, uint filterMethod, uint compressionMethod, Stream alphaBitstream)
		{
			this.Preprocessing = preprocessing;
			this.FilterMethod = filterMethod;
			this.CompressionMethod = compressionMethod;
			this.AlphaBitstream = alphaBitstream;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpAlphaChunk" /> class.
		/// </summary>
		public WebpAlphaChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpAlphaChunk val
				&& EqualityComparer<uint?>.Default.Equals(this.Preprocessing, val.Preprocessing)
				&& EqualityComparer<uint?>.Default.Equals(this.FilterMethod, val.FilterMethod)
				&& EqualityComparer<uint?>.Default.Equals(this.CompressionMethod, val.CompressionMethod)
				&& EqualityComparer<Stream?>.Default.Equals(this.AlphaBitstream, val.AlphaBitstream)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Preprocessing?.GetHashCode() ?? 0);
				hash = hash * 23 + (FilterMethod?.GetHashCode() ?? 0);
				hash = hash * 23 + (CompressionMethod?.GetHashCode() ?? 0);
				hash = hash * 23 + (AlphaBitstream?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpAlphaChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>ICCP</c> chunk data representation.
	/// </summary>
	[FourCC("ICCP")]
	public class WebpIccColorProfileChunk : WebpChunkBase, IEquatable<WebpIccColorProfileChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, IccData.
		/// </summary>
		public Stream? IccData { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpIccColorProfileChunk" /> class with the specified value.
		/// </summary>
		/// <param name="iccData">The parameter that assigns <see cref="IccData" /> directly.</param>
		public WebpIccColorProfileChunk(Stream iccData)
		{
			this.IccData = iccData;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpIccColorProfileChunk" /> class.
		/// </summary>
		public WebpIccColorProfileChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpIccColorProfileChunk val
				&& EqualityComparer<Stream?>.Default.Equals(this.IccData, val.IccData)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (IccData?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpIccColorProfileChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>EXIF</c> chunk data representation.
	/// </summary>
	[FourCC("EXIF")]
	public class WebpExifMetadataChunk : WebpChunkBase, IEquatable<WebpExifMetadataChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, ExifData.
		/// </summary>
		public Stream? ExifData { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpExifMetadataChunk" /> class with the specified value.
		/// </summary>
		/// <param name="exifData">The parameter that assigns <see cref="ExifData" /> directly.</param>
		public WebpExifMetadataChunk(Stream exifData)
		{
			this.ExifData = exifData;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpExifMetadataChunk" /> class.
		/// </summary>
		public WebpExifMetadataChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpExifMetadataChunk val
				&& EqualityComparer<Stream?>.Default.Equals(this.ExifData, val.ExifData)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (ExifData?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpExifMetadataChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   The WebP <c>XMP </c> chunk data representation.
	/// </summary>
	[FourCC("XMP ")]
	public class WebpXmpMetadataChunk : WebpChunkBase, IEquatable<WebpXmpMetadataChunk?>
	{
	
		/// <summary>
		///   Represents one of WebP chunk properties, named, XmpData.
		/// </summary>
		public Stream? XmpData { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="WebpXmpMetadataChunk" /> class with the specified value.
		/// </summary>
		/// <param name="xmpData">The parameter that assigns <see cref="XmpData" /> directly.</param>
		public WebpXmpMetadataChunk(Stream xmpData)
		{
			this.XmpData = xmpData;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="WebpXmpMetadataChunk" /> class.
		/// </summary>
		public WebpXmpMetadataChunk()
		{
		}

		/// <inheritdoc cref="object.Equals(object?)" />
		public override bool Equals(object? other)
		{
			return other is WebpXmpMetadataChunk val
				&& EqualityComparer<Stream?>.Default.Equals(this.XmpData, val.XmpData)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (XmpData?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(WebpXmpMetadataChunk? other) => Equals((object?)other);
	}

	/// <summary>
	///   Methods for method-chained mutation of properties within WebP chunks.
	/// </summary>
	public static class FluentChunkExtensions
	{
		/// <summary>
		///   Changes the <see cref="WebpVp8LossyChunk.Vp8Data" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8LossyChunk.Vp8Data" />
		///   property.
		/// </returns>
		public static WebpVp8LossyChunk WithVp8Data(
			this WebpVp8LossyChunk sourceChunk,
			Stream valueToReplaceWith)
		{
			sourceChunk.Vp8Data = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8LosslessChunk.Vp8Data" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8LosslessChunk.Vp8Data" />
		///   property.
		/// </returns>
		public static WebpVp8LosslessChunk WithVp8Data(
			this WebpVp8LosslessChunk sourceChunk,
			Stream valueToReplaceWith)
		{
			sourceChunk.Vp8Data = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8ExtensionChunk.ContainsIccp" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8ExtensionChunk.ContainsIccp" />
		///   property.
		/// </returns>
		public static WebpVp8ExtensionChunk WithContainsIccp(
			this WebpVp8ExtensionChunk sourceChunk,
			bool valueToReplaceWith)
		{
			sourceChunk.ContainsIccp = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8ExtensionChunk.ContainsAlpha" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8ExtensionChunk.ContainsAlpha" />
		///   property.
		/// </returns>
		public static WebpVp8ExtensionChunk WithContainsAlpha(
			this WebpVp8ExtensionChunk sourceChunk,
			bool valueToReplaceWith)
		{
			sourceChunk.ContainsAlpha = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8ExtensionChunk.ContainsExif" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8ExtensionChunk.ContainsExif" />
		///   property.
		/// </returns>
		public static WebpVp8ExtensionChunk WithContainsExif(
			this WebpVp8ExtensionChunk sourceChunk,
			bool valueToReplaceWith)
		{
			sourceChunk.ContainsExif = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8ExtensionChunk.ContainsXmp" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8ExtensionChunk.ContainsXmp" />
		///   property.
		/// </returns>
		public static WebpVp8ExtensionChunk WithContainsXmp(
			this WebpVp8ExtensionChunk sourceChunk,
			bool valueToReplaceWith)
		{
			sourceChunk.ContainsXmp = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8ExtensionChunk.ContainsAnimation" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8ExtensionChunk.ContainsAnimation" />
		///   property.
		/// </returns>
		public static WebpVp8ExtensionChunk WithContainsAnimation(
			this WebpVp8ExtensionChunk sourceChunk,
			bool valueToReplaceWith)
		{
			sourceChunk.ContainsAnimation = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8ExtensionChunk.CanvasWidthMinusOne" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8ExtensionChunk.CanvasWidthMinusOne" />
		///   property.
		/// </returns>
		public static WebpVp8ExtensionChunk WithCanvasWidthMinusOne(
			this WebpVp8ExtensionChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.CanvasWidthMinusOne = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpVp8ExtensionChunk.CanvasHeightMinusOne" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpVp8ExtensionChunk.CanvasHeightMinusOne" />
		///   property.
		/// </returns>
		public static WebpVp8ExtensionChunk WithCanvasHeightMinusOne(
			this WebpVp8ExtensionChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.CanvasHeightMinusOne = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationChunk.BackgroundColor" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationChunk.BackgroundColor" />
		///   property.
		/// </returns>
		public static WebpAnimationChunk WithBackgroundColor(
			this WebpAnimationChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.BackgroundColor = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationChunk.LoopCount" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationChunk.LoopCount" />
		///   property.
		/// </returns>
		public static WebpAnimationChunk WithLoopCount(
			this WebpAnimationChunk sourceChunk,
			ushort valueToReplaceWith)
		{
			sourceChunk.LoopCount = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.FrameX" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.FrameX" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithFrameX(
			this WebpAnimationFrameChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.FrameX = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.FrameY" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.FrameY" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithFrameY(
			this WebpAnimationFrameChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.FrameY = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.FrameWidthMinusOne" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.FrameWidthMinusOne" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithFrameWidthMinusOne(
			this WebpAnimationFrameChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.FrameWidthMinusOne = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.FrameHeightMinusOne" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.FrameHeightMinusOne" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithFrameHeightMinusOne(
			this WebpAnimationFrameChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.FrameHeightMinusOne = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.FrameDuration" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.FrameDuration" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithFrameDuration(
			this WebpAnimationFrameChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.FrameDuration = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.BlendingMethod" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.BlendingMethod" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithBlendingMethod(
			this WebpAnimationFrameChunk sourceChunk,
			bool valueToReplaceWith)
		{
			sourceChunk.BlendingMethod = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.DisposalMethod" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.DisposalMethod" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithDisposalMethod(
			this WebpAnimationFrameChunk sourceChunk,
			bool valueToReplaceWith)
		{
			sourceChunk.DisposalMethod = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAnimationFrameChunk.FrameData" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAnimationFrameChunk.FrameData" />
		///   property.
		/// </returns>
		public static WebpAnimationFrameChunk WithFrameData(
			this WebpAnimationFrameChunk sourceChunk,
			Stream valueToReplaceWith)
		{
			sourceChunk.FrameData = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAlphaChunk.Preprocessing" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAlphaChunk.Preprocessing" />
		///   property.
		/// </returns>
		public static WebpAlphaChunk WithPreprocessing(
			this WebpAlphaChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.Preprocessing = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAlphaChunk.FilterMethod" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAlphaChunk.FilterMethod" />
		///   property.
		/// </returns>
		public static WebpAlphaChunk WithFilterMethod(
			this WebpAlphaChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.FilterMethod = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAlphaChunk.CompressionMethod" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAlphaChunk.CompressionMethod" />
		///   property.
		/// </returns>
		public static WebpAlphaChunk WithCompressionMethod(
			this WebpAlphaChunk sourceChunk,
			uint valueToReplaceWith)
		{
			sourceChunk.CompressionMethod = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpAlphaChunk.AlphaBitstream" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpAlphaChunk.AlphaBitstream" />
		///   property.
		/// </returns>
		public static WebpAlphaChunk WithAlphaBitstream(
			this WebpAlphaChunk sourceChunk,
			Stream valueToReplaceWith)
		{
			sourceChunk.AlphaBitstream = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpIccColorProfileChunk.IccData" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpIccColorProfileChunk.IccData" />
		///   property.
		/// </returns>
		public static WebpIccColorProfileChunk WithIccData(
			this WebpIccColorProfileChunk sourceChunk,
			Stream valueToReplaceWith)
		{
			sourceChunk.IccData = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpExifMetadataChunk.ExifData" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpExifMetadataChunk.ExifData" />
		///   property.
		/// </returns>
		public static WebpExifMetadataChunk WithExifData(
			this WebpExifMetadataChunk sourceChunk,
			Stream valueToReplaceWith)
		{
			sourceChunk.ExifData = valueToReplaceWith;
			return sourceChunk;
		}

		/// <summary>
		///   Changes the <see cref="WebpXmpMetadataChunk.XmpData" /> property inside
		///   the given <paramref name="sourceChunk" /> parameter.
		/// </summary>
		/// <param name="sourceChunk">Input WebP chunk</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceChunk" /> with the new <see cref="WebpXmpMetadataChunk.XmpData" />
		///   property.
		/// </returns>
		public static WebpXmpMetadataChunk WithXmpData(
			this WebpXmpMetadataChunk sourceChunk,
			Stream valueToReplaceWith)
		{
			sourceChunk.XmpData = valueToReplaceWith;
			return sourceChunk;
		}

	}
}

namespace ContentDotNet.Extensions.Image.Webp.Chunks.Utilities
{
	using ContentDotNet.Extensions.Image.Webp.Chunks;
	
	/// <summary>
	///   Create WebP chunks from just their 4 character code.
	/// </summary>
	public static class BuiltInNameToChunkFactory
	{
		/// <summary>
		///   The keys are box four-character-codes and the values are factories to create that chunk. Example:
		///   <code>
		///     WebpChunkBase chunkData = BuiltInNameToChunkFactory
	    ///			.DataFactoryTable["VP8 "]();
		///     WebpVp8Chunk vp8 = (WebpVp8Chunk)chunkData;
		///     // ...
		///   </code>
		/// </summary>
		public static readonly Dictionary<string, Func<WebpChunkBase>> DataFactoryTable = new()
		{
			["VP8 "] = () => new WebpVp8LossyChunk(),
			["VP8L"] = () => new WebpVp8LosslessChunk(),
			["VP8 "] = () => new WebpVp8ExtensionChunk(),
			["ANIM"] = () => new WebpAnimationChunk(),
			["ANMF"] = () => new WebpAnimationFrameChunk(),
			["ALPH"] = () => new WebpAlphaChunk(),
			["ICCP"] = () => new WebpIccColorProfileChunk(),
			["EXIF"] = () => new WebpExifMetadataChunk(),
			["XMP "] = () => new WebpXmpMetadataChunk(),
		};
	}
}

