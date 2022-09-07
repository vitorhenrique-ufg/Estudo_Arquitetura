using System.Reflection;

namespace EM.Frameworks.IoC
{
    public class EstrategiaInjecaoDependenciaConstrutorSemParametros : IEstrategiaInjecaoDependencia
    {
        public void Execute(ref Dictionary<string, object> mapaDeInstancia, string key, Type type)
        {
            ConstructorInfo construtorSemParametros = type.GetConstructors().First(m => !m.GetParameters().Any());
            object objetoInstanciado = construtorSemParametros.Invoke(Array.Empty<object>());

            mapaDeInstancia.Add(key, objetoInstanciado);
        }
    }
}
