namespace ChineseReportDesigner.Models
{
    public class ColumnInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public bool IsTotal { get; set; }
    }
}