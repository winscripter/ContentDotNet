namespace ContentDotNet.Extensions.Video.H264.SeiModel.Abstractions
{
    /// <summary>
    ///   The SEI object.
    /// </summary>
    public interface IH264SeiObject
    {
        /// <summary>
        ///   The code that identifies this SEI object.
        /// </summary>
        uint Id { get; }

        /// <summary>
        ///   The function name based on the ITU-T H.264 spec.
        /// </summary>
        string FunctionName { get; }

        /// <summary>
        ///   The display name for this SEI element.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Payload size is written here automatically during the parsing of SEI.
        /// </summary>
        uint PayloadSize { get; set; }

        /// <summary>
        ///   The I/O for this object.
        /// </summary>
        IH264SeiIO<object> IO { get; }
    }

    /// <summary>
    ///   The SEI object.
    /// </summary>
    public interface IH264SeiObject<T> : IH264SeiObject
    {
        /// <summary>
        ///   The I/O for this object.
        /// </summary>
        new IH264SeiIO<T> IO { get; }
    }
}
