using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoundClinic_WPF.Models;

public class ApplicationRole 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
   
    [Required]
    public string RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public ICollection<ApplicationUser> Users { get; set; }
}