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

    public winPatient()
    {
        InitializeComponent();
        
        cmbGender.SelectedIndex = 0;
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

        MessageBox.Show("ذخیره شد.");
        this.DialogResult = true;
        this.Close();
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
        if (txtNationalCode.Text==null )
        {

        }
    }
}
