using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class ReviewCreateRequest
{
    public long ReviewedDrinkId { get; set; }
    public long SourceId { get; set; }
    public string? Url { get; set; }
}