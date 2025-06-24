using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundClinic_WPF.Models.ViewModels.Report
{
    public class PatientReportViewModel
    {
        public string FullName { get; set; }
        public string NationalCodeString { get; set; }
        public string MobileNumberString { get; set; }
        public string AdmissionDate { get; set; }
    }
}
