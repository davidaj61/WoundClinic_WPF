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
using WoundClinic_WPF.Services.IRepository;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for winCares.xaml
    /// </summary>
    public partial class winCares : Window
    {
        private readonly IDressingRepository _repo;
        private Dressing _editingDressing;
        public winCares(IDressingRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        public void LoadDressing(Dressing dressing)
        {
            _editingDressing = dressing;
            txtName.Text = dressing.DressingName;
            chkConstPrice.IsChecked = dressing.HasConstPrice;
            txtPrice.Text = dressing.Price.ToString();
            chkIsDrug.IsChecked = dressing.IsDrug;
        }
    }
}
