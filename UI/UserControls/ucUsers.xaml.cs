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
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;

namespace WoundClinic_WPF.UI.UserControls
{
    /// <summary>
    /// Interaction logic for ucUsers.xaml
    /// </summary>
    public partial class ucUsers : UserControl
    {
        private List<ApplicationUser> users = new List<ApplicationUser>();
        public ucUsers()
        {
            InitializeComponent();
            users=ApplicationUserRepository.GetAllUsers();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgvSearch.ItemsSource = users;
        }

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            dgvSearch.ItemsSource = users.Where(x => x.Person.FullName.Contains(txtSearch.Text) || x.NationalCode.ToString().Contains(txtSearch.Text)).ToList();
            dgvSearch.Items.Refresh();
        }
    }
}
