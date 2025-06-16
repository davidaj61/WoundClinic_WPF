using System.Windows;
using System.Windows.Controls;
using Stimulsoft.Report;
using WoundClinic_WPF.Models.ViewModels;

namespace WoundClinic_WPF.UI.UserControls
{
    /// <summary>
    /// Interaction logic for ucReport.xaml
    /// </summary>
    public partial class ucReport : UserControl
    {
        private List<ReportViewModel> patients = new List<ReportViewModel>();
        public ucReport()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            StiReport report = new StiReport();
            report.Load("Reports/rptMonth.mrt"); // مسیر فایل گزارش MRT
            // افزودن داده به گزارش
            report.RegBusinessObject("PatientList", patients);
            // Sync کردن داده‌ها
            report.Dictionary.Synchronize();
            report.Compile();
            report.Render();
            report.ShowWithWpf();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
