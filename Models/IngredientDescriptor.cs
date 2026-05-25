using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("ingredients_descriptors")]
public class IngredientDescriptor
{
    [Column("ingredient_id")]
    public long IngredientId { get; set; }

    [ForeignKey("IngredientId")]
    public Ingredient? Ingredient { get; set; }

    [Column("descriptor_id")]
    public long DescriptorId { get; set; }

    [ForeignKey("DescriptorId")]
    public Descriptor? Descriptor { get; set; }
}