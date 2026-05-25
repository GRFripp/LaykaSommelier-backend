using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("cocktails")]
public class Cocktail
{
    [Key]
    [Column("cocktail_id")]
    public long Id { get; set; }

    [Required]
    [Column("cocktail_name")]
    public string Name { get; set; } = string.Empty;

    [Column("cocktail_volume")]
    public double Volume { get; set; }

    [Column("cocktail_acidity")]
    public double Acidity { get; set; }

    [Column("cocktail_sugar_level")]
    public double SugarLevel { get; set; }

    [Column("cocktail_abv")]
    public double Abv { get; set; }

    [Required]
    [Column("cocktail_glass")]
    public string Glass { get; set; } = string.Empty;

    [Column("cocktail_making_method_id")]
    public long MakingMethodId { get; set; }

    [ForeignKey("MakingMethodId")]
    public MakingMethod? MakingMethod { get; set; }

    [Required]
    [Column("cocktail_description")]
    public string Description { get; set; } = string.Empty;

    [Column("cocktail_author")]
    public string Author { get; set; } = "unknown";

    [Required]
    [Column("cocktail_serving")]
    public string Serving { get; set; } = string.Empty;

    [Column("cocktail_image_url")]
    public string ImageUrl { get; set; } = string.Empty;
}