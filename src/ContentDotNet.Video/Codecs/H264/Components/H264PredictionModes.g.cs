namespace ContentDotNet.Video.Codecs.H264.Components
{
	/// <summary>
	///   H.264 prediction modes.
	/// </summary>
	public static class H264PredictionModes
	{
		/// <summary>
		///   The prediction mode na.
		/// </summary>
		public const int na = 0;

		/// <summary>
		///   The prediction mode Pred_L0.
		/// </summary>
		public const int Pred_L0 = 1;

		/// <summary>
		///   The prediction mode Pred_L1.
		/// </summary>
		public const int Pred_L1 = 2;

		/// <summary>
		///   The prediction mode Direct.
		/// </summary>
		public const int Direct = 3;

		/// <summary>
		///   The prediction mode Intra_4x4.
		/// </summary>
		public const int Intra_4x4 = 4;

		/// <summary>
		///   The prediction mode Intra_8x8.
		/// </summary>
		public const int Intra_8x8 = 5;

		/// <summary>
		///   The prediction mode Intra_16x16.
		/// </summary>
		public const int Intra_16x16 = 6;

		/// <summary>
		///   The prediction mode BiPred.
		/// </summary>
		public const int BiPred = 7;

	}
}
