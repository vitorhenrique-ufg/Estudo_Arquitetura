namespace EM.Frameworks.IoC;

public interface IServiceProvider
{
    TInterface GetService<TInterface>();
}
