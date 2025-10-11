namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel.Expressions
{
    /// <summary>
    ///   The context index factory expressions. Intended to be used with <see langword="using static"/> in C#. Then
    ///   you can use shorthand methods like:
    ///   <code>
    ///     IContextIndexValue n = I(50);
    ///     IContextIndexValue n2 = A(2, 20);
    ///     IContextIndexValue n3 = R(n, n2);
    ///     IContextIndexValue n4 = B(N(n3));
    ///   </code>
    /// </summary>
    public static class ContextIndexFactoryExpressions
    {
        /// <summary>
        ///   Creates <see cref="ContextIndexIntegerValue"/>.
        /// </summary>
        /// <param name="i">The input value.</param>
        /// <returns><see cref="ContextIndexIntegerValue"/></returns>
        public static IContextIndexValue I(int i)
        {
            return new ContextIndexIntegerValue(i);
        }

        /// <summary>
        ///   Creates <see cref="ContextIndexAffixValue"/>.
        /// </summary>
        /// <param name="prefix">Prefix value</param>
        /// <param name="suffix">Suffix value</param>
        /// <returns><see cref="ContextIndexAffixValue"/></returns>
        public static IContextIndexValue A(int prefix, int suffix)
        {
            return new ContextIndexAffixValue(prefix, suffix);
        }

        /// <summary>
        ///   Combines two <see cref="IContextIndexValue"/> and creates <see cref="UnprocessedContextIndexRecord"/>.
        /// </summary>
        /// <param name="maxBinIdxCtx">maxBinIdxCtx</param>
        /// <param name="ctxIdxOffset">ctxIdxOffset</param>
        /// <returns><see cref="UnprocessedContextIndexRecord"/></returns>
        public static UnprocessedContextIndexRecord R(IContextIndexValue maxBinIdxCtx, IContextIndexValue ctxIdxOffset)
        {
            return new UnprocessedContextIndexRecord(maxBinIdxCtx, ctxIdxOffset);
        }

        /// <summary>
        ///   Sets the source <see cref="IContextIndexValue"/>'s
        ///   <see cref="IContextIndexValue.UsesDecodeBypass"/> property to true.
        /// </summary>
        /// <param name="ctxValue">Source <see cref="IContextIndexValue"/></param>
        /// <returns>Modified <see cref="IContextIndexValue"/></returns>
        public static IContextIndexValue B(IContextIndexValue ctxValue)
        {
            ctxValue.UsesDecodeBypass = true;
            return ctxValue;
        }

        /// <summary>
        ///   Sets the source <see cref="IContextIndexValue"/>'s
        ///   <see cref="IContextIndexValue.HasSuffix"/> property to false.
        /// </summary>
        /// <param name="ctxValue">Source <see cref="IContextIndexValue"/></param>
        /// <returns>Modified <see cref="IContextIndexValue"/></returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="ctxValue"/> is not <see cref="ContextIndexAffixValue"/></exception>
        public static IContextIndexValue N(IContextIndexValue ctxValue)
        {
            ctxValue.HasSuffix = false;
            return ctxValue;
        }
    }
}
