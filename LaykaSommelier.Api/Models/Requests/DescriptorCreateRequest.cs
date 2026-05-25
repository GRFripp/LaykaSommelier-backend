using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class DescriptorCreateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public long CategoryId { get; set; }
}