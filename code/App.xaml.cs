using ChineseReportDesigner.ViewModels;
using ChineseReportDesigner.Views;
using System.Windows;
namespace ChineseReportDesigner
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var vm = new MainViewModel();
            var win = new MainWindow { DataContext = vm };
            win.Show();
            base.OnStartup(e);
        }
    }
}