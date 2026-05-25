using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("suggestions")]
public class Suggestion
{
    [Key]
    [Column("suggestion_id")]
    public long Id { get; set; }

    [Column("suggested_cocktail_id")]
    public long CocktailId { get; set; }

    [ForeignKey("CocktailId")]
    public Cocktail? Cocktail { get; set; }

    [Column("suggestion_employee_id")]
    public long EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    public Employee? Employee { get; set; }

    [Required]
    [Column("suggestion_status")]
    public string Status { get; set; } = "pending";
}