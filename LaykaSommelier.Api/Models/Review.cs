using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("reviews")]
public class Review
{
    [Key]
    [Column("review_id")]
    public long Id { get; set; }

    [Column("reviewed_drink_id")]
    public long ReviewedDrinkId { get; set; }

    [ForeignKey("ReviewedDrinkId")]
    public Drink? Drink { get; set; }

    [Column("review_source_id")]
    public long SourceId { get; set; }

    [ForeignKey("SourceId")]
    public Source? Source { get; set; }

    [Column("review_url")]
    public string? Url { get; set; }
}