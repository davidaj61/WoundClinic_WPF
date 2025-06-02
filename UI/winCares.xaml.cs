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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Validations;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for winCares.xaml
    /// </summary>
    public partial class winCares : Window
    {
        private Dressing _editingDressing;
        private List<Dressing> dressings;
        public winCares()
        {
            InitializeComponent();
            LoadDataGrid();
        }
        private void FormReset()
        {
            txtName.Text = "";
            cbxIsDrug.IsChecked = false;
            cbxIsActive.IsChecked = true;
            btnAddList.Visibility = Visibility.Visible;
            btnEditList.Visibility = Visibility.Collapsed;
        }
        private void LoadDataGrid()
        {
            dressings = DressingRepository.GetAll().ToList();
            if (dressings.Count == 0)
                dressings.Add(new Dressing());
            dgvDressing.ItemsSource = dressings;
        }

        public void LoadDressing(Dressing dressing)
        {
            _editingDressing = dressing;
            txtName.Text = dressing.DressingName;
            cbxIsDrug.IsChecked = dressing.IsDrug;
            cbxIsActive.IsChecked = dressing.IsActive;
        }
        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            // اعتبارسنجی ساده
            if (!txtName.Text.HasValue())
            {
                MessageBox.Show("اطلاعات را به درستی وارد کنید.");
                return;
            }

            if (_editingDressing == null)
                _editingDressing = new Dressing();
            _editingDressing.DressingName = txtName.Text;
            _editingDressing.IsDrug = cbxIsDrug.IsChecked == true;
            _editingDressing.IsActive = cbxIsActive.IsChecked == true;

            if (_editingDressing.Id == 0)
            {
                DressingRepository.Create(_editingDressing);
                FormReset();
                dressings.Add(_editingDressing);
                dgvDressing.Items.Refresh();
            }
            else
            {
                DressingRepository.Update(_editingDressing);
                FormReset();
                dgvDressing.Items.Refresh();
            }
            // اضافه کردن پانسمان به لیست
            
            dgvDressing.Items.Refresh();
            _editingDressing = null;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgvDressing.SelectedItem is Dressing selectedDressing)
            {
                LoadDressing(selectedDressing);
                btnAddList.Visibility = Visibility.Collapsed;
                btnEditList.Visibility = Visibility.Visible;
            }
        }
    }
}
