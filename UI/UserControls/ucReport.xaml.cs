using HandyControl.Controls;
using HandyControl.Data;
using Stimulsoft.Report;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Models.ViewModels.Report;
using WoundClinic_WPF.Services;


namespace WoundClinic_WPF.UI.UserControls
{
    /// <summary>
    /// Interaction logic for ucReport.xaml
    /// </summary>
    public partial class ucReport : UserControl
    {
        private List<ReportViewModel> data = new List<ReportViewModel>();
        public ucReport()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkO46nMQvol4ASeg91in+mGJLnn2KMIpg3eSXQSgaFOm15+0l" +
                                             "hekKip+wRGMwXsKpHAkTvorOFqnpF9rchcYoxHXtjNDLiDHZGTIWq6D/2q4k/eiJm9fV6FdaJIUbWGS3whFWRLPHWC" +
                                             "BsWnalqTdZlP9knjaWclfjmUKf2Ksc5btMD6pmR7ZHQfHXfdgYK7tLR1rqtxYxBzOPq3LIBvd3spkQhKb07LTZQoyQ" +
                                             "3vmRSMALmJSS6ovIS59XPS+oSm8wgvuRFqE1im111GROa7Ww3tNJTA45lkbXX+SocdwXvEZyaaq61Uc1dBg+4uFRxv" +
                                             "yRWvX5WDmJz1X0VLIbHpcIjdEDJUvVAN7Z+FW5xKsV5ySPs8aegsY9ndn4DmoZ1kWvzUaz+E1mxMbOd3tyaNnmVhPZ" +
                                             "eIBILmKJGN0BwnnI5fu6JHMM/9QR2tMO1Z4pIwae4P92gKBrt0MqhvnU1Q6kIaPPuG2XBIvAWykVeH2a9EP6064e11" +
                                             "PFCBX4gEpJ3XFD0peE5+ddZh+h495qUc1H2B";
            var report = new StiReport();
            report.Load(@"Reports\rptMonth.mrt");
            report.RegBusinessObject("rptCons", data);
            
            report.Dictionary.Synchronize();
            report.Compile();
            report.ShowWithWpf();

        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            dgvReport.Items.Clear();
            
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
