namespace ContentDotNet.Video.Codecs.H264.Extensions
{
	using ContentDotNet.Video.Codecs.H264.Components;
	using System.Runtime.CompilerServices;

	public static partial class H264Extensions
	{
			/// <summary>
		///   Derives the variable <c>MbaffFrameFlag</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DeriveMbaffFrameFlag(
			this H264CodecContext value)
			=> value.Sps?.MbAdaptiveFrameFieldFlag == true && value.SliceHeader?.FieldPicFlag == false;
			/// <summary>
		///   Derives the variable <c>PicWidthInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicWidthInMbs(
			this H264CodecContext value)
			=> (int)(value.Sps?.PicWidthInMbsMinus1 ?? 1) + 1;
			/// <summary>
		///   Derives the variable <c>PicHeightInMapUnits</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInMapUnits(
			this H264CodecContext value)
			=> (int)(value.Sps?.PicHeightInMapUnitsMinus1 ?? 1) + 1;
			/// <summary>
		///   Derives the variable <c>PicWidthInSamplesL</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicWidthInSamplesL(
			this H264CodecContext value)
			=> (int)value.DerivePicWidthInMbs() * 16;
			/// <summary>
		///   Derives the variable <c>SliceQpy</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveSliceQpy(
			this H264CodecContext value)
			=> (int)(26 + (value.Pps?.PicInitQpMinus26 ?? 1) + (value.SliceHeader?.SliceQpDelta ?? 1));
			/// <summary>
		///   Derives the variable <c>IdrPicFlag</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DeriveIdrPicFlag(
			this H264CodecContext value)
			=> value.NalUnit?.NalUnitType == 5;
			/// <summary>
		///   Derives the variable <c>MaxPicOrderCntLsb</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveMaxPicOrderCntLsb(
			this H264CodecContext value)
			=> (int)Math.Pow(2, (value.Sps?.Log2MaxPicOrderCntLsbMinus4 ?? 0) + 4);
			/// <summary>
		///   Derives the variable <c>MaxFrameNum</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveMaxFrameNum(
			this H264CodecContext value)
			=> (int)Math.Pow(2, (value.Sps?.Log2MaxFrameNumMinus4 ?? 0) + 4);
			/// <summary>
		///   Derives the variable <c>SliceGroupChangeRate</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveSliceGroupChangeRate(
			this H264CodecContext value)
			=> ((int?)value.Pps?.SliceGroupChangeRateMinus1 ?? 0) + 1;
			/// <summary>
		///   Derives the variable <c>PicSizeInMapUnits</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicSizeInMapUnits(
			this H264CodecContext value)
			=> (int)value.DerivePicWidthInMbs() * value.DerivePicHeightInMbs();
			/// <summary>
		///   Derives the variable <c>FrameHeightInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveFrameHeightInMbs(
			this H264CodecContext value)
			=> (2 - (value.Sps!.FrameMbsOnlyFlag ? 1 : 0)) * value.DerivePicHeightInMapUnits();
			/// <summary>
		///   Derives the variable <c>PicHeightInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInMbs(
			this H264CodecContext value)
			=> value.DeriveFrameHeightInMbs() / (1 + (value.Sps!.FrameMbsOnlyFlag ? 1 : 0));
			/// <summary>
		///   Derives the variable <c>MapUnitsInSliceGroup0</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveMapUnitsInSliceGroup0(
			this H264CodecContext value)
			=> Math.Min(((int?)value.SliceHeader?.SliceGroupChangeCycle ?? 0) * value.DeriveSliceGroupChangeRate(), value.DerivePicSizeInMapUnits());
			/// <summary>
		///   Derives the variable <c>PicSizeInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicSizeInMbs(
			this H264CodecContext value)
			=> value.DerivePicWidthInMbs() * value.DerivePicHeightInMbs();
			/// <summary>
		///   Derives the variable <c>BitDepthY</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveBitDepthY(
			this H264CodecContext value)
			=> 8 + ((int?)value.Sps.BitDepthLumaMinus8 ?? 0);
			/// <summary>
		///   Derives the variable <c>BitDepthC</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveBitDepthC(
			this H264CodecContext value)
			=> 8 + ((int?)value.Sps.BitDepthChromaMinus8 ?? 0);
			/// <summary>
		///   Derives the variable <c>ChromaFormat</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static H264ChromaFormat DeriveChromaFormat(
			this H264CodecContext value)
			=> H264ChromaFormat.GetSubsamplingAndSize(value.Sps!);
			/// <summary>
		///   Derives the variable <c>ChromaArrayType</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveChromaArrayType(
			this H264CodecContext value)
			=> !(value.Sps?.SeparateColourPlaneFlag ?? false) ? ((int?)value.Sps?.ChromaFormatIdc ?? 0) : 0;
			/// <summary>
		///   Derives the variable <c>ChromaMacroblockSizes</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static H264MacroblockChromaSizes DeriveChromaMacroblockSizes(
			this H264CodecContext value)
			=> new(16 / value.DeriveChromaFormat().ChromaWidth, 16 / value.DeriveChromaFormat().ChromaHeight);
			/// <summary>
		///   Derives the variable <c>PicHeightInSamplesC</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInSamplesC(
			this H264CodecContext value)
			=> value.DeriveFrameHeightInMbs() * 16 / value.DeriveChromaFormat().ChromaHeight;
			/// <summary>
		///   Derives the variable <c>PicWidthInSamplesC</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicWidthInSamplesC(
			this H264CodecContext value)
			=> value.DerivePicWidthInMbs() * value.DeriveChromaMacroblockSizes().MbWidthC;
			/// <summary>
		///   Derives the variable <c>PicHeightInSamplesL</c>.
		/// </summary>
		/// <param name="value">Source H.264 codec</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInSamplesL(
			this H264CodecContext value)
			=> value.DeriveFrameHeightInMbs() * 16;
		}
}
