using System.Reflection;

namespace EM.Frameworks.IoC
{
    public class EstrategiaInjecaoDependenciaPropriedade : IEstrategiaInjecaoDependencia
    {
        public void Execute(ref Dictionary<Type, object> mapaDeInstancia, Type key, Type type)
        {
            IEnumerable<PropertyInfo> propriedadesSetter = (from propriedade in type.GetProperties()
                                                            from acessor in propriedade.GetAccessors()
                                                            where Equals(acessor.ReturnType, typeof(void))
                                                            select propriedade);

            if (propriedadesSetter.Any()) 
            {
                dynamic implementacaoType = mapaDeInstancia.First(m => Equals(m.Key, key)).Value;
                foreach (PropertyInfo propriedadeSetter in propriedadesSetter)
                {
                    KeyValuePair<Type, object> instancia = mapaDeInstancia.FirstOrDefault(m => Equals(m.Key, propriedadeSetter.PropertyType));
                    if (instancia.Value is null)
                    {
                        throw new Exception("Não foi possível encontrar a dependencia de um dos parametros");
                    }

                    PropertyInfo? propriedade = type.GetProperty(propriedadeSetter.Name);
                    if(propriedade is not null)
                    {
                        propriedadeSetter.SetValue(implementacaoType, instancia.Value);
                    }
                }
            }
        }
    }
}
