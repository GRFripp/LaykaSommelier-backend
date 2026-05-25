using System.ComponentModel.DataAnnotations;

namespace LaykaSommelier.Api.Models.Requests;

public class EmployeeUpdateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
	[Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = "1234";
    [Required]
    public string Position { get; set; } = string.Empty;
}