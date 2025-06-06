namespace ContentDotNet;

/// <summary>
///   Represents a service.
/// </summary>
public interface IService
{
}

/// <summary>
///   List of services of given type.
/// </summary>
/// <typeparam name="TServiceType">The type of the service.</typeparam>
public class ServiceList<TServiceType> where TServiceType : IService
{
    private readonly List<TServiceType> _serviceCollection;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServiceList{TServiceType}"/> class.
    /// </summary>
    public ServiceList()
    {
        _serviceCollection = [];
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServiceList{TServiceType}"/> class.
    /// </summary>
    /// <param name="services">Services to be incorporated.</param>
    public ServiceList(IEnumerable<TServiceType> services)
    {
        _serviceCollection = [.. services];
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServiceList{TServiceType}"/> class.
    /// </summary>
    /// <param name="firstService">The service to be incorporated.</param>
    public ServiceList(TServiceType firstService)
        : this([firstService])
    {
    }

    /// <summary>
    ///   Registers a given service.
    /// </summary>
    /// <param name="serviceType">The service to register.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void RegisterService(TServiceType serviceType)
    {
        if (_serviceCollection.Any(svc => svc.GetType().FullName == serviceType.GetType().FullName))
            throw new InvalidOperationException($"Service of type {serviceType.GetType()} already exists");

        _serviceCollection.Add(serviceType);
    }

    /// <summary>
    ///   Checks if there's any service of type <paramref name="service"/>.
    /// </summary>
    /// <param name="service">The service's type to check for presence.</param>
    /// <returns>A boolean, indicating whether there's at least one service whose specific type is equal to <paramref name="service"/>'s.</returns>
    public bool ContainsService(TServiceType service)
    {
        return _serviceCollection.Any(svc => svc.GetType().FullName == service.GetType().FullName);
    }

    /// <summary>
    ///   Checks if there's at least one service of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of the service to check for presence.</typeparam>
    /// <returns>A boolean indicating if there's at least one service of type <typeparamref name="T"/>.</returns>
    public bool ContainsService<T>() where T : TServiceType
    {
        return _serviceCollection.OfType<T>().Any();
    }

    /// <summary>
    ///   Returns a service whose type matches <typeparamref name="T"/>, or <see langword="null"/> if
    ///   there's no such registered service of given type.
    /// </summary>
    /// <typeparam name="T">The type of the service to retrieve.</typeparam>
    /// <returns>The service of type <typeparamref name="T"/>.</returns>
    public T? TryGetService<T>() where T : class, TServiceType
    {
        if (!ContainsService<T>())
            return null;

        return (T?)_serviceCollection.SingleOrDefault(svc => svc.GetType().FullName == typeof(T).FullName);
    }
}
