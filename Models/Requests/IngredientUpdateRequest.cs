using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class IngredientUpdateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public double Acidity { get; set; } = 7.0;
    public double SugarLevel { get; set; }
    public double Abv { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}