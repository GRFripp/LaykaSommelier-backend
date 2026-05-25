using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("cocktails_ingredients")]
public class CocktailIngredient
{
    [Column("cocktail_id")]
    public long CocktailId { get; set; }

    [ForeignKey("CocktailId")]
    public Cocktail? Cocktail { get; set; }

    [Column("ingredient_id")]
    public long IngredientId { get; set; }

    [ForeignKey("IngredientId")]
    public Ingredient? Ingredient { get; set; }

    [Column("ingredient_volume")]
    public double Volume { get; set; }
}