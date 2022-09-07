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

    private readonly Dictionary<string, Type> _mapaDeRegistrosInstancia = new();
    private Dictionary<string, object> mapaDeInstancia = new();
    private readonly IEstrategiaInjecaoDependencia _estrategiaConstrutor = new EstrategiaInjecaoDependenciaConstrutor();
    private readonly IEstrategiaInjecaoDependencia _estrategiaConstrutorSemParametros = new EstrategiaInjecaoDependenciaConstrutorSemParametros();
    private readonly IEstrategiaInjecaoDependencia _estrategiaPropriedade = new EstrategiaInjecaoDependenciaPropriedade();

    public ServiceProviderBuilder Register<TInterface, TImplementacao>()  where TImplementacao : TInterface
    {
        _mapaDeRegistrosInstancia.Add(typeof(TInterface).Name, typeof(TImplementacao));
        return this;
    }

    public IServiceProvider Build()
    {
        foreach(KeyValuePair<string, Type> registroInstancia in _mapaDeRegistrosInstancia)
        {
            ExecuteInjecaoDependencia(registroInstancia.Key, registroInstancia.Value);
        }

        return new ServiceProviderDefault(mapaDeInstancia);
    }

    public void ExecuteInjecaoDependencia(string key, Type type)
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
}
