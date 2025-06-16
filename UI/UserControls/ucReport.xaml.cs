using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using FastReport;
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
            Report report = new Report();
            report.Load("Reports/rptMonth.frx"); // مسیر فایل گزارش MRT
            // افزودن داده به گزارش
            report.RegisterData(patients, "PatientList");
            report.GetDataSource("PatientList").Enabled=true;
            
            //report.Prepare();
            report.Show();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
