using System.Reflection;

namespace EM.Frameworks.IoC
{
    public interface IEstrategiaInjecaoDependencia
    {
        public void Execute(ref Dictionary<string, object> mapaDeInstancia, string key, Type type);
    }
}
