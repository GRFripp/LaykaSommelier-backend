using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class DrinkCreateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Type { get; set; } = string.Empty;
    public string? SubType { get; set; }
    public string? Country { get; set; }
    public string? Producer { get; set; }
    public int Aged { get; set; }
    public double Abv { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}