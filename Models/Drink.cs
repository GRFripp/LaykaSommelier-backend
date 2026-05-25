using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("drinks")]
public class Drink
{
    [Key]
    [Column("drink_id")]
    public long Id { get; set; }

    [Required]
    [Column("drink_name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("drink_type")]
    public string Type { get; set; } = string.Empty;

    [Column("drink_sub_type")]
    public string? SubType { get; set; }

    [Column("drink_country")]
    public string? Country { get; set; }

    [Column("drink_producer")]
    public string? Producer { get; set; }

    [Column("drink_aged")]
    public int Aged { get; set; }

    [Column("drink_abv")]
    public double Abv { get; set; }

    [Column("drink_image_url")]
    public string ImageUrl { get; set; } = string.Empty;
}