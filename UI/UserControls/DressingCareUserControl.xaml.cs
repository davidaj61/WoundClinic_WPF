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

namespace WoundClinic_WPF.UI.UserControls;

/// <summary>
/// Interaction logic for DressingCareUserControl1.xaml
/// </summary>
public partial class DressingCareUserControl : UserControl
{
    //private HandyControl.Controls.TabItem _tab;
    private Patient _patient;
    private CareRegisterUserControl ucCare;
    public Patient Patient => _patient;

    public DressingCareUserControl()
    {

        InitializeComponent();
        this.DataContext = this;
       
        
    }
    public DressingCareUserControl(Patient patient/*,HandyControl.Controls.TabItem tabitem*/):this()
    {
        _patient = patient;
        //_tab = tabitem;
       
    }

    private void tcSelectCares_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        
    }

    private void rbtIranian_Checked(object sender, RoutedEventArgs e)
    {

    }
}
