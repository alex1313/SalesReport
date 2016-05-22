namespace SalesReport.IoC.Installers
{
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using DataAccess.UnitOfWork;

    public class DataAccessInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();

            container.Register(Component.For<IUnitOfWorkFactory>().AsFactory().LifestyleSingleton(),
                Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestyleTransient());
        }
    }
}