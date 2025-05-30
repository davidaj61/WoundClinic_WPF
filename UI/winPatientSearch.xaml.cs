﻿using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WoundClinic.Models.ViewModels;
using WoundClinic_WPF.Services;

namespace WoundClinic_WPF.UI
{
    /// <summary>
    /// Interaction logic for winPatientSearch.xaml
    /// </summary>
    public partial class winPatientSearch : Window
    {
        private List<SearchedPatientViewModel> searchedPatientViewModels=new ();
        
        public winPatientSearch()
        {
            InitializeComponent();
            dgvSearch.ItemsSource = searchedPatientViewModels;
             
        }


        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            if(txtSearch.Text.Length==3)
            {
                searchedPatientViewModels = PatientRepository.SearchPatients(txtSearch.Text);
                dgvSearch.ItemsSource = searchedPatientViewModels;
            }
            else if (txtSearch.Text.Length < 3)
            {
                searchedPatientViewModels.Clear();
                dgvSearch.Items.Refresh();
            }
            else 
            {
                dgvSearch.ItemsSource = searchedPatientViewModels.Where(x=>x.FullName.Contains(txtSearch.Text)|| x.MobileNumberString.Contains(txtSearch.Text)|| x.NationalCodeString.Contains(txtSearch.Text));
                dgvSearch.Items.Refresh();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.PatientAdmission(new SearchedPatientViewModel().ToPatientModel(long.Parse(searchedPatientViewModels[dgvSearch.SelectedIndex].NationalCodeString)));
            this.Close();
        }
    }
}
