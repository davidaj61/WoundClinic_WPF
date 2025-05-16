using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WoundClinic_WPF.Models;

public class DressingCare
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int WoundCareId { get; set; }

    public byte DressingId { get; set; }

    public byte Quantity { get; set; }

    public int Price { get; set; }

    public int Payment => Quantity * Price;

    public WoundCare WoundCare { get; set; }

    public Dressing Dressing { get; set; }

    
}