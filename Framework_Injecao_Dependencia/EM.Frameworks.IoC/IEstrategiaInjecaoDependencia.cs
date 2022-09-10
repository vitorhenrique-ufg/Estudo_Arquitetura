using System.Reflection;

namespace EM.Frameworks.IoC
{
    public interface IEstrategiaInjecaoDependencia
    {
        public void Execute(ref Dictionary<Type, object> mapaDeInstancia, Type key, Type type);
    }
}
