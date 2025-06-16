using System.Windows;
using System.Windows.Media;
using HandyControl.Controls;
using HandyControl.Data;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.Shared;
using MessageBox = HandyControl.Controls.MessageBox;

namespace WoundClinic_WPF.UI;

/// <summary>
/// Interaction logic for winPatient.xaml
/// </summary>
public partial class winPatient : HandyControl.Controls.Window
{
    private Patient _patient;
    private Person _person;
    private MainWindow _mainWindow;
    public winPatient()
    {
        InitializeComponent();
        cmbGender.SelectedIndex = 0;

    }
    public winPatient(MainWindow minWin) : this()
    {
        _mainWindow = minWin;
    }

    // برای ویرایش بیمار
    public void LoadPatient(Patient patient)
    {
        _patient = patient;
        if (patient.Person != null)
        {
            txtNationalCode.Text = patient.Person.NationalCode.ToString();
            txtFirstName.Text = patient.Person.FirstName;
            txtLastName.Text = patient.Person.LastName;
            cmbGender.SelectedIndex = patient.Person.Gender ? 0 : 1;
            txtNationalCode.IsEnabled = false; // کد ملی قابل ویرایش نباشد
        }
        txtMobile.Text = patient.MobileNumber.ToString();
        txtAddress.Text = patient.Address;
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!long.TryParse(txtNationalCode.Text, out long nationalCode) ||
            string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            !long.TryParse(txtMobile.Text, out long mobile))
        {
            MessageBox.Show("اطلاعات را به درستی وارد کنید.");
            return;
        }
        if (_person.NationalCode == 0)
        {

            _person.FirstName = txtFirstName.Text;
            _person.LastName = txtLastName.Text;
            _person.Gender = cmbGender.SelectedIndex == 0 ? false : true;
            _person.IsAtba = (bool)rbtAtba.IsChecked;
        }


        _patient = _patient ?? new Patient();


        _patient.MobileNumber = mobile;
        _patient.Address = txtAddress.Text;
        _patient.UserId = CurrentUser.User.NationalCode;

        // ذخیره Person و Patient
        if (_person.NationalCode == 0)
        {
            _person.NationalCode = nationalCode;
            PersonRepository.Create(_person);
        }
        else
            PersonRepository.Update(_person);
        if (_patient.NationalCode == 0)
        {
            _patient.NationalCode = nationalCode;
            PatientRepository.Create(_patient);
            _patient.Person = _person;
        }
        else
            PatientRepository.Update(_patient);

        var result = MessageBox.Show("آیا میخواهید بیمار را پذیرش نمایید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.No)
            this.Close();
        else
        {
            _mainWindow.PatientAdmission(_patient);
            this.Close();
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
        var info=new GrowlInfo{
            Message="ثبت بیمار را لغو کردید",
            FlowDirection=FlowDirection.RightToLeft,
            WaitTime=3,
            ShowCloseButton = true,
            Icon=Application.Current.Resources["InfoIcon"] as Geometry,
            ShowPersianDateTime = true,
        };
        Growl.Info(info);
    }

    private void Atba_Checked(object sender, RoutedEventArgs e)
    {
        txtNationalCode.IsEnabled = false;
        try
        {
            txtNationalCode.Text = PersonRepository.GetCodeForNewAtba().ToString();
            _person = new Person();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void txtNationalCode_LostFocus(object sender, RoutedEventArgs e)
    {

        if (txtNationalCode.Text == null || !txtNationalCode.IsEnabled || txtNationalCode.Text.Length != 10 || !long.TryParse(txtNationalCode.Text, out long nationaNumber))
        {
            txtNationalCode.Text = "";
            return;
        }
        else

        if (!PersonRepository.PersonIsPatient(nationaNumber, out _person) && _person.NationalCode == 0)
            txtFirstName.Focus();
        else if (!PersonRepository.PersonIsPatient(nationaNumber, out _person) && _person.NationalCode != 0)
        {
            txtFirstName.Text = _person.FirstName;
            txtLastName.Text = _person.LastName;
            cmbGender.SelectedIndex = _person.Gender ? 1 : 0;

        }
        else
        {
            _patient = _person.Patient;

            txtFirstName.Text = _person.FirstName;
            txtLastName.Text = _person.LastName;
            cmbGender.SelectedIndex = _person.Gender ? 1 : 0;
            txtMobile.Text = _patient.MobileNumberString;
            txtAddress.Text = _patient.Address;

            var result = MessageBox.Show("این بیمار قبلا ثبت شده است. آیا میخواهید بیمار را پذیرش نمایید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                _mainWindow.PatientAdmission(_patient);
                this.Close();
            }
        }
    }

    private void rbtIranian_Checked(object sender, RoutedEventArgs e)
    {
        if (txtNationalCode != null)
        {
            txtNationalCode.Text = "";
            txtNationalCode.IsEnabled = true;
            txtNationalCode.Focus();
        }
    }
}
