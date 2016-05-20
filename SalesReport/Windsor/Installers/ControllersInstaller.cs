namespace SalesReport.Windsor.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .Pick().If(type => type.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());
        }
    }
}