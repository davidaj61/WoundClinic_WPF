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
        public winUser()
        {
            InitializeComponent();
            Instance = this;
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
                    txtFirstName.Text=person.FirstName;
                    txtLastName.Text = person.LastName;
                    cmbGender.SelectedIndex = person.Gender?1:0;
                }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
