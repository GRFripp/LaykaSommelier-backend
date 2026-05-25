using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("making_methods")]
public class MakingMethod
{
    [Key]
    [Column("making_method_id")]
    public long Id { get; set; }

    [Required]
    [Column("making_method_name")]
    public string Name { get; set; } = string.Empty;

    [Column("making_method_dilution")]
    public double Dilution { get; set; }
}