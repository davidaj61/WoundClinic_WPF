using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services.IRepository;

namespace WoundClinic_WPF.UI.UserControls
{
    /// <summary>
    /// Interaction logic for DressingCareUserControl1.xaml
    /// </summary>
    public partial class DressingCareUserControl : UserControl
    {
        private readonly IDressingCareRepository _services;
        private Person _person;
        private CareRegisterUserControl ucCare; 

        public DressingCareUserControl()
        {

            InitializeComponent();
            
            if (tcSelectCares.SelectedIndex==0)
            {
                ucCare = new CareRegisterUserControl(App.ServiceProvider.GetRequiredService<IDressingRepository>(),false);
                ucCare.Parent = this;
                tabDressing.Content = ucCare;
            }
            else
            {
                ucCare = new CareRegisterUserControl(App.ServiceProvider.GetRequiredService<IDressingRepository>(), true);
                ucCare.Parent = this;
                tabDrug.Content = ucCare;
            }
            //ucDrug.IsDrug = true;
            //tabDrug.Content=ucDrug;
        }
        public DressingCareUserControl(IDressingCareRepository services,Person person):this()
        {
            _person = person;
            _services = services;
        }

        private void tcSelectCares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (tcSelectCares.SelectedIndex == 0)
            {
                ucCare = new CareRegisterUserControl(App.ServiceProvider.GetRequiredService<IDressingRepository>(), false);
                ucCare.Parent = this;
                tabDressing.Content = ucCare;
            }
            else
            {
                ucCare = new CareRegisterUserControl(App.ServiceProvider.GetRequiredService<IDressingRepository>(), true);
                ucCare.Parent = this;
                tabDrug.Content = ucCare;
            }
        }
    }
}
