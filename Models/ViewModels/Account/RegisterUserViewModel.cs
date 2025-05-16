using System.ComponentModel.DataAnnotations;
using WoundClinic.Validations;

namespace WoundClinic.Models.ViewModels;

public sealed class RegisterUserViewModel
{
    [Required(ErrorMessage = "ورود کد ملی الزامیست")]
    [Display(Name = "کد ملی")]
    [Length(10, 10, ErrorMessage = "تعداد ارقام کد ملی صحیح نمی باشد.")]
    [NationalCode(ErrorMessage = "کد ملی صحیح نمی باشد.")]
    public string PersonNationalCode { get; set; }

    [Required(ErrorMessage = "نام نمی تواند خالی باشد.")]
    [Display(Name = "نام")]
    [Length(2, 50, ErrorMessage = "نام باید بین 3 تا 50 کاراکتر باشد")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "نام خانوادگی نمی تواند خالی باشد.")]
    [Display(Name = "نام خانوادگی")]
    [Length(2, 50, ErrorMessage = "نام خانوادگی باید بین 3 تا 50 کاراکتر باشد")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "جنسیت را مشخص نمایید.")]
    [Display(Name = "جنسیت")]
    public bool Gender { get; set; } = true;

    [Required(ErrorMessage = "رمز ورود نمی تواند خالی باشد.")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "رمز ورود")]
    public string Password { get; set; }

    [Required(ErrorMessage = "تکرار رمز ورود نمی تواند خالی باشد.")]
    [DataType(DataType.Password)]
    [Display(Name = "تکرار رمز ورود")]
    [Compare("Password", ErrorMessage = "رمز ورود و تکرار آن مشابه نیستند.")]
    public string ConfirmPassword { get; set; }

}

