using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WoundClinic_WPF.Models;

public class Dressing
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }

    [DisplayName("نام پانسمان")]
    [Column(TypeName = "nvarchar(50)")]
    public string DressingName { get; set; }

    [DisplayName("قیمت ثابت دارد")]
    public bool HasConstPrice { get; set; }

    [DisplayName("قیمت")]
    public int Price { get; set; } = 0;

    public bool IsDrug { get; set; } = false;

    public ICollection<DressingCare> DressingCares { get; set; }
}

