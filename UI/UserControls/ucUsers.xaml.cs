using System.Windows;
using System.Windows.Controls;
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
        public static ucUsers Instance;
        public ucUsers()
        {
            InitializeComponent();

            Instance = this;
        }
        public void DgvLoad()
        {
            users = ApplicationUserRepository.GetAllUsers();
            dgvSearch.ItemsSource = users;
            dgvSearch.Items.Refresh();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)=>DgvLoad();

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            dgvSearch.ItemsSource = users.Where(x => x.Person.FullName.Contains(txtSearch.Text) || x.NationalCode.ToString().Contains(txtSearch.Text)).ToList();
            dgvSearch.Items.Refresh();
        }
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            new winUser().ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            new winUser(dgvSearch.SelectedItem as ApplicationUser).ShowDialog();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            new winUser(dgvSearch.SelectedItem as ApplicationUser).ShowDialog();
        }

        private void ChangeActivate_Click(object sender, RoutedEventArgs e)
        {
            ApplicationUserRepository.ChangeUserActivate(dgvSearch.SelectedItem as ApplicationUser);
            dgvSearch.Items.Refresh();
        }
    }
}
