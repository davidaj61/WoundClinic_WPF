using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System;
using Microsoft.EntityFrameworkCore;


namespace WoundClinic_WPF.Models;

[PrimaryKey(nameof(NationalCode))]  
public class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long NationalCode { get; set; }

    [Required(ErrorMessage ="ورود شماره تلفن همراه الزامی است.")]
    public long MobileNumber { get; set; }

    [NotMapped]
    public string MobileNumberString 
    { 
        get => MobileNumber.ToString("D11"); 
        set =>MobileNumber=long.TryParse(value, out long number)?number:0;
    }

    public string? Address { get; set; }


    [Required]
    public long UserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public Person Person { get; set; }

    public ICollection<WoundCare> WoundCares { get; set; }
}
