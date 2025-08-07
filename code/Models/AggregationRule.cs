using System.ComponentModel;
namespace ChineseReportDesigner.Models
{
    public class AggregationRule : INotifyPropertyChanged
    {
        public FieldInfo Field { get; set; }
        public AggregationType Type { get; set; } = AggregationType.Sum;
        public string DisplayName => $"{Type}({Field?.DisplayName})";
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}