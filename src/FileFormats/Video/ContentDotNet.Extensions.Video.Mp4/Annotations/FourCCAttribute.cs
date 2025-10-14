namespace ContentDotNet.Extensions.Video.Mp4.Annotations
{
    /// <summary>
    ///   Represents a Four Character Code on a box
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FourCCAttribute : Attribute
    {
        /// <summary>
        ///   The value of the four-character-code
        /// </summary>
        public string FourCC { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="FourCCAttribute"/> class.
        /// </summary>
        /// <param name="fourCC">See <see cref="FourCC"/></param>
        public FourCCAttribute(string fourCC)
        {
            FourCC = fourCC;
        }
    }
}
