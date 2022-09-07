namespace EM.Frameworks.IoC.Testes;

public class ServiceProviderTestes
{
    interface IServiceA
    {

    }

    interface IServiceB
    {

    }

    interface IServiceC
    {
        IServiceA ServiceA { get; }
        IServiceB? ServiceB { get; set; }
    }

    interface IServiceD
    {
        public IServiceA? ServiceA { get; set; }
        public IServiceB ServiceB { get; }
        public IServiceC ServiceC { get; }
    }

    class ServiceA : IServiceA
    {

    }

    class ServiceB : IServiceB
    {

    }

    class ServiceC : IServiceC
    {
        public IServiceA ServiceA { get; }
        public IServiceB? ServiceB { get; set; }

        public ServiceC(IServiceA serviceA)
        {
            ServiceA = serviceA;
        }
    }

    class ServiceD : IServiceD
    {
        public IServiceA? ServiceA { get; set; }
        public IServiceB ServiceB { get; }
        public IServiceC ServiceC { get; }

        public ServiceD(IServiceB servicoB, IServiceC serviceC)
        {
            ServiceB = servicoB;
            ServiceC = serviceC;
        }
    }


    [Fact]
    public void Deve_Injetar_Dependencia_Atraves_De_ConstrutorOuPropriedadeSetter()
    {
        ServiceProviderBuilder builder = new();

        builder
            .Register<IServiceA, ServiceA>()
            .Register<IServiceB, ServiceB>()
            .Register<IServiceC, ServiceC>();

        IServiceProvider serviceProvider = builder.Build();

        IServiceC servico = serviceProvider.GetService<IServiceC>();
        
        servico.ServiceA.Should().NotBeNull();
        servico.ServiceB.Should().NotBeNull();
    }

    [Fact]
    public void Deve_Injetar_Dependencia_Atraves_De_Construtor_Com_Mais_De_Um_Parametro()
    {
        ServiceProviderBuilder builder = new();

        builder
            .Register<IServiceA, ServiceA>()
            .Register<IServiceB, ServiceB>()
            .Register<IServiceC, ServiceC>()
            .Register<IServiceD, ServiceD>();

        IServiceProvider serviceProvider = builder.Build();

        IServiceD servico = serviceProvider.GetService<IServiceD>();
        
        servico.ServiceA.Should().NotBeNull();
        servico.ServiceB.Should().NotBeNull();
        servico.ServiceC.Should().NotBeNull();
        servico.ServiceC.ServiceA.Should().NotBeNull();
        servico.ServiceC.ServiceB.Should().NotBeNull();
    }

    [Fact]
    public void Deve_Identificar_A_Falta_De_Dependencia()
    {
        ServiceProviderBuilder builder = new();

        builder
            .Register<IServiceA, ServiceA>()
            .Register<IServiceC, ServiceC>();

        Assert.Throws<Exception>(() => builder.Build());
    }

    [Fact]
    public void Deve_Identificar_A_Falta_De_Dependencia_Construtor()
    {
        ServiceProviderBuilder builder = new();

        builder
            .Register<IServiceB, ServiceB>()
            .Register<IServiceC, ServiceC>();

        Assert.Throws<Exception>(() => builder.Build());
    }
}