using System.ComponentModel.DataAnnotations;
using WoundClinic.Validations;
using WoundClinic.Models;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services;

namespace WoundClinic.Models.ViewModels
{
    public class SearchedPatientViewModel
    {
        public long NationalCode { get; set; }
        public string NationalCodeString {  get; set; }
        public string FullName { get; set; } = string.Empty;
        public string MobileNumberString { get; set; }

        internal Patient ToPatientModel(long nationalCode)
        {
            return PatientRepository.Get(nationalCode);
        }
    }
}
