namespace EM.Frameworks.IoC
{
    public static class UtilitariosInjecaoDependencia
    {
        public static bool EhInjecaoPorConstrutorSemParametro(Type type) =>
            type.GetConstructors().Any(m => !m.GetParameters().Any());

        public static bool EhInjecaoPorConstrutorComParametro(Type type) =>
            type.GetConstructors().Any(m => m.GetParameters().Any());
    }
}
