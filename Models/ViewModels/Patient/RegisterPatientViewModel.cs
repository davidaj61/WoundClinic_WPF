using System.ComponentModel.DataAnnotations;
using WoundClinic.Validations;
using WoundClinic.Models;

namespace WoundClinic.Models.ViewModels
{
    public class RegisterPatientViewModel
    {
        [Required(ErrorMessage = "ورود کد ملی الزامی است")]
        [Display(Name = "کد ملی")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید 10 رقم باشد")]
        [Length(10, 10, ErrorMessage = "تعداد ارقام کد ملی صحیح نمی باشد.")]
        [NationalCode(ErrorMessage = "کد ملی صحیح نمی باشد.")]
        public string NationalCodeString {  get; set; }
        

        [Required(ErrorMessage = "ورود نام الزامی است")]
        [Display(Name = "نام")]
        [Length(2, 25, ErrorMessage = "نام باید بین 3 تا 25 حرف باشد")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "فقط حروف فارسی مجاز است.")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ورود نام خانوادگی الزامی است")]
        [Display(Name = "نام خانوادگی")]
        [Length(2, 50, ErrorMessage = "نام خانوادگی باید بین 3 تا 50 حرف باشد")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "فقط حروف فارسی مجاز است.")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "انتخاب جنسیت الزامی است")]
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }
        

        [Required(ErrorMessage = "ورود شماره تلفن همراه الزامی است.")]
        [Length(11, 11, ErrorMessage = "شماره موبایل صحیح نمی باشد")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "شماره موبایل 11 رقمی وارد کنید")]
        public string MobileNumberString { get; set; }

        public string Address { get; set; }
    }
}
