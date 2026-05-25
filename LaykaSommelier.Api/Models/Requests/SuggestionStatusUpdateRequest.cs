using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class SuggestionStatusUpdateRequest
{
    public string Status { get; set; } = string.Empty; // "approved" или "rejected"
}