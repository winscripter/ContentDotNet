namespace ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations
{
    /// <summary>
    ///   Specifies that a property uses the f(v) descriptor where f is <paramref name="nBits"/>.
    /// </summary>
    /// <param name="nBits">Number of bits</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FDescriptorAttribute(int nBits) : Attribute
    {
        /// <summary>
        ///   Number of bits
        /// </summary>
        public int NumberOfBits { get; set; } = nBits;
    }

    /// <summary>
    ///   Specifies that a property uses the ue(v) descriptor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UeDescriptorAttribute : Attribute
    {
    }

    /// <summary>
    ///   Specifies that a property uses the se(v) descriptor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SeDescriptorAttribute : Attribute
    {
    }

    /// <summary>
    ///   Specifies that a property uses the ae(v) descriptor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AeDescriptorAttribute : Attribute
    {
    }

    /// <summary>
    ///   Specifies that a property uses the te(v) descriptor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TeDescriptorAttribute : Attribute
    {
    }

    /// <summary>
    ///   Specifies that a property uses the me(v) descriptor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MeDescriptorAttribute : Attribute
    {
    }

    /// <summary>
    ///   Specifies that a property uses the ce(v) descriptor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CeDescriptorAttribute : Attribute
    {
    }

    /// <summary>
    ///   Specifies that a property uses the u(x) descriptor where x can be any value provided by <paramref name="innerValue"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UDescriptorAttribute(string innerValue) : Attribute
    {
        /// <summary>
        ///   Inner value. Can be numeric or the letter "v".
        /// </summary>
        public string InnerValue { get; set; } = innerValue;
    }
}
