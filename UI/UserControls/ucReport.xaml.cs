using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using HandyControl.Controls;
using HandyControl.Data;
using ReportingLibrary;
using WoundClinic_WPF.Models.ViewModels;
using WoundClinic_WPF.Services;


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
            var report = new ReportManager("Reports/rptMonth.mrt");
            // افزودن داده به گزارش
            report.ShowReport<ReportViewModel>(patients, "PatientList");
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            dgvReport.Items.Clear();
            var data=new List<ReportViewModel>();
            if (dtpStart.SelectedDate == null || dtpEnd.SelectedDate == null)
            {
                Growl.Info(new GrowlInfo
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Message="لطفا تاریخ شروع و پایان را مشخص کنید",
                    ShowCloseButton=true,
                    WaitTime=3,

                });
                return;
            }
            data = WoundCareRepository.GetWoundCareBetweenTwoDates(dtpStart.SelectedDate.Value, dtpEnd.SelectedDate.Value);
            dgvReport.ItemsSource= data;
            dgvReport.Items.Refresh();
        }
    }
}
