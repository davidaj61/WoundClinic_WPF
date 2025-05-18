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
    
    public string? Name => Person.FullName;

    [NotMapped]
    public string UserRoles => Roles != null ? string.Join(',', Roles.Select(r => r.RoleName)): string.Empty;

    public string? AuthenticationType => "User Authentication";

    public bool IsAuthenticated => !string.IsNullOrEmpty(Name);

    public bool IsActive { get; set; }

    public Person Person { get; set; }

    public ICollection<ApplicationRole> Roles { get; set; }

    public ICollection<Patient> Patients { get; set; }

    public ICollection<WoundCare> WoundCares { get; set; }

}

