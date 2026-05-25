using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class SuggestionUpdateRequest
{
    public long CocktailId { get; set; }
    public long EmployeeId { get; set; }
    [Required]
    public string Status { get; set; } = string.Empty;
}