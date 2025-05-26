using System.ComponentModel.DataAnnotations;
using WoundClinic.Validations;
using WoundClinic.Models;

namespace WoundClinic.Models.ViewModels
{
    public class SearchedPatientViewModel
    {
        public string NationalCodeString {  get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MobileNumberString { get; set; }
        public string Address { get; set; }
    }
}
