namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   List of SEI model parameters.
/// </summary>
public sealed class SeiModelParameterList
{
    private readonly List<ISeiModelParameter> _parameters;

    /// <summary>
    ///   Initializes a new instance of the <see cref="SeiModelParameterList"/> class.
    /// </summary>
    public SeiModelParameterList()
    {
        _parameters = [];
    }

    /// <summary>
    ///   Adds an SEI model parameter.
    /// </summary>
    /// <param name="parameter">SEI model parameter</param>
    public void Add(ISeiModelParameter parameter)
    {
        _parameters.Add(parameter);
    }

    /// <summary>
    ///   Gets the SEI model parameter of the specified type.
    /// </summary>
    /// <typeparam name="TSeiModelParameter">Type of the SEI model parameter.</typeparam>
    /// <returns>SEI model parameter of specified type.</returns>
    public TSeiModelParameter? TryGet<TSeiModelParameter>()
        where TSeiModelParameter : class, ISeiModelParameter
    {
        ISeiModelParameter? parameter = _parameters.SingleOrDefault(p => p is TSeiModelParameter);
        if (parameter is null)
            return null;

        return (TSeiModelParameter)Convert.ChangeType(parameter, typeof(TSeiModelParameter));
    }
}
