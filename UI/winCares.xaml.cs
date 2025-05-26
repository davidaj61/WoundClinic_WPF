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
using System.Xml.Linq;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;

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
            
            dressings = DressingRepository.GetAll().ToList();
            if(dressings.Count==0)
                dressings.Add(new Dressing());
            
        }

        public void LoadDressing(Dressing dressing)
        {
            _editingDressing = dressing;
            txtName.Text = dressing.DressingName;
            cbxHasPrice.IsChecked = dressing.HasConstPrice;
            txtPrice.Text = dressing.Price.ToString();
            cbxIsDrug.IsChecked = dressing.IsDrug;
            cbxIsActive.IsChecked = dressing.IsActive;
        }
        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            // اعتبارسنجی ساده
            if (string.IsNullOrWhiteSpace(txtName.Text) || !int.TryParse(txtPrice.Text, out int price))
            {
                MessageBox.Show("اطلاعات را به درستی وارد کنید.");
                return;
            }

            if (_editingDressing == null)
                _editingDressing = new Dressing();

            _editingDressing.DressingName = txtName.Text;
            _editingDressing.HasConstPrice = cbxHasPrice.IsChecked==true;
            _editingDressing.Price = price;
            _editingDressing.IsDrug = cbxIsDrug.IsChecked==true;
            _editingDressing.IsActive = cbxIsActive.IsChecked==true;

            if (_editingDressing.Id == 0)
                DressingRepository.Create(_editingDressing);
            else
                DressingRepository.Update(_editingDressing);
            dressings.Add(_editingDressing); 
            dgvDressing.Items.Refresh();


        }

    }
}
