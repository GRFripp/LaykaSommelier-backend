using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class SourceCreateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}