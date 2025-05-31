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
        public static MainWindow Instance { get; private set; }
        private ObservableCollection<HandyControl.Controls.TabItem> _tabs=new ObservableCollection<HandyControl.Controls.TabItem>();

        public string LoginUserName
        {
            get { return CurrentUser.User?.Person?.FullName ?? "ناشناس"; }
            set { /* Property setter logic, if needed */ }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            tabMain.ItemsSource = _tabs;
            Instance = this;
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            new winPatient(this).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new winPatientSearch().ShowDialog();
        }
        public void PatientAdmission(Patient patient)
        {
            var tab = new HandyControl.Controls.TabItem
            {
                Header = patient.Person.FullName,
                Tag=patient.NationalCode,
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
                tab.Content = new DressingCareUserControl(patient,tab);
                _tabs.Add(tab);
            }
            tabMain.SelectedItem = tab;
        }

        public void CloseTab(HandyControl.Controls.TabItem tab)
        {
            var a = Environment.StackTrace; //.Contains(nameof(btnSave_Click));
            _tabs.Remove(tab);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(tabMain.Items.Count > 0)
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
    }
}
