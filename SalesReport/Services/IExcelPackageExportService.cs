namespace SalesReport.Services
{
    using System.Collections.Generic;
    using Domain;
    using OfficeOpenXml;

    public interface IExcelPackageExportService
    {
        ExcelPackage ExportOrder(IEnumerable<Order> orders);
    }
}