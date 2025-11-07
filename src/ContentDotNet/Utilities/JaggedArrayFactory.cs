namespace ContentDotNet.Utilities
{
    /// <summary>
    ///   Factory for .NET jagged arrays.
    /// </summary>
    public static class JaggedArrayFactory
    {
        /// <summary>
        /// Creates a 2D jagged array of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">Number of rows (outer array).</param>
        /// <param name="length2">Number of columns (inner arrays).</param>
        /// <returns>A 2D jagged array of type T.</returns>
        public static T[][] Create2D<T>(int length1, int length2)
        {
            T[][] obj = new T[length1][];
            for (int i = 0; i < length1; i++)
                obj[i] = new T[length2];
            return obj;
        }

        /// <summary>
        /// Creates a 3D jagged array of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">First dimension size.</param>
        /// <param name="length2">Second dimension size.</param>
        /// <param name="length3">Third dimension size.</param>
        /// <returns>A 3D jagged array of type T.</returns>
        public static T[][][] Create3D<T>(int length1, int length2, int length3)
        {
            T[][][] obj = new T[length1][][];
            for (int i = 0; i < length1; i++)
                obj[i] = Create2D<T>(length2, length3);
            return obj;
        }

        /// <summary>
        /// Creates a 4D jagged array of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">First dimension size.</param>
        /// <param name="length2">Second dimension size.</param>
        /// <param name="length3">Third dimension size.</param>
        /// <param name="length4">Fourth dimension size.</param>
        /// <returns>A 4D jagged array of type T.</returns>
        public static T[][][][] Create4D<T>(int length1, int length2, int length3, int length4)
        {
            T[][][][] obj = new T[length1][][][];
            for (int i = 0; i < length1; i++)
                obj[i] = Create3D<T>(length2, length3, length4);
            return obj;
        }

        /// <summary>
        /// Creates a 5D jagged array of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">First dimension size.</param>
        /// <param name="length2">Second dimension size.</param>
        /// <param name="length3">Third dimension size.</param>
        /// <param name="length4">Fourth dimension size.</param>
        /// <param name="length5">Fifth dimension size.</param>
        /// <returns>A 5D jagged array of type T.</returns>
        public static T[][][][][] Create5D<T>(int length1, int length2, int length3, int length4, int length5)
        {
            T[][][][][] obj = new T[length1][][][][];
            for (int i = 0; i < length1; i++)
                obj[i] = Create4D<T>(length2, length3, length4, length5);
            return obj;
        }

        /// <summary>
        /// Creates a 2D nested list of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">Number of outer lists (rows).</param>
        /// <param name="length2">Number of inner lists (columns).</param>
        /// <returns>A 2D nested list of type T.</returns>
        public static List<List<T>> Create2DList<T>(int length1, int length2)
        {
            var result = new List<List<T>>(length1);
            for (int i = 0; i < length1; i++)
            {
                var inner = new List<T>(length2);
                for (int j = 0; j < length2; j++)
                    inner.Add(default!);
                result.Add(inner);
            }
            return result;
        }

        /// <summary>
        /// Creates a 3D nested list of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">First dimension size.</param>
        /// <param name="length2">Second dimension size.</param>
        /// <param name="length3">Third dimension size.</param>
        /// <returns>A 3D nested list of type T.</returns>
        public static List<List<List<T>>> Create3DList<T>(int length1, int length2, int length3)
        {
            var result = new List<List<List<T>>>(length1);
            for (int i = 0; i < length1; i++)
                result.Add(Create2DList<T>(length2, length3));
            return result;
        }

        /// <summary>
        /// Creates a 4D nested list of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">First dimension size.</param>
        /// <param name="length2">Second dimension size.</param>
        /// <param name="length3">Third dimension size.</param>
        /// <param name="length4">Fourth dimension size.</param>
        /// <returns>A 4D nested list of type T.</returns>
        public static List<List<List<List<T>>>> Create4DList<T>(int length1, int length2, int length3, int length4)
        {
            var result = new List<List<List<List<T>>>>(length1);
            for (int i = 0; i < length1; i++)
                result.Add(Create3DList<T>(length2, length3, length4));
            return result;
        }

        /// <summary>
        /// Creates a 5D nested list of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="length1">First dimension size.</param>
        /// <param name="length2">Second dimension size.</param>
        /// <param name="length3">Third dimension size.</param>
        /// <param name="length4">Fourth dimension size.</param>
        /// <param name="length5">Fifth dimension size.</param>
        /// <returns>A 5D nested list of type T.</returns>
        public static List<List<List<List<List<T>>>>> Create5DList<T>(int length1, int length2, int length3, int length4, int length5)
        {
            var result = new List<List<List<List<List<T>>>>>(length1);
            for (int i = 0; i < length1; i++)
                result.Add(Create4DList<T>(length2, length3, length4, length5));
            return result;
        }

        /// <summary>
        /// Creates a 2D jagged array initialized with the specified value.
        /// </summary>
        public static T[][] CreateInitialized2D<T>(int length1, int length2, T value)
        {
            var result = new T[length1][];
            for (int i = 0; i < length1; i++)
            {
                result[i] = new T[length2];
                for (int j = 0; j < length2; j++)
                    result[i][j] = value;
            }
            return result;
        }

        /// <summary>
        /// Creates a 3D jagged array initialized with the specified value.
        /// </summary>
        public static T[][][] CreateInitialized3D<T>(int length1, int length2, int length3, T value)
        {
            var result = new T[length1][][];
            for (int i = 0; i < length1; i++)
                result[i] = CreateInitialized2D(length2, length3, value);
            return result;
        }

        /// <summary>
        /// Creates a 4D jagged array initialized with the specified value.
        /// </summary>
        public static T[][][][] CreateInitialized4D<T>(int length1, int length2, int length3, int length4, T value)
        {
            var result = new T[length1][][][];
            for (int i = 0; i < length1; i++)
                result[i] = CreateInitialized3D(length2, length3, length4, value);
            return result;
        }

        /// <summary>
        /// Creates a 5D jagged array initialized with the specified value.
        /// </summary>
        public static T[][][][][] CreateInitialized5D<T>(int length1, int length2, int length3, int length4, int length5, T value)
        {
            var result = new T[length1][][][][];
            for (int i = 0; i < length1; i++)
                result[i] = CreateInitialized4D(length2, length3, length4, length5, value);
            return result;
        }

        /// <summary>
        /// Creates a 2D nested list initialized with the specified value.
        /// </summary>
        public static List<List<T>> CreateInitialized2DList<T>(int length1, int length2, T value)
        {
            var result = new List<List<T>>(length1);
            for (int i = 0; i < length1; i++)
            {
                var inner = new List<T>(length2);
                for (int j = 0; j < length2; j++)
                    inner.Add(value);
                result.Add(inner);
            }
            return result;
        }

        /// <summary>
        /// Creates a 3D nested list initialized with the specified value.
        /// </summary>
        public static List<List<List<T>>> CreateInitialized3DList<T>(int length1, int length2, int length3, T value)
        {
            var result = new List<List<List<T>>>(length1);
            for (int i = 0; i < length1; i++)
                result.Add(CreateInitialized2DList(length2, length3, value));
            return result;
        }

        /// <summary>
        /// Creates a 4D nested list initialized with the specified value.
        /// </summary>
        public static List<List<List<List<T>>>> CreateInitialized4DList<T>(int length1, int length2, int length3, int length4, T value)
        {
            var result = new List<List<List<List<T>>>>(length1);
            for (int i = 0; i < length1; i++)
                result.Add(CreateInitialized3DList(length2, length3, length4, value));
            return result;
        }

        /// <summary>
        /// Creates a 5D nested list initialized with the specified value.
        /// </summary>
        public static List<List<List<List<List<T>>>>> CreateInitialized5DList<T>(int length1, int length2, int length3, int length4, int length5, T value)
        {
            var result = new List<List<List<List<List<T>>>>>(length1);
            for (int i = 0; i < length1; i++)
                result.Add(CreateInitialized4DList(length2, length3, length4, length5, value));
            return result;
        }
    }
}
