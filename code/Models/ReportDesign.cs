using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace ChineseReportDesigner.Models
{
    public class ReportDesign : INotifyPropertyChanged
    {
        public List<FieldInfo> AvailableFields { get; set; } = new();
        public ObservableCollection<FieldInfo> RowGroups { get; set; } = new();
        public ObservableCollection<FieldInfo> ColumnFields { get; set; } = new();
        public ObservableCollection<AggregationRule> Aggregations { get; set; } = new();
        public bool EnableYearQuarterMode { get; set; } = false;
        public string DateField { get; set; } = "";
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}