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

        public DressingCareUserControl()
        {

            InitializeComponent();
            tabDressing.Content=App.ServiceProvider.GetRequiredService<CareRegisterUserControl>();
            tabDrug.Content= App.ServiceProvider.GetRequiredService<CareRegisterUserControl>();
        }
        public DressingCareUserControl(IDressingCareRepository services,Person person):this()
        {
            _person = person;
            _services = services;
        }

    }
}
