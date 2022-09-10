using System.Reflection;

namespace EM.Frameworks.IoC
{
    public class EstrategiaInjecaoDependenciaConstrutorSemParametros : IEstrategiaInjecaoDependencia
    {
        public void Execute(ref Dictionary<Type, object> mapaDeInstancia, Type key, Type type)
        {
            ConstructorInfo construtorSemParametros = type.GetConstructors().First(m => !m.GetParameters().Any());
            object objetoInstanciado = construtorSemParametros.Invoke(null);

            mapaDeInstancia.Add(key, objetoInstanciado);
        }
    }
}
