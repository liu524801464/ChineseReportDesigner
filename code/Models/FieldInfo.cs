using System;
using System.ComponentModel;
namespace ChineseReportDesigner.Models
{
    public class FieldInfo : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Type DataType { get; set; }
        public FieldCategory Category { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public enum FieldCategory
    {
        Dimension,
        Measure,
        DateTime
    }
    public enum AggregationType
    {
        Sum, Count, Avg, Max, Min
    }
}