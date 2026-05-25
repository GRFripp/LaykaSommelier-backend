using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin.Auth;

using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;


namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyToken([FromBody] TokenRequest request)
    {
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.Token);
            var uid = decodedToken.Uid;
            var email = decodedToken.Claims.ContainsKey("email") ? decodedToken.Claims["email"].ToString() : "";

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
			var role = employee?.Position ?? "unknown";
			var employeeId = employee?.Id ?? -1L;

			return Ok(new { uid, email, role, employeeId });
        }
        catch
        {
            return Unauthorized();
        }
    }
}

public class TokenRequest
{
    public string Token { get; set; } = string.Empty;
}