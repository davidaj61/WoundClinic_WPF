using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WoundClinic.Validations;


namespace WoundClinic_WPF.Models;

[PrimaryKey(nameof(NationalCode))]  
public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long NationalCode { get; set; }

    [NotMapped]
    public string NationalCodeString 
    {
        get=>NationalCode.ToString(); 
        set=>NationalCode=long.TryParse(value,out var res)?res:0; 
    }

    [Required]
    [Column(TypeName = "nvarchar(25)")]
    [Display(Name = "نام")]
    [Length(2, 25, ErrorMessage = "نام باید بین 3 تا 25 حرف باشد")]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "فقط حروف فارسی مجاز است.")]
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "نام خانوادگی")]
    [Length(2, 50, ErrorMessage = "نام خانوادگی باید بین 3 تا 50 حرف باشد")]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "فقط حروف فارسی مجاز است.")]
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "bit")]
    [Display(Name = "جنسیت")]
    public bool Gender { get; set; }

    public Patient? Patient { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }

    [NotMapped]
    public string FullName => FirstName + " " + LastName;

}
