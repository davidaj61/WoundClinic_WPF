using System.Windows;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.Shared;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for winLogin.xaml
    /// </summary>
    public partial class winLogin : Window
    {
        private ApplicationUser _user =new ApplicationUser();
        public winLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Password) ||!long.TryParse(txtUserName.Text,out long nationalNumber))
            {
                MessageBox.Show("نام کاربری یا رمز عبور را وارد کنید.");
                return;
            }
            var user = ApplicationUserRepository.GetByNationalCode(nationalNumber);
            if (user == null || !ApplicationUserRepository.CheckPassword(user,txtPassword.Password))
            {
                MessageBox.Show("نام کاربری یا رمز عبور نادرست است.");
                return;
            }
            // ورود موفق
            ApplicationUserRepository.SetUserLastLogin(user);
            _user = user;
            Close();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CurrentUser.User = _user;
            new MainWindow().Show();
        }
    }
}
