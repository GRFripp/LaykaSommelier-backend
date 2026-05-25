using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class CocktailUpdateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public double Volume { get; set; }
    public double Acidity { get; set; }
    public double SugarLevel { get; set; }
    public double Abv { get; set; }
    [Required]
    public string Glass { get; set; } = string.Empty;
    public long MakingMethodId { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = "unknown";
    [Required]
    public string Serving { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}