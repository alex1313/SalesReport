using System.Web.Mvc;

namespace SalesReport.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using DataAccess;
    using DataAccess.UnitOfWork;
    using Services;
    using ViewModels;

    public class HomeController : Controller
    {
        private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string ReportFileName = "Sales report.xlsx";

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IExcelPackageExportService _excelPackageExportService;
        private readonly IEmailSender _emailSender;

        public HomeController(IExcelPackageExportService excelPackageExportService, IEmailSender emailSender, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _excelPackageExportService = excelPackageExportService;
            _emailSender = emailSender;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new ReportSettingsViewModel());
        }

        [HttpGet]
        public FileContentResult DownloadReport()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var orderDetails = uow.OrderDetailsRepository.GetAll();

                var excelPackage = _excelPackageExportService.ExportOrder(orderDetails);

                var result = new FileContentResult(excelPackage.GetAsByteArray(), ExcelContentType)
                {
                    FileDownloadName = ReportFileName
                };

                return result;
            }
        }

        [HttpPost]
        public ActionResult SendReportByEmail(ReportSettingsViewModel reportSettings)
        {
            if (ModelState.IsValid == false)
                return View("Index", reportSettings);

            var startDate = reportSettings.StartDate ?? DateTime.MinValue;
            var endDate = reportSettings.EndDate ?? DateTime.MaxValue;

            // should be a query in the future
            using (var uow = _unitOfWorkFactory.Create())
            {
                var orderDetails = uow.OrderDetailsRepository
                    .Get(x => x.Order.OrderDate > startDate && x.Order.OrderDate < endDate)
                    .ToArray();

                var excelPackage = _excelPackageExportService.ExportOrder(orderDetails);
                var excelMemoryStream = new MemoryStream(excelPackage.GetAsByteArray());

                _emailSender.Send(reportSettings.RecipientEmail, "Sales report", string.Empty, excelMemoryStream);

                return Json($"Report has been sent successfully ({orderDetails.Length} order details exported)", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
