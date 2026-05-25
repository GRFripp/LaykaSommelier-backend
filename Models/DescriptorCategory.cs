using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("descriptor_categories")]
public class DescriptorCategory
{
    [Key]
    [Column("descriptor_category_id")]
    public long Id { get; set; }

    [Required]
    [Column("descriptor_category_name")]
    public string Name { get; set; } = string.Empty;

    [Column("descriptor_category_color")]
    public string Color { get; set; } = string.Empty;
}