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
using Microsoft.Extensions.DependencyInjection;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services.IRepository;
using WoundClinic_WPF.UI.UserControls;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :HandyControl.Controls.Window
        {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var patientRepo = App.ServiceProvider.GetService<IPatientRepository>();
            new winPatient(patientRepo, App.ServiceProvider.GetService<IPersonRepository>()).ShowDialog();
        }
    }
}
