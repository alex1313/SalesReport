using System.Web.Mvc;

namespace SalesReport.Controllers
{
    using System.IO;
    using System.Linq;
    using DataAccess;
    using Services;
    using ViewModels;

    public class HomeController : Controller
    {
        private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string ReportFileName = "Sales report.xlsx";

        private readonly SalesReportDbContext _dbContext = new SalesReportDbContext();

        private readonly IExcelPackageExportService _excelPackageExportService;
        private readonly IEmailSender _emailSender;

        public HomeController(IExcelPackageExportService excelPackageExportService, IEmailSender emailSender)
        {
            _excelPackageExportService = excelPackageExportService;
            _emailSender = emailSender;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new ReportSettingsViewModel());
        }

        [HttpGet]
        public FileContentResult DownloadReport()
        {
            var orders = _dbContext.Orders.AsEnumerable();

            var excelPackage = _excelPackageExportService.ExportOrder(orders);

            var result = new FileContentResult(excelPackage.GetAsByteArray(), ExcelContentType)
            {
                FileDownloadName = ReportFileName
            };

            return result;
        }

        [HttpPost]
        public ActionResult SendReportByEmail(ReportSettingsViewModel reportSettings)
        {
            if (ModelState.IsValid == false)
                return View("Index", reportSettings);

            var orders = _dbContext.Orders
                .Where(x => reportSettings.StartDate.HasValue == false || x.OrderDate > reportSettings.StartDate.Value)
                .Where(x => reportSettings.EndDate.HasValue == false || x.OrderDate < reportSettings.EndDate.Value)
                .ToArray();

            var excelPackage = _excelPackageExportService.ExportOrder(orders);
            var excelMemoryStream = new MemoryStream(excelPackage.GetAsByteArray());

            _emailSender.Send(reportSettings.RecipientEmail, "Sales report", string.Empty, excelMemoryStream);

            return Json($"Report has been sent successfully ({orders.Length} order details exported)", JsonRequestBehavior.AllowGet);
        }
    }
}
