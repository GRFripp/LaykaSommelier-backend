using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class DescriptorCategoryUpdateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}