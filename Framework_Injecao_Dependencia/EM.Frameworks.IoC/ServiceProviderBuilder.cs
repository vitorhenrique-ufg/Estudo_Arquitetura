namespace EM.Frameworks.IoC;
/// <summary>
/// Micro framework de injecao de dependencia... Sonhando em um dia se tornar um Autofac
/// https://autofac.org/
/// </summary>
public class ServiceProviderBuilder
{
    /// <summary>
    /// Registra a implementacao
    /// </summary>
    /// <typeparam name="TInterface">Interface</typeparam>
    /// <typeparam name="TImplementacao">Implementacao real</typeparam>
    /// <returns>
    /// Api fluente
    /// </returns>

    private readonly Dictionary<Type, Type> _mapaDeRegistrosInstancia = new();
    private Dictionary<Type, object> mapaDeInstancia = new();

    private readonly Dictionary<Type, Delegate> _mapaDeRegistrosInstanciaPorFuncao = new();
    private readonly IEstrategiaInjecaoDependencia _estrategiaConstrutor = new EstrategiaInjecaoDependenciaConstrutor();
    private readonly IEstrategiaInjecaoDependencia _estrategiaConstrutorSemParametros = new EstrategiaInjecaoDependenciaConstrutorSemParametros();
    private readonly IEstrategiaInjecaoDependencia _estrategiaPropriedade = new EstrategiaInjecaoDependenciaPropriedade();

    public ServiceProviderBuilder Register<TInterface, TImplementacao>()  where TImplementacao : TInterface
    {
        _mapaDeRegistrosInstancia.Add(typeof(TInterface), typeof(TImplementacao));
        return this;
    }

    public void Register<TInterface>(Func<IServiceProvider, TInterface> value)
    {
        _mapaDeRegistrosInstanciaPorFuncao.Add(typeof(TInterface), value);
    }

    public IServiceProvider Build()
    {
        foreach(KeyValuePair<Type, Type> registroInstancia in _mapaDeRegistrosInstancia)
        {
            ExecuteInjecaoDependencia(registroInstancia.Key, registroInstancia.Value);
        }

        ServiceProviderDefault serviceProvider = new(mapaDeInstancia);

        if (_mapaDeRegistrosInstanciaPorFuncao.Any())
        {
            ExecuteInjecaoDependenciaPorFuncao(serviceProvider);
        }

        return serviceProvider;
    }

    private void ExecuteInjecaoDependencia(Type key, Type type)
    {
        if (UtilitariosInjecaoDependencia.EhInjecaoPorConstrutorSemParametro(type))
        {
            _estrategiaConstrutorSemParametros.Execute(ref mapaDeInstancia, key, type);
        }

        if (UtilitariosInjecaoDependencia.EhInjecaoPorConstrutorComParametro(type))
        {
            _estrategiaConstrutor.Execute(ref mapaDeInstancia, key, type);
        }
        
        _estrategiaPropriedade.Execute(ref mapaDeInstancia, key, type);
    }

    private void ExecuteInjecaoDependenciaPorFuncao(ServiceProviderDefault serviceProvider)
    {
        foreach (KeyValuePair<Type, Delegate> registroInstancia in _mapaDeRegistrosInstanciaPorFuncao)
        {
            object? objetoInstanciadoPorFuncao = registroInstancia.Value.DynamicInvoke(serviceProvider);
            if (objetoInstanciadoPorFuncao is not null)
            {
                mapaDeInstancia.Add(registroInstancia.Key, objetoInstanciadoPorFuncao);
            }
        }
    }
}
