using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WoundClinic_WPF.Models;

public class Dressing
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DisplayName("نام پانسمان")]
    [Column(TypeName = "nvarchar(50)")]
    public string DressingName { get; set; }

    public bool IsDrug { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public ICollection<DressingCare> DressingCares { get; set; }
}

