using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WoundClinic_WPF.Services.Shared;
using WoundClinic_WPF.Validations;
using MessageBox = HandyControl.Controls.MessageBox;

namespace WoundClinic_WPF.UI.UserControls;

/// <summary>
/// Interaction logic for DressingCareUserControl1.xaml
/// </summary>
public partial class DressingCareUserControl : UserControl
{
    private Patient _patient;
    private HandyControl.Controls.TabItem _tabItem;
    private WoundCare _wc;
    private DressingCare _dc;
    public Patient Patient => _patient;
    private List<DressingCare> dressingCares = new List<DressingCare>();

    public DressingCareUserControl()
    {
        InitializeComponent();
        this.DataContext = this;
        txtDate.Focus();
        txtDate.Text = DateTime.Now.ToPersianDate();
        dgvCares.ItemsSource = dressingCares;


    }
    public DressingCareUserControl(Patient patient, HandyControl.Controls.TabItem tabItem) : this()
    {
        _patient = patient;
        _tabItem = tabItem;

    }

    private void btnAdmission_Click(object sender, RoutedEventArgs e)
    {
        if (txtDate.Text.IsValidDate())
        {
            _wc = new WoundCare
            {
                UserId = CurrentUser.User.NationalCode,
                PatientId = _patient.NationalCode,
                Date = txtDate.Text.ToMiladyDate(),
                Description = txtDescription.Text,
                
            };

            gbxDressing.IsEnabled = true;
            var cares = DressingRepository.GetAllActive();
            cmbCares.ItemsSource = cares;
            cmbCares.DisplayMemberPath = nameof(Dressing.DressingName);
            cmbCares.SelectedValuePath = nameof(Dressing.Id);
            dressingCares.Clear();
        }
        else
        {
            MessageBox.Show("تاریخ وارد شده صحیح نمی باشد", "توجه", MessageBoxButton.OK, MessageBoxImage.Warning);
            txtDate.Clear();
            txtDate.Focus();
            return;
        }
    }

    public Dressing SelectedDressing => cmbCares.SelectedItem as Dressing;
    

    private void btnAddList_Click(object sender, RoutedEventArgs e)
    {
        dressingCares.Add(new DressingCare
        {
            Dressing=SelectedDressing,
            DressingId = (cmbCares.SelectedItem as Dressing).Id,
            Quantity = byte.TryParse(txtCount.Text, out byte count) ? count : (byte)0,
            Price = int.TryParse(txtPrice.Text.Replace(",", ""), out int price) ? price : 0,
        });

        cmbCares.SelectedIndex = -1;
        txtCount.Text = "1";
        txtPrice.Text = "0";
        dgvCares.Items.Refresh();
        txtTotalPrice.Text = dressingCares.Sum(x => x.Payment).ToString("N0");

    }

    private void btnDeleteCare_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var care = button.DataContext as DressingCare;
        if (care != null && dressingCares != null)
        {
            dressingCares.Remove(care);
            dgvCares.Items.Refresh();
        }
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {

        if (dressingCares.HasValue() || _wc.HasValue())
            if (MessageBox.Show("آیا از لغو این پذیرش اطمینان دارید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                MainWindow.Instance.CloseTab(_tabItem);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        if (dressingCares.Count == 0)
            MessageBox.Show("لیست پانسمان خالی است", "توجه", MessageBoxButton.OK, MessageBoxImage.Warning);

        else if (WoundCareRepository.Create(_wc))
        {
            dressingCares.ForEach(a => a.WoundCareId = _wc.Id);
            DressingCareRepository.CreateList(dressingCares);
            MainWindow.Instance.CloseTab(_tabItem);
        }
    }

    private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as HandyControl.Controls.TextBox;
        if (textBox.Text.HasValue())
            return;

        // حذف جداکننده‌های قبلی
        string unformatted = textBox.Text.Replace(",", "");
        if (long.TryParse(unformatted, out long value))
        {
            int selectionStart = textBox.SelectionStart;
            textBox.Text = value.ToString("N0");
            // قرار دادن مکان‌نما در انتهای متن
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}
