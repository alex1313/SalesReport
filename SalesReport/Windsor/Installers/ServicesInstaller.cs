namespace SalesReport.Windsor.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Services;
    using Services.Implementation;

    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IExcelPackageExportService>().ImplementedBy<ExcelPackageExportService>()
                    .LifestyleTransient(),

                Component.For<IEmailSender>().ImplementedBy<EmailSender>()
                    .LifestyleTransient()
                );
        }
    }
}