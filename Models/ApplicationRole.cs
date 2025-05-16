

using System.ComponentModel.DataAnnotations;

namespace WoundClinic_WPF.Models;

public class ApplicationRole 
{
    [Key]
    public int Id { get; set; }
   
    [Required]
    public string RoleName { get; set; }

    public string? RoleDescription { get; set; }
}