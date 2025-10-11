namespace ContentDotNet
{
    using ContentDotNet.Security;
    using System.Drawing;

    /// <summary>
    ///   The configuration.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        ///   The memory allocator.
        /// </summary>
        public IMemoryAllocator MemoryAllocator { get; set; }

        /// <summary>
        ///   Processing security.
        /// </summary>
        public ProcessingSecurity ProcessingSecurity { get; set; }

        public Configuration(IMemoryAllocator memoryAllocator, ProcessingSecurity processingSecurity)
        {
            MemoryAllocator = memoryAllocator;
            ProcessingSecurity = processingSecurity;
        }

        public Configuration()
        {
            MemoryAllocator = new MemoryAllocator(this);
            ProcessingSecurity = new ProcessingSecurity()
            {
                DoSOptions = new()
                {
                    MaximumAllocateBytes = 1 * 1024 * 1024 * 1024, // 1GB
                    UseMaximumAllocateBytes = true
                },
                ImageSecurityOptions = new()
                {
                    MaximumImageSize = new Size(10 * 1024, 8 * 1024)
                }
            };
        }
    }
}
