using ChineseReportDesigner.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace ChineseReportDesigner.Engines
{
    public class PivotEngine
    {
        public DataTable Generate(Models.ReportDesign design, List<Dictionary<string, object>> data)
        {
            var dt = new DataTable();
            foreach (var f in design.RowGroups)
                dt.Columns.Add(f.DisplayName, typeof(string));
            var quarterEngine = new QuarterPivotEngine();
            var columns = quarterEngine.BuildYearQuarterColumns(data, design.DateField);
            foreach (var col in columns)
                dt.Columns.Add(col.DisplayName, typeof(decimal));
            var grouped = data.GroupBy(row =>
                design.RowGroups.Select(g => row[g.Name]?.ToString()).ToArray());
            foreach (var g in grouped)
            {
                var row = dt.NewRow();
                for (int i = 0; i < design.RowGroups.Count; i++)
                    row[i] = g.Key[i];
                var valueField = design.Aggregations.First().Field.Name;
                var rowData = quarterEngine.PivotRow(g, columns, valueField);
                int colIdx = design.RowGroups.Count;
                foreach (var col in columns)
                {
                    row[colIdx++] = rowData.GetValueOrDefault(col.Name, 0);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}