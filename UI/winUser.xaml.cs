using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Validations;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for winUser.xaml
    /// </summary>
    public partial class winUser : HandyControl.Controls.Window
    {
        private static winUser Instance;
        private ApplicationUser _user = new();
        public winUser()
        {
            InitializeComponent();
            Instance = this;
        }
        public winUser(ApplicationUser user) : this()
        {
            _user = user;
            LoadUser();
        }

        private void LoadUser()
        {
            if (Environment.StackTrace.Contains("Password"))
            {
                txtFirstName.Visibility = Visibility.Collapsed;
                txtLastName.Visibility = Visibility.Collapsed;
                txtNationalCode.Visibility = Visibility.Collapsed;
                cmbGender.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtFirstName.Text = _user.Person.FirstName;
                txtLastName.Text = _user.Person.LastName;
                txtNationalCode.Text = _user.NationalCode.ToString("D10");
                cmbGender.SelectedIndex = _user.Person.Gender ? 1 : 0;
                txtPassword.Visibility = Visibility.Collapsed;
                txtConfirmPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
            var info = new GrowlInfo
            {
                Message = "ثبت کاربر را لغو کردید",
                FlowDirection = FlowDirection.RightToLeft,
                WaitTime = 3,
                ShowCloseButton = true,
                Icon = Application.Current.Resources["InfoIcon"] as Geometry,
                ShowPersianDateTime = true,
            };
            Growl.Info(info);
        }

        private void txtNationalCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNationalCode.Text.HasValue() && txtNationalCode.Text.IsIranNationalCode())
                if (PersonRepository.CheckPersonExistWithReturnPerson(long.Parse(txtNationalCode.Text), out Person person))
                {
                    txtNationalCode.Text = person.NationalCodeString;
                    txtFirstName.Text = person.FirstName;
                    txtLastName.Text = person.LastName;
                    cmbGender.SelectedIndex = person.Gender ? 1 : 0;
                }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Environment.StackTrace.Contains("Password"))
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                    _user.PasswordHash = string.Empty; // Password is not required in this case
                {
                    Growl.ErrorGlobal(new GrowlInfo
                    {
                        Message = "رمز عبور و تکرار رمز عبور یکسان نیستند",
                        FlowDirection = FlowDirection.RightToLeft,
                        WaitTime = 3,
                        ShowCloseButton = true,
                        Icon = Application.Current.Resources["ErrorIcon"] as Geometry,
                        ShowPersianDateTime = true,
                    });
                    return;
                }

                _user.PasswordHash = txtPassword.Text;
                ApplicationUserRepository.ChangePassword(_user, _user.PasswordHash);
            }
            if (Environment.StackTrace.Contains("Edit"))
            {
                _user.NationalCode = long.Parse(txtNationalCode.Text);
                _user.Person.FirstName = txtFirstName.Text;
                _user.Person.LastName = txtLastName.Text;
                _user.Person.Gender = cmbGender.SelectedIndex == 1;

                ApplicationUserRepository.Edit(_user);
            }


            _user.NationalCode = long.Parse(txtNationalCode.Text);
            _user.Person.FirstName = txtFirstName.Text;
            _user.Person.LastName = txtLastName.Text;
            _user.Person.Gender = cmbGender.SelectedIndex == 1;
            if (txtPassword.Text != txtConfirmPassword.Text)
                _user.PasswordHash = string.Empty; // Password is not required in this case
            {
                Growl.ErrorGlobal(new GrowlInfo
                {
                    Message = "رمز عبور و تکرار رمز عبور یکسان نیستند",
                    FlowDirection = FlowDirection.RightToLeft,
                    WaitTime = 3,
                    ShowCloseButton = true,
                    Icon = Application.Current.Resources["ErrorIcon"] as Geometry,
                    ShowPersianDateTime = true,
                });
                return;
            }
            _user.PasswordHash = txtPassword.Text;

            ApplicationUserRepository.Add(_user, _user.PasswordHash);

        }
    }
}
