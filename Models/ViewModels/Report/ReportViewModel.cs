using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundClinic_WPF.Models.ViewModels.Report
{
    public class ReportViewModel
    {
        public string NationalCode { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Date { get; set; }
        public string Care { get; set; }
        public int Payment { get; set; }
    }
}
