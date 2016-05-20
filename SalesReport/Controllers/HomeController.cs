using System.Web.Mvc;

namespace SalesReport.Controllers
{
    using System.IO;
    using System.Linq;
    using DataAccess;
    using Services;

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

        public ActionResult Index()
        {
            return View();
        }

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

        public JsonResult SendReportByEmail()
        {
            var orders = _dbContext.Orders.AsEnumerable();

            var excelPackage = _excelPackageExportService.ExportOrder(orders);
            var excelMemoryStream = new MemoryStream(excelPackage.GetAsByteArray());

            _emailSender.Send("test@gmail.com", "Sales report", string.Empty, excelMemoryStream);

            return Json(true);
        }
    }
}
