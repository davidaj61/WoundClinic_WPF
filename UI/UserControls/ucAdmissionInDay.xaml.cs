using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.Shared;
using WoundClinic_WPF.UI.UserControls;

namespace UI.UserControls
{
    /// <summary>
    /// Interaction logic for ucAdmissionInDay.xaml
    /// </summary>
    public partial class ucAdmissionInDay : UserControl
    {
        
        public static ucAdmissionInDay Instance { get; private set; }
        public ucAdmissionInDay(DateTime date)
        {

            InitializeComponent();
            Instance = this;
            this.DataContext = this;
            LoadAdmissionInDay(date);
        }

        public void LoadAdmissionInDay(DateTime date)
        {
            var items = WoundCareRepository.GetAdmissionListByDate(date);
            if (items == null)
                return;
            dgvAdmission.ItemsSource=WoundCareRepository.GetAdmissionListByDate(date);
            dgvAdmission.Items.Refresh();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var wc = WoundCareRepository.GetWoundCareById((dgvAdmission.SelectedItem as WoundCare).Id);
            var cares = DressingCareRepository.GetListByWoundCareId(wc.Id);
            var dCUC = new DressingCareUserControl();

            dCUC.PrintAdmission(wc,cares);
        }
    }
}
