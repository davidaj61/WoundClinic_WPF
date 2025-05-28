using System.Windows;
using System.Windows.Controls;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.Shared;
using MessageBox = HandyControl.Controls.MessageBox;

namespace WoundClinic_WPF.UI;

/// <summary>
/// Interaction logic for winPatient.xaml
/// </summary>
public partial class winPatient : Window
{
    private Patient _editingPatient;
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
        _editingPatient = patient;
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
        bool gender = ((cmbGender.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "true") == "true";
        Person person = _editingPatient?.Person ?? new Person();
        person.NationalCode = nationalCode;
        person.FirstName = txtFirstName.Text;
        person.LastName = txtLastName.Text;
        person.Gender = gender;
        person.IsAtba = (bool)rbtAtba.IsChecked;
        if (_editingPatient == null)
            _editingPatient = new Patient();
        _editingPatient.NationalCode = nationalCode;
        _editingPatient.MobileNumber = mobile;
        _editingPatient.Address = txtAddress.Text;
        _editingPatient.Person = person;
        _editingPatient.UserId = CurrentUser.User.NationalCode;

        // ذخیره Person و Patient
        if (_editingPatient.Person.Patient == null)
            PersonRepository.Create(person);
        else
            PersonRepository.Update(person);
        if (_editingPatient.Person.Patient == null)
            PatientRepository.Create(_editingPatient);
        else
            PatientRepository.Update(_editingPatient);
        var result = MessageBox.Show("آیا میخواهید بیمار را پذیرش نمایید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.No)
            this.Close();
        else
        {
            _mainWindow.PatientAdmission(_editingPatient);
            this.Close();
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void Atba_Checked(object sender, RoutedEventArgs e)
    {
        txtNationalCode.IsEnabled = false;
        try
        {
            txtNationalCode.Text = PersonRepository.GetCodeForNewAtba().ToString();
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
            _editingPatient = PatientRepository.Get(nationaNumber);
        if (_editingPatient.Person == null)
            txtFirstName.Focus();
        else
        {
            txtFirstName.Text = _editingPatient.Person.FirstName;
            txtLastName.Text = _editingPatient.Person.LastName;
            txtMobile.Text = _editingPatient.MobileNumberString;
            txtAddress.Text = _editingPatient.Address;

            var result = MessageBox.Show("این بیمار قبلا ثبت شده است. آیا میخواهید بیمار را پذیرش نمایید؟", "توجه", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                _mainWindow.PatientAdmission(_editingPatient);
                this.Close();
            }
        }
    }
}
