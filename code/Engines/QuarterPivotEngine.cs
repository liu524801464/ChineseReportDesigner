using ChineseReportDesigner.Models;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
namespace ChineseReportDesigner.Engines
{
    public class QuarterPivotEngine
    {
        public List<ColumnInfo> BuildYearQuarterColumns(List<Dictionary<string, object>> data, string dateField)
        {
            var years = data.Where(d => d[dateField] is DateTime)
                .Select(d => ((DateTime)d[dateField]).Year)
                .Distinct().OrderBy(y => y).ToList();
            var cols = new List<ColumnInfo>();
            foreach (var y in years)
            {
                cols.Add(new ColumnInfo { Name = $"{y}Total", DisplayName = $"{y}ÄêÐ¡¼Æ", Year = y, IsTotal = true });
                for (int q = 1; q <= 4; q++)
                    cols.Add(new ColumnInfo { Name = $"{y}Q{q}", DisplayName = $"Q{q}", Year = y, Quarter = q, IsTotal = false });
            }
            return cols;
        }
        public Dictionary<string, decimal> PivotRow(
            IEnumerable<Dictionary<string, object>> group,
            List<ColumnInfo> columns,
            string valueField)
        {
            var result = new Dictionary<string, decimal>();
            foreach (var col in columns)
            {
                if (col.IsTotal)
                {
                    var sum = group
                        .Where(g => g["SaleDate"] is DateTime dt && dt.Year == col.Year)
                        .Sum(g => Convert.ToDecimal(g[valueField]));
                    result[col.Name] = Math.Round(sum, 2);
                }
                else
                {
                    var sum = group
                        .Where(g => g["SaleDate"] is DateTime dt
                            && dt.Year == col.Year
                            && GetQuarter(dt) == col.Quarter)
                        .Sum(g => Convert.ToDecimal(g[valueField]));
                    result[col.Name] = Math.Round(sum, 2);
                }
            }
            return result;
        }
        private int GetQuarter(DateTime dt) => (dt.Month + 2) / 3;
    }
}