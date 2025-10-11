namespace ContentDotNet.Extensions.Video.H264 {

	using ContentDotNet.Extensions.Video.H264.Models;
	using System.Runtime.CompilerServices;

	public static partial class H264Extensions {
			/// <summary>
		///   Derives the variable <c>MbaffFrameFlag</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool MbaffFrameFlag(
			this H264RbspState value)
			=> value.SequenceParameterSetData?.MbAdaptiveFrameFieldFlag == true && value.SliceHeader?.FieldPicFlag == false;

		/// <summary>
		///   Derives the variable <c>MbaffFrameFlag</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DeriveMbaffFrameFlag(
			this H264State value)
			=> value.H264RbspState?.MbaffFrameFlag() ?? ThrowHelper.RbspStateUnavailable<bool>();

			/// <summary>
		///   Derives the variable <c>PicWidthInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicWidthInMbs(
			this H264RbspState value)
			=> (int)(value.SequenceParameterSetData?.PicWidthInMbsMinus1 ?? 1) + 1;

		/// <summary>
		///   Derives the variable <c>PicWidthInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicWidthInMbs(
			this H264State value)
			=> value.H264RbspState?.PicWidthInMbs() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>PicHeightInMapUnits</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicHeightInMapUnits(
			this H264RbspState value)
			=> (int)(value.SequenceParameterSetData?.PicHeightInMapUnitsMinus1 ?? 1) + 1;

		/// <summary>
		///   Derives the variable <c>PicHeightInMapUnits</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInMapUnits(
			this H264State value)
			=> value.H264RbspState?.PicHeightInMapUnits() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>PicWidthInSamplesL</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicWidthInSamplesL(
			this H264RbspState value)
			=> (int)value.PicWidthInMbs() * 16;

		/// <summary>
		///   Derives the variable <c>PicWidthInSamplesL</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicWidthInSamplesL(
			this H264State value)
			=> value.H264RbspState?.PicWidthInSamplesL() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>SliceQpy</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SliceQpy(
			this H264RbspState value)
			=> (int)(26 + (value.PictureParameterSet?.PicInitQpMinus26 ?? 1) + (value.SliceHeader?.SliceQpDelta ?? 1));

		/// <summary>
		///   Derives the variable <c>SliceQpy</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveSliceQpy(
			this H264State value)
			=> value.H264RbspState?.SliceQpy() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>IdrPicFlag</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IdrPicFlag(
			this H264RbspState value)
			=> value.NalUnit?.NalUnitType == 5;

		/// <summary>
		///   Derives the variable <c>IdrPicFlag</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DeriveIdrPicFlag(
			this H264State value)
			=> value.H264RbspState?.IdrPicFlag() ?? ThrowHelper.RbspStateUnavailable<bool>();

			/// <summary>
		///   Derives the variable <c>MaxPicOrderCntLsb</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MaxPicOrderCntLsb(
			this H264RbspState value)
			=> (int)Math.Pow(2, (value.SequenceParameterSetData?.Log2MaxPicOrderCntLsbMinus4 ?? 0) + 4);

		/// <summary>
		///   Derives the variable <c>MaxPicOrderCntLsb</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveMaxPicOrderCntLsb(
			this H264State value)
			=> value.H264RbspState?.MaxPicOrderCntLsb() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>MaxFrameNum</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MaxFrameNum(
			this H264RbspState value)
			=> (int)Math.Pow(2, (value.SequenceParameterSetData?.Log2MaxFrameNumMinus4 ?? 0) + 4);

		/// <summary>
		///   Derives the variable <c>MaxFrameNum</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveMaxFrameNum(
			this H264State value)
			=> value.H264RbspState?.MaxFrameNum() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>SliceGroupChangeRate</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SliceGroupChangeRate(
			this H264RbspState value)
			=> ((int?)value.PictureParameterSet?.SliceGroupChangeRateMinus1 ?? 0) + 1;

		/// <summary>
		///   Derives the variable <c>SliceGroupChangeRate</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveSliceGroupChangeRate(
			this H264State value)
			=> value.H264RbspState?.SliceGroupChangeRate() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>PicSizeInMapUnits</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicSizeInMapUnits(
			this H264RbspState value)
			=> (int)value.PicWidthInMbs() * value.PicHeightInMbs();

		/// <summary>
		///   Derives the variable <c>PicSizeInMapUnits</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicSizeInMapUnits(
			this H264State value)
			=> value.H264RbspState?.PicSizeInMapUnits() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>FrameHeightInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FrameHeightInMbs(
			this H264RbspState value)
			=> (2 - (value.SequenceParameterSetData!.FrameMbsOnlyFlag ? 1 : 0)) * value.PicHeightInMapUnits();

		/// <summary>
		///   Derives the variable <c>FrameHeightInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveFrameHeightInMbs(
			this H264State value)
			=> value.H264RbspState?.FrameHeightInMbs() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>PicHeightInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicHeightInMbs(
			this H264RbspState value)
			=> value.FrameHeightInMbs() / (1 + (value.SequenceParameterSetData!.FrameMbsOnlyFlag ? 1 : 0));

		/// <summary>
		///   Derives the variable <c>PicHeightInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInMbs(
			this H264State value)
			=> value.H264RbspState?.PicHeightInMbs() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>MapUnitsInSliceGroup0</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MapUnitsInSliceGroup0(
			this H264RbspState value)
			=> Math.Min(((int?)value.SliceHeader?.SliceGroupChangeCycle ?? 0) * value.SliceGroupChangeRate(), value.PicSizeInMapUnits());

		/// <summary>
		///   Derives the variable <c>MapUnitsInSliceGroup0</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveMapUnitsInSliceGroup0(
			this H264State value)
			=> value.H264RbspState?.MapUnitsInSliceGroup0() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>PicSizeInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicSizeInMbs(
			this H264RbspState value)
			=> value.PicWidthInMbs() * value.PicHeightInMbs();

		/// <summary>
		///   Derives the variable <c>PicSizeInMbs</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicSizeInMbs(
			this H264State value)
			=> value.H264RbspState?.PicSizeInMbs() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>BitDepthY</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitDepthY(
			this H264RbspState value)
			=> 8 + ((int?)value.SequenceParameterSetData.BitDepthLumaMinus8 ?? 0);

		/// <summary>
		///   Derives the variable <c>BitDepthY</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveBitDepthY(
			this H264State value)
			=> value.H264RbspState?.BitDepthY() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>BitDepthC</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitDepthC(
			this H264RbspState value)
			=> 8 + ((int?)value.SequenceParameterSetData.BitDepthChromaMinus8 ?? 0);

		/// <summary>
		///   Derives the variable <c>BitDepthC</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveBitDepthC(
			this H264State value)
			=> value.H264RbspState?.BitDepthC() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>ChromaFormat</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static H264ChromaFormat ChromaFormat(
			this H264RbspState value)
			=> H264ChromaFormat.GetSubsamplingAndSize(value.SequenceParameterSetData!);

		/// <summary>
		///   Derives the variable <c>ChromaFormat</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static H264ChromaFormat DeriveChromaFormat(
			this H264State value)
			=> value.H264RbspState?.ChromaFormat() ?? ThrowHelper.RbspStateUnavailable<H264ChromaFormat>();

			/// <summary>
		///   Derives the variable <c>ChromaArrayType</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ChromaArrayType(
			this H264RbspState value)
			=> !(value.SequenceParameterSetData?.SeparateColourPlaneFlag ?? false) ? ((int?)value.SequenceParameterSetData?.ChromaFormatIdc ?? 0) : 0;

		/// <summary>
		///   Derives the variable <c>ChromaArrayType</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DeriveChromaArrayType(
			this H264State value)
			=> value.H264RbspState?.ChromaArrayType() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>ChromaMacroblockSizes</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static H264MacroblockChromaSizes ChromaMacroblockSizes(
			this H264RbspState value)
			=> new(16 / value.ChromaFormat().ChromaWidth, 16 / value.ChromaFormat().ChromaHeight);

		/// <summary>
		///   Derives the variable <c>ChromaMacroblockSizes</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static H264MacroblockChromaSizes DeriveChromaMacroblockSizes(
			this H264State value)
			=> value.H264RbspState?.ChromaMacroblockSizes() ?? ThrowHelper.RbspStateUnavailable<H264MacroblockChromaSizes>();

			/// <summary>
		///   Derives the variable <c>PicHeightInSamplesC</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicHeightInSamplesC(
			this H264RbspState value)
			=> value.FrameHeightInMbs() * 16 / value.ChromaFormat().ChromaHeight;

		/// <summary>
		///   Derives the variable <c>PicHeightInSamplesC</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInSamplesC(
			this H264State value)
			=> value.H264RbspState?.PicHeightInSamplesC() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>PicWidthInSamplesC</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicWidthInSamplesC(
			this H264RbspState value)
			=> value.PicWidthInMbs() * value.ChromaMacroblockSizes().MbWidthC;

		/// <summary>
		///   Derives the variable <c>PicWidthInSamplesC</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicWidthInSamplesC(
			this H264State value)
			=> value.H264RbspState?.PicWidthInSamplesC() ?? ThrowHelper.RbspStateUnavailable<int>();

			/// <summary>
		///   Derives the variable <c>PicHeightInSamplesL</c>.
		/// </summary>
		/// <param name="value">Source H.264 RBSP state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PicHeightInSamplesL(
			this H264RbspState value)
			=> value.FrameHeightInMbs() * 16;

		/// <summary>
		///   Derives the variable <c>PicHeightInSamplesL</c>.
		/// </summary>
		/// <param name="value">Source H.264 state</param>
		/// <returns>
		///   The value of the derived variable.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DerivePicHeightInSamplesL(
			this H264State value)
			=> value.H264RbspState?.PicHeightInSamplesL() ?? ThrowHelper.RbspStateUnavailable<int>();

		}
}
