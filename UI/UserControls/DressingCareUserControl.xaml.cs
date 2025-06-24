using System.Windows;
using System.Windows.Controls;
using HandyControl.Controls;
using HandyControl.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System.Drawing;
using System.IO;
using UI.UserControls;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.Shared;
using WoundClinic_WPF.Validations;
using MessageBox = HandyControl.Controls.MessageBox;
using Models.ViewModels.Report;

namespace WoundClinic_WPF.UI.UserControls;

/// <summary>
/// Interaction logic for DressingCareUserControl1.xaml
/// </summary>
public partial class DressingCareUserControl : UserControl
{
    public static DressingCareUserControl Instance { get; private set; }
    private Patient _patient;
    private HandyControl.Controls.TabItem _tabItem;
    private WoundCare _wc;
    private DressingCare _dc;
    public Patient Patient => _patient;
    private List<DressingCare> dressingCares = new List<DressingCare>();
    public DressingCareUserControl()
    {
        InitializeComponent();
        Instance = this;
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
        MessageBoxResult res = MessageBoxResult.Yes;
        if (txtDate.Text.IsValidDate() && txtDate.Text.ToMiladyDate() <= DateTime.Now)
        {
            if (WoundCareRepository.HasAdmissionAtDate(txtDate.Text.ToMiladyDate(), _patient.NationalCode))
            {

                res = MessageBox.Show(_patient.Person.Gender ? "آقای" : "خانم" + " " + _patient.Person.FullName + "  در تاریخ" + txtDate.Text + " یک پذیرش قبلی دارد آیا میخواهید آن را مشاهده کنید", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    var (w, d) = WoundCareRepository.GetResentAdmission(_patient.NationalCode, txtDate.Text.ToMiladyDate());
                    txtDescription.Text = w.Description;
                    dressingCares = d;
                    dgvCares.Items.Refresh();
                }
                else return;
            }
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
        int p = 0;
        byte q = 1;
        if (SelectedDressing == null || !int.TryParse(txtPrice.Text, out p) || !byte.TryParse(txtCount.Text, out q))
            return;
        dressingCares.Add(new DressingCare
        {
            Dressing = SelectedDressing,
            DressingId = SelectedDressing.Id,
            Quantity = q,
            Price = p,
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
        {
            Growl.Warning(new GrowlInfo
            {
                FlowDirection = FlowDirection.RightToLeft,
                Message = "لیست پانسمان خالی است",
                ShowCloseButton = true,
                WaitTime = 3,
            });
            return;
        }

        if (WoundCareRepository.Add(_wc))
            if (dressingCares.Count == 1)
            {
                _dc = dressingCares.First();
                DressingCareRepository.Create(new DressingCare
                {
                    DressingId = _dc.DressingId,
                    WoundCareId = _wc.Id,
                    Quantity = _dc.Quantity,
                    Price = _dc.Price
                });
                MainWindow.Instance.CloseTab(_tabItem);
            }
            else
            {
                var dcList = new List<DressingCare>();
                dressingCares.ForEach(dc => dcList.Add(new DressingCare
                {
                    DressingId = dc.DressingId,
                    WoundCareId = _wc.Id,
                    Quantity = dc.Quantity,
                    Price = dc.Price
                }));
                DressingCareRepository.CreateList(dcList);
                MainWindow.Instance.CloseTab(_tabItem);
            }
        Growl.SuccessGlobal(new GrowlInfo
        {
            FlowDirection = FlowDirection.RightToLeft,
            Message = string.Format("خدمات مربوط به {0} {1} با موفقیت ثبت شد", _patient.Person.Gender ? "آقای" : "خانم", _patient.Person.FullName),
            ShowCloseButton = true,
            WaitTime = 3,
        });
        PrintAdmission(_wc, dressingCares);
        ShowAdmissionInDay(_wc.Date.Date);
    }

    private void ShowAdmissionInDay(DateTime date)
    {
        if(MainWindow.Instance.tabMain.Items.OfType<HandyControl.Controls.TabItem>()
            .Any(x => x.Header.ToString().Contains(date.ToPersianDate()))) 
            return;
        MainWindow.Instance.tabMain.Items.Add(new HandyControl.Controls.TabItem
        {
            Header = "پذیرش "+date.ToPersianDate(),
            Content = new ucAdmissionInDay(date),
        });
    }

    internal void PrintAdmission(WoundCare wc, List<DressingCare> dressingCares)
    {
        var list = new List<WoundCareViewModel>();
        list.Add(new WoundCareViewModel
        {
            FullName = _patient.Person.FullName,
            NationalCodeString = _patient.Person.NationalCodeString,
            MobileNumberString = _patient.MobileNumberString,
            StringDate = _wc.StringDate,
        });
        Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkO46nMQvol4ASeg91in+mGJLnn2KMIpg3eSXQSgaFOm15+0l" +
                                             "hekKip+wRGMwXsKpHAkTvorOFqnpF9rchcYoxHXtjNDLiDHZGTIWq6D/2q4k/eiJm9fV6FdaJIUbWGS3whFWRLPHWC" +
                                             "BsWnalqTdZlP9knjaWclfjmUKf2Ksc5btMD6pmR7ZHQfHXfdgYK7tLR1rqtxYxBzOPq3LIBvd3spkQhKb07LTZQoyQ" +
                                             "3vmRSMALmJSS6ovIS59XPS+oSm8wgvuRFqE1im111GROa7Ww3tNJTA45lkbXX+SocdwXvEZyaaq61Uc1dBg+4uFRxv" +
                                             "yRWvX5WDmJz1X0VLIbHpcIjdEDJUvVAN7Z+FW5xKsV5ySPs8aegsY9ndn4DmoZ1kWvzUaz+E1mxMbOd3tyaNnmVhPZ" +
                                             "eIBILmKJGN0BwnnI5fu6JHMM/9QR2tMO1Z4pIwae4P92gKBrt0MqhvnU1Q6kIaPPuG2XBIvAWykVeH2a9EP6064e11" +
                                             "PFCBX4gEpJ3XFD0peE5+ddZh+h495qUc1H2B";
        var report = new StiReport();
        report.Load(@"Reports\rptBill.mrt");
        report.RegBusinessObject("PatientBill",list);
        report.RegBusinessObject("Bill", dressingCares);
        report.Dictionary.Synchronize();
        report.Compile();
        report.ShowWithWpf();
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
