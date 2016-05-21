namespace SalesReport.Services.Implementation
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Domain.Entities;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    public class ExcelPackageExportService : IExcelPackageExportService
    {
        private const string DateFormat = "dd.mm.yyyy";

        public ExcelPackage ExportOrder(IEnumerable<Order> orders)
        {
            var excelPackage = new ExcelPackage();

            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

            worksheet.Cells[1, 1].Value = "Order id";
            worksheet.Cells[1, 2].Value = "Order date";
            worksheet.Cells[1, 3].Value = "Unit price";
            worksheet.Cells[1, 4].Value = "Quantity";
            worksheet.Cells[1, 5].Value = "Sum";

            var orderDetailsList = orders.SelectMany(x => x.OrderDetails).ToList();
            for (var i = 0; i < orderDetailsList.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = orderDetailsList[i].OrderID;

                var orderDateCell = worksheet.Cells[i + 2, 2];
                orderDateCell.Style.Numberformat.Format = orderDateCell.Style.Numberformat.Format = DateFormat;
                orderDateCell.Value = orderDetailsList[i].Order.OrderDate;

                var unitPriceCell = worksheet.Cells[i + 2, 3];
                unitPriceCell.Value = orderDetailsList[i].UnitPrice;

                var quantityCell = worksheet.Cells[i + 2, 4];
                quantityCell.Value = orderDetailsList[i].Quantity;

                worksheet.Cells[i + 2, 5].Formula = $"{unitPriceCell.Address} * {quantityCell.Address}";
            }

            using (var cells = worksheet.Cells["A1:E1"])
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