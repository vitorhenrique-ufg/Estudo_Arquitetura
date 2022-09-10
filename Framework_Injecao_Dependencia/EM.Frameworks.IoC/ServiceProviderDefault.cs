namespace EM.Frameworks.IoC;

internal class ServiceProviderDefault : IServiceProvider
{
    private readonly Dictionary<Type, object> _typesProvider;

    public ServiceProviderDefault(Dictionary<Type, object> types)
    {
        _typesProvider = types;
    }

    public TInterface GetService<TInterface>()
    {
        //Service locator
        //object? objetoInstanciado = (TInterface)_typesProvider[typeof(TInterface)];
    
    _typesProvider.TryGetValue(typeof(TInterface), out object? objetoInstanciado);
        return (TInterface)objetoInstanciado!;
    }
}
