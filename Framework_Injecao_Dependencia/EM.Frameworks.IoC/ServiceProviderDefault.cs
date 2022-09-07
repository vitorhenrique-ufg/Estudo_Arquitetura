namespace EM.Frameworks.IoC;

internal class ServiceProviderDefault : IServiceProvider
{
    private readonly Dictionary<string, object> _typesProvider;

    public ServiceProviderDefault(Dictionary<string, object> types)
    {
        _typesProvider = types;
    }

    public TInterface GetService<TInterface>()
    {
        _typesProvider.TryGetValue(typeof(TInterface).Name, out object? objetoInstanciado);
        return (TInterface)objetoInstanciado!;
    }
}
