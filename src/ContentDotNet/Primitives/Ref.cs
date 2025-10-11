namespace ContentDotNet.Primitives
{
    /// <summary>
    ///   A value type stored inside a reference type.
    /// </summary>
    public class Ref<T>
        where T : unmanaged
    {
        private T value;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Ref{T}"/> class.
        /// </summary>
        /// <param name="value">The value to initialize with.</param>
        public Ref(T value)
        {
            this.value = value;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Ref{T}"/> class with the default value.
        /// </summary>
        public Ref()
        {
            this.value = default;
        }

        /// <summary>
        ///   The backing value contained in a reference type.
        /// </summary>
        public T Value
        {
            get => value;
            set => this.value = value;
        }
    }
}
