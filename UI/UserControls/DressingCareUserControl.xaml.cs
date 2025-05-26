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
    
    private Person _person;
    private CareRegisterUserControl ucCare; 

    public DressingCareUserControl()
    {

        InitializeComponent();
        
        if (tcSelectCares.SelectedIndex==0)
        {
            ucCare = new CareRegisterUserControl();
            ucCare.Parent = this;
            tabDressing.Content = ucCare;
        }
        else
        {
            ucCare = new CareRegisterUserControl();
            ucCare.Parent = this;
            tabDrug.Content = ucCare;
        }
        
    }
    public DressingCareUserControl(Person person):this()
    {
        _person = person;
       
    }

    private void tcSelectCares_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (tcSelectCares.SelectedIndex == 0)
        {
            ucCare = new CareRegisterUserControl();
            ucCare.Parent = this;
            tabDressing.Content = ucCare;
        }
        else
        {
            ucCare = new CareRegisterUserControl();
            ucCare.Parent = this;
            tabDrug.Content = ucCare;
        }
    }

    private void rbtIranian_Checked(object sender, RoutedEventArgs e)
    {

    }
}
