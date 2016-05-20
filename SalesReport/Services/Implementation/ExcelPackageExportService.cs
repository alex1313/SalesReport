namespace SalesReport.Services.Implementation
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Domain;
    using Domain.Entities;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    class ExcelPackageExportService : IExcelPackageExportService
    {
        private const string DateFormat = "dd.mm.yyyy";

        public ExcelPackage ExportOrder(IEnumerable<Order> orders)
        {
            var excelPackage = new ExcelPackage();

            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

            worksheet.Cells[1, 1].Value = "Дата заказа";

            var orderList = orders.ToList();
            for (var i = 0; i < orderList.Count; i++)
            {
                var cell = worksheet.Cells[i + 2, 1];
                cell.Style.Numberformat.Format = cell.Style.Numberformat.Format = DateFormat;
                cell.Value = orderList[i].OrderDate;
            }

            using (var cells = worksheet.Cells["A1"])
            {
                cells.Style.Font.Bold = true;
                cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cells.Style.Fill.BackgroundColor.SetColor(Color.Gold);
                cells.Style.Font.Color.SetColor(Color.Black);
            }

            return excelPackage;
        }
    }
}