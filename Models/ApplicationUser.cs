using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Security.Principal;

namespace WoundClinic_WPF.Models;
// Add profile data for application users by adding properties to the ApplicationUser class
[PrimaryKey(nameof(NationalCode))]
public class ApplicationUser :IIdentity
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long NationalCode { get; set; }

    public string PasswordHash { get; set; }

    public string? Name => Person.FullName;

    public string? AuthenticationType => "User Authentication";

    public bool IsAuthenticated => !string.IsNullOrEmpty(Name);

    public bool IsActive { get; set; }

    public int RoleId { get; set; }

    public DateTime LastLogin { get; set; }

    public Person Person { get; set; }

    public ApplicationRole Role { get; set; }

    public ICollection<Patient> Patients { get; set; }

    public ICollection<WoundCare> WoundCares { get; set; }

}

