using System.Windows;
using System.Windows.Controls;
using WoundClinic.Models.ViewModels;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.Shared;
using WoundClinic_WPF.UI.UserControls;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : HandyControl.Controls.Window
    {
        public static MainWindow Instance { get; private set; }
        private List<HandyControl.Controls.TabItem> _tabs = new();
        private List<SearchedPatientViewModel> searchedPatientViewModels = new();

        public string LoginUserName
        {
            get { return CurrentUser.User?.Person?.FullName ?? "ناشناس"; }
            set { /* Property setter logic, if needed */ }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            if (tabMain.Items.OfType<HandyControl.Controls.TabItem>().Any())
                _tabs = tabMain.Items.OfType<HandyControl.Controls.TabItem>().ToList();
            else
                tabMain.ItemsSource = _tabs;

            Instance = this;
            dgvSearch.ItemsSource = searchedPatientViewModels;
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            new winPatient(this).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void PatientAdmission(Patient patient)
        {
            var tab = new HandyControl.Controls.TabItem
            {
                Header = patient.Person.FullName,
                Tag = patient.NationalCode,
            };
            // بررسی وجود تب با نام بیمار
            if (tabMain.Items.OfType<HandyControl.Controls.TabItem>().Any(x => x.Tag.ToString() == patient.Person.NationalCode.ToString()))
            {
                MessageBox.Show("تب بیمار با این نام قبلا باز شده است.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                tabMain.Items.OfType<HandyControl.Controls.TabItem>().FirstOrDefault(x => x.Header.ToString() == patient.Person.FullName).IsSelected = true;
                return;
            }
            else
            {
                tab.Content = new DressingCareUserControl(patient, tab);
                tabMain.Items.Add(tab);
                tabMain.Items.Refresh();
            }
            tabMain.SelectedItem = tab;
        }

        public void CloseTab(HandyControl.Controls.TabItem tab)
        {
            var a = Environment.StackTrace; //.Contains(nameof(btnSave_Click));
            tabMain.Items.Remove(tab);
            tabMain.Items.Refresh();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tabMain.Items.Count > 1)
            {
                var result = HandyControl.Controls.MessageBox.Show("آیا مطمئن هستید که می‌خواهید برنامه را ببندید؟", "خروج", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true; // جلوگیری از بسته شدن پنجره
                    return;
                }
                Application.Current.Shutdown(); // پایان برنامه 
            }
            Application.Current.Shutdown(); // پایان برنامه 
        }

        private void btnAddCare_Click(object sender, RoutedEventArgs e)
        {
            new winCares().ShowDialog();
        }

        //متدهای مربوط به تب جستجوی بیمار

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Length == 3)
            {
                searchedPatientViewModels = PatientRepository.SearchPatients(txtSearch.Text);
                dgvSearch.ItemsSource = searchedPatientViewModels;
            }
            else if (txtSearch.Text.Length < 3)
            {
                searchedPatientViewModels.Clear();
                dgvSearch.Items.Refresh();
            }
            else
            {
                dgvSearch.ItemsSource = searchedPatientViewModels.Where(x => x.FullName.Contains(txtSearch.Text) || x.MobileNumberString.Contains(txtSearch.Text) || x.NationalCodeString.Contains(txtSearch.Text));
                dgvSearch.Items.Refresh();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.PatientAdmission(new SearchedPatientViewModel().ToPatientModel(long.Parse(searchedPatientViewModels[dgvSearch.SelectedIndex].NationalCodeString)));

        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            if (tabMain.Items.OfType<HandyControl.Controls.TabItem>().FirstOrDefault(x => x.Tag == "users") == null)
                tabMain.Items.Add(new HandyControl.Controls.TabItem { Content = new ucUsers(), Header = "مدیریت کاربران", Tag = "users",IsSelected=true });
            else
                tabMain.Items.OfType<HandyControl.Controls.TabItem>().FirstOrDefault(x => x.Tag == "users").IsSelected=true;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            tabMain.Items.Add(new HandyControl.Controls.TabItem { Content = new ucReport(), Header = "گزارش مراجعات", Tag = "rptMonth", IsSelected = true });
        }
    }
}
