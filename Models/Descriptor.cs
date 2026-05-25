using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("descriptors")]
public class Descriptor
{
    [Key]
    [Column("descriptor_id")]
    public long Id { get; set; }

    [Required]
    [Column("descriptor_name")]
    public string Name { get; set; } = string.Empty;

    [Column("descriptor_category_id")]
    public long CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public DescriptorCategory? Category { get; set; }
}