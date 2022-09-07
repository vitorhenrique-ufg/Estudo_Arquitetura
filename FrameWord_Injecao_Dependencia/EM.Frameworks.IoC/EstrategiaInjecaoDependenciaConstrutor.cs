using System.Reflection;

namespace EM.Frameworks.IoC
{
    public class EstrategiaInjecaoDependenciaConstrutor : IEstrategiaInjecaoDependencia
    {
        public void Execute(ref Dictionary<string, object> mapaDeInstancia, string key, Type type)
        {
            ConstructorInfo construtorComParametros = type.GetConstructors()[0];
            ParameterInfo[] parametrosConstrutor = construtorComParametros.GetParameters();

            List<object> parameters = new();

            foreach (ParameterInfo parametro in parametrosConstrutor)
            {
                if (!mapaDeInstancia.TryGetValue(parametro.ParameterType.Name, out object? instanciaEncontrada))
                {
                    throw new Exception("Não foi possível encontrar a dependencia de um dos parametros");
                }
                parameters.Add(instanciaEncontrada);
            }
                object objetoInstanciado = construtorComParametros.Invoke(parameters.ToArray());
                mapaDeInstancia.Add(key, objetoInstanciado);
        }
    }
}
