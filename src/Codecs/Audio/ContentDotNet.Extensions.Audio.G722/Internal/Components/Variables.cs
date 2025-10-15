namespace ContentDotNet.Extensions.Audio.G722.Internal.Components
{
    /// <summary>
    ///   Last 22 variables that were parsed.
    /// </summary>
    internal class Variables
    {
        private readonly G722Variables[] variables;

        public Variables()
        {
            this.variables = new G722Variables[22];
        }

        public G722Variables this[int index]
        {
            get => this.variables[index];
            set => this.variables[index] = value;
        }

        public void Add(G722Variables variable)
        {
            for (int i = 21; i > 0; i--)
                variables[i] = variables[i - 1];
            variables[0] = variable;
        }
    }
}
