using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WoundClinic.Models.ViewModels;
using WoundClinic_WPF.Services;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for winPatientSearch.xaml
    /// </summary>
    public partial class winPatientSearch : Window
    {
        private List<SearchedPatientViewModel> searchedPatientViewModels = new();
        public winPatientSearch()
        {
            InitializeComponent();
        }

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            if((sender as TextBox).Text.Length==3)
            {
                searchedPatientViewModels = PatientRepository.SearchPatients((sender as TextBox).Text);
            }
            else if ((sender as TextBox).Text.Length < 3)
            {
                searchedPatientViewModels.Clear();
            }
            else 
            {

            }
        }
    }
}
