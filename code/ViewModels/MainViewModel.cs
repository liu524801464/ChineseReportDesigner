using ChineseReportDesigner.Models;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System;
namespace ChineseReportDesigner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ReportDesign Design { get; set; } = new();
        private DataTable _previewData;
        public DataView PreviewData => _previewData?.DefaultView;
        public ICommand LoadDataCommand { get; set; }
        public ICommand ExportExcelCommand { get; set; }
        private List<Dictionary<string, object>> _rawData;
        public MainViewModel()
        {
            LoadDataCommand = new RelayCommand(LoadSampleData);
            ExportExcelCommand = new RelayCommand(ExportToExcel);
        }
        private void LoadSampleData()
        {
            _rawData = new List<Dictionary<string, object>>
            {
                new() { ["Town"] = "A镇", ["Category"] = "农产品", ["SubCategory"] = "大米", ["SaleDate"] = new DateTime(2020, 3, 1), ["Sales"] = 30m },
                new() { ["Town"] = "A镇", ["Category"] = "农产品", ["SubCategory"] = "大米", ["SaleDate"] = new DateTime(2020, 6, 1), ["Sales"] = 20m },
                new() { ["Town"] = "A镇", ["Category"] = "农产品", ["SubCategory"] = "大米", ["SaleDate"] = new DateTime(2020, 9, 1), ["Sales"] = 25m },
                new() { ["Town"] = "A镇", ["Category"] = "农产品", ["SubCategory"] = "大米", ["SaleDate"] = new DateTime(2020, 12, 1), ["Sales"] = 25m },
                new() { ["Town"] = "B镇", ["Category"] = "工业品", ["SubCategory"] = "五金", ["SaleDate"] = new DateTime(2021, 3, 1), ["Sales"] = 35m },
            };
            Design.AvailableFields = new List<FieldInfo>
            {
                new() { Name = "Town", DisplayName = "乡镇", DataType = typeof(string), Category = FieldCategory.Dimension },
                new() { Name = "Category", DisplayName = "类别", DataType = typeof(string), Category = FieldCategory.Dimension },
                new() { Name = "SubCategory", DisplayName = "子类别", DataType = typeof(string), Category = FieldCategory.Dimension },
                new() { Name = "SaleDate", DisplayName = "销售日期", DataType = typeof(DateTime), Category = FieldCategory.DateTime },
                new() { Name = "Sales", DisplayName = "销售额", DataType = typeof(decimal), Category = FieldCategory.Measure }
            };
            Design.DateField = "SaleDate";
            Design.EnableYearQuarterMode = true;
            RefreshPreview();
        }
        private void RefreshPreview()
        {
            if (Design.RowGroups.Any() && Design.Aggregations.Any())
            {
                var engine = new Engines.PivotEngine();
                _previewData = engine.Generate(Design, _rawData);
                OnPropertyChanged(nameof(PreviewData));
            }
        }
        private void ExportToExcel()
        {
            Exporters.ExcelExporter.ExportToExcel(_previewData, Design, "中国式报表.xlsx");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}