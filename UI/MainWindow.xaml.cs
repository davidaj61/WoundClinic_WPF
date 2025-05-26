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
using WoundClinic_WPF.Services.Shared;
using WoundClinic_WPF.UI.UserControls;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : HandyControl.Controls.Window
    {
        
        private List<HandyControl.Controls.TabItem> _tabs;
        public MainWindow()
        {
            InitializeComponent();
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _tabs=tabMain.Items.OfType<HandyControl.Controls.TabItem>().ToList();
            if (_tabs != null && _tabs.Any(x => x.Header == "پذیرش بیمار"))
                return;
            else
            {                 
                var tab = new HandyControl.Controls.TabItem
                {
                    Header = "پذیرش بیمار",

                    
                };
                tabMain.Items.Add(tab);
                tabMain.SelectedItem = tab;
            }


            //new winPatient().ShowDialog();
        }
    }
}
