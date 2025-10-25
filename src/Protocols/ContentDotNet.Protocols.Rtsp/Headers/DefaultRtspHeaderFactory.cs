namespace ContentDotNet.Protocols.Rtsp.Headers
{
    using System;

    /// <summary>
    ///   A default implementation of an RTSP header factory.
    /// </summary>
    public class DefaultRtspHeaderFactory : IRtspHeaderFactory
    {
        private static readonly IEnumerable<Type> types;
        private static readonly IEnumerable<object> blankInstances;

        static DefaultRtspHeaderFactory()
        {
            types = typeof(DefaultRtspHeaderFactory)
                .Assembly
                .GetTypes()
                .Where(x => x.Namespace == "ContentDotNet.Protocols.Rtsp.Headers.Impl");

            blankInstances = types.Select(x => Activator.CreateInstance(x, null)!);
        }

        /// <inheritdoc cref="IRtspHeaderFactory.Create(string)" />
        public IRtspHeader Create(string rtspHeaderName)
        {
            object obj = blankInstances.SingleOrDefault(x => ((IRtspHeader)x).Text == rtspHeaderName)
                ?? throw new InvalidOperationException("No RTSP header exists with name " + rtspHeaderName);

            // New fresh copy since they have instance fields
            return (IRtspHeader)Activator.CreateInstance(obj.GetType(), null)!;
        }

        /// <inheritdoc cref="IRtspHeaderFactory.Create{T}()" />
        public T Create<T>() where T : IRtspHeader
        {
            return (T)((object)Create(typeof(T)));
        }

        /// <inheritdoc cref="IRtspHeaderFactory.Create(Type)" />
        public IRtspHeader Create(Type type)
        {
            Type type1 = types.SingleOrDefault(x => x.BaseType == type)
                ?? throw new InvalidOperationException("No RTSP header exists with name " + type.FullName);

            // New fresh copy since they have instance fields
            return (IRtspHeader)Activator.CreateInstance(type1, null)!;
        }
    }
}
