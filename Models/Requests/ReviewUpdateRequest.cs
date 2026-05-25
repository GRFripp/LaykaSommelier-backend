using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class ReviewUpdateRequest
{
    public long ReviewedDrinkId { get; set; }
    public long SourceId { get; set; }
    public string? Url { get; set; }
}