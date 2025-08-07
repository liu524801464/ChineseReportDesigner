using OfficeOpenXml;
using System.Data;
namespace ChineseReportDesigner.Exporters
{
    public static class ExcelExporter
    {
        public static void ExportToExcel(DataTable data, Models.ReportDesign design, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("±¨±í");
            ws.Cells["A1"].LoadFromDataTable(data, true);
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            System.IO.File.WriteAllBytes(filePath, package.GetAsByteArray());
        }
    }
}