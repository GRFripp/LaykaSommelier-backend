using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaykaSommelier.Api.Models;

[Table("employees")]
public class Employee
{
    [Key]
    [Column("employee_id")]
    public long Id { get; set; }

    [Required]
    [Column("employee_name")]
    public string Name { get; set; } = string.Empty;
	[Required]
	[Column("employee_email")]
	public string Email {get; set; } = "123@gmail.com";
    [Column("employee_password")]
    public string Password { get; set; } = "1234";

    [Required]
    [Column("employee_position")]
    public string Position { get; set; } = "Помощник";
}