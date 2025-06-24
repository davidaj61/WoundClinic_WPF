using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WoundClinic_WPF.Validations;


namespace WoundClinic_WPF.Models;
public class WoundCare
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public long PatientId { get; set; }

    public long UserId { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public Patient Patient { get; set; }

    public string StringDate => Date.ToPersianDate();

    public ApplicationUser ApplicationUser { get; set; }

    public ICollection<DressingCare> DressingCares { get; set; }
    


}
