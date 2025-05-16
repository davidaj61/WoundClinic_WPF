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
using Fluent;
using Microsoft.Extensions.DependencyInjection;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services.IRepository;
using WoundClinic_WPF.UI.UserControls;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Person person = new Person()
        {
            NationalCode = 1234567890,
            FirstName = "John",
            LastName = "Doe",
            Gender = true,
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tabPatient.Items.Add(new DressingCareUserControl(App.ServiceProvider.GetRequiredService<IDressingCareRepository>(),person));
        }
    }
}
