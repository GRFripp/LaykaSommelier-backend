using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("sources")]
public class Source
{
    [Key]
    [Column("source_id")]
    public long Id { get; set; }

    [Required]
    [Column("source_name")]
    public string Name { get; set; } = string.Empty;

    [Column("source_url")]
    public string Url { get; set; } = string.Empty;
}