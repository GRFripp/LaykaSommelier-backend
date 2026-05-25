using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("descriptors_reviews")]
public class DescriptorReview
{
    [Column("descriptor_id")]
    public long DescriptorId { get; set; }

    [ForeignKey("DescriptorId")]
    public Descriptor? Descriptor { get; set; }

    [Column("review_id")]
    public long ReviewId { get; set; }

    [ForeignKey("ReviewId")]
    public Review? Review { get; set; }
}