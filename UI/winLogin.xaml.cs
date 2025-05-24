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
using WoundClinic_WPF.Services.IRepository;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for winLogin.xaml
    /// </summary>
    public partial class winLogin : Window
    {
        private readonly IApplicationUserRepository _Repos;
        public winLogin(IApplicationUserRepository Repo)
        {
            InitializeComponent();
            _Repos = Repo;
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Password) ||!long.TryParse(txtUserName.Text,out long nationalNumber))
            {
                MessageBox.Show("نام کاربری یا رمز عبور را وارد کنید.");
                return;
            }
            var user = _Repos.GetByNationalCode(nationalNumber);
            if (user == null || !_Repos.CheckPassword(user,txtPassword.Password))
            {
                MessageBox.Show("نام کاربری یا رمز عبور نادرست است.");
                return;
            }
            // ورود موفق
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
