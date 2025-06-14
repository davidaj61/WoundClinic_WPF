﻿using System;
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
using WoundClinic_WPF.UI.UserControls;
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
        private Person _person = new Person();
        private ApplicationRole _role = new ApplicationRole();
        public winUser()
        {
            InitializeComponent();
            Instance = this;
            cmbUserType.ItemsSource = ApplicationRoleRepository.GetAll();
            cmbUserType.DisplayMemberPath = nameof(ApplicationRole.RoleDescription);
            cmbUserType.SelectedValuePath = nameof(ApplicationRole.Id);
        }
        public winUser(ApplicationUser user) : this()
        {
            using var db = new ApplicationDbContext();
            _user = db.ApplicationUsers.FirstOrDefault(u => u.NationalCode == user.NationalCode);
            if (_user != null)
            {
                db.Entry(_user).Reference(u => u.Person).Load();
                db.Entry(_user).Reference(u => u.Role).Load();
                _person = _user.Person;
                _role = _user.Role;
            }
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
                txtNationalCode.IsReadOnly = true;
                txtFirstName.Text = _user.Person.FirstName;
                txtLastName.Text = _user.Person.LastName;
                txtNationalCode.Text = _user.NationalCode.ToString("D10");
                cmbGender.SelectedIndex = _user.Person.Gender ? 1 : 0;
                cmbUserType.SelectedItem = (cmbUserType.ItemsSource as List<ApplicationRole>).First(x => x.Id == _user.RoleId);
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
                if (PersonRepository.CheckPersonExistWithReturnPerson(long.Parse(txtNationalCode.Text), out _person))
                {
                    txtNationalCode.Text = _person.NationalCodeString;
                    txtFirstName.Text = _person.FirstName;
                    txtLastName.Text = _person.LastName;
                    cmbGender.SelectedIndex = _person.Gender ? 1 : 0;
                }
        }

        bool CheckPassword()
        {
            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                _user.PasswordHash = string.Empty; // Password is not required in this case
                Growl.Error(new GrowlInfo
                {
                    Message = "رمز عبور و تکرار رمز عبور یکسان نیستند",
                    FlowDirection = FlowDirection.RightToLeft,
                    WaitTime = 3,
                    ShowCloseButton = true,
                    Icon = Application.Current.Resources["ErrorIcon"] as Geometry,
                    ShowPersianDateTime = true,
                });
                return false;
            }
            return true;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Environment.StackTrace.Contains("Password") && CheckPassword())
            {
                _user.PasswordHash = txtPassword.Password;
                ApplicationUserRepository.ChangePassword(_user, _user.PasswordHash);
                Growl.SuccessGlobal(new GrowlInfo
                {
                    Message = "رمز ورود کاربر با موفقیت ویرایش شد",
                    FlowDirection = FlowDirection.RightToLeft,
                    WaitTime = 3,
                    ShowCloseButton = true,
                    Icon = Application.Current.Resources["SuccessIcon"] as Geometry,
                    ShowPersianDateTime = true,
                });
            }
            else if (Environment.StackTrace.Contains("Edit"))
            {
                _person.NationalCodeString = txtNationalCode.Text;
                _person.FirstName = txtFirstName.Text;
                _person.LastName = txtLastName.Text;
                _person.Gender = cmbGender.SelectedIndex == 1;
                PersonRepository.Update(_person);
                ApplicationUserRepository.Edit(new ApplicationUser { RoleId=(int)cmbUserType.SelectedValue,NationalCode=_person.NationalCode});
                Growl.SuccessGlobal(new GrowlInfo
                {
                    Message = "کاربر با موفقیت ویرایش شد",
                    FlowDirection = FlowDirection.RightToLeft,
                    WaitTime = 3,
                    ShowCloseButton = true,
                    Icon = Application.Current.Resources["SuccessIcon"] as Geometry,
                    ShowPersianDateTime = true,
                });
                Close();
            }
            else if (_person.NationalCode == 0 && CheckPassword())
            {
                _person.NationalCode = long.Parse(txtNationalCode.Text);
                _person.FirstName = txtFirstName.Text;
                _person.LastName = txtLastName.Text;
                _person.Gender = cmbGender.SelectedIndex == 1;
                _user.RoleId = (int)cmbUserType.SelectedValue;
                if (txtNationalCode.Text.IsIranNationalCode() && PersonRepository.Create(_person) != null)
                {
                    _user.NationalCode = _person.NationalCode;
                    _user.PasswordHash = txtPassword.Password;
                    ApplicationUserRepository.Add(_user, txtPassword.Password);
                }
            }
            else
            {
                _user.NationalCode = _person.NationalCode;
                _user.PasswordHash = txtPassword.Password;
                ApplicationUserRepository.Add(_user, txtPassword.Password);
            }

            Growl.SuccessGlobal(new GrowlInfo
            {
                Message = "کاربر با موفقیت ثبت شد",
                FlowDirection = FlowDirection.RightToLeft,
                WaitTime = 3,
                ShowCloseButton = true,
                Icon = Application.Current.Resources["SuccessIcon"] as Geometry,
                ShowPersianDateTime = true,
            });
            ucUsers.Instance.DgvLoad();
            Close();
        }

        public static void Edit(ApplicationUser user)
        {
            
        }
    }
}
