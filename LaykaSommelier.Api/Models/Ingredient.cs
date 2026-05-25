using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("ingredients")]
public class Ingredient
{
    [Key]
    [Column("ingredient_id")]
    public long Id { get; set; }

    [Required]
    [Column("ingredient_name")]
    public string Name { get; set; } = string.Empty;

    [Column("ingredient_acidity")]
    public double Acidity { get; set; } = 7.0;

    [Column("ingredient_sugar_level")]
    public double SugarLevel { get; set; }

    [Column("ingredient_abv")]
    public double Abv { get; set; }

    [Column("ingredient_image_url")]
    public string ImageUrl { get; set; } = string.Empty;
}