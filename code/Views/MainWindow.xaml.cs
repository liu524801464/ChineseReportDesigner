using ChineseReportDesigner.ViewModels;
using System.Windows;
namespace ChineseReportDesigner.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}