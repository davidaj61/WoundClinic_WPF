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
using WoundClinic_WPF.Services;

namespace WoundClinic_WPF.UI.UserControls;

/// <summary>
/// Interaction logic for CareRegisterUserControl.xaml
/// </summary>
public partial class CareRegisterUserControl : UserControl
{

    private List<Dressing> DataList=new List<Dressing>();

    public static readonly DependencyProperty DependencyParent = DependencyProperty.Register("Parent", typeof(DressingCareUserControl), typeof(CareRegisterUserControl), new PropertyMetadata(null));
    //public static readonly DependencyProperty DependencyIsDrug = DependencyProperty.Register("IsDrug", typeof(bool), typeof(CareRegisterUserControl), new PropertyMetadata(false));

    public DressingCareUserControl Parent { get => (DressingCareUserControl)GetValue(DependencyParent); set => SetValue(DependencyParent, value); }
    //public bool IsDrug { get => (bool)GetValue(DependencyIsDrug); set => SetValue(DependencyIsDrug, value); }

    public CareRegisterUserControl()
    {
        InitializeComponent();
    }
    public CareRegisterUserControl(bool IsDrug ):this()
    {
        if (IsDrug)
            DataList = DressingRepository.GetAll().Where(x => x.IsDrug).ToList();
        else
            DataList = DressingRepository.GetAll().Where(x => !x.IsDrug).ToList();
        
        cmbCares.ItemsSource = DataList;
        cmbCares.DisplayMemberPath = "DressingName";
        cmbCares.SelectedValuePath = "Id";
    }


    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (cmbCares.SelectedIndex == -1)
        {
            MessageBox.Show("لطفا نام خدمت را انتخاب کنید");
            return;
        }
        if (string.IsNullOrEmpty(txtPrice.Text))
        {
            MessageBox.Show("لطفا قیمت خدمت را وارد کنید");
            return;
        }
        if (string.IsNullOrEmpty(txtCount.Text))
        {
            MessageBox.Show("لطفا تعداد خدمات انجام شده را وارد کنید");
            return;
        }
        Dressing dressing = new Dressing()
        {
            DressingName = cmbCares.DisplayMemberPath,
            Price = int.Parse(txtPrice.Text),
            HasConstPrice = true,
            IsDrug = false
        };
        DataList.Add(dressing);
        
    }

    private void btnAddList_Click(object sender, RoutedEventArgs e)
    {

    }

    private void cmbCares_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((cmbCares.SelectedItem as Dressing).HasConstPrice)
        {
            txtPrice.IsEnabled = false;
            txtPrice.Text = (cmbCares.SelectedItem as Dressing).Price.ToString();
        }
        else
        {
            txtPrice.IsEnabled = true;
            txtPrice.Text = string.Empty;
        }
    }
}