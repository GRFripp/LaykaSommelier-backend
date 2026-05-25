using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;
using FirebaseAdmin.Auth;
namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        return await _context.Employees.ToListAsync();
    }

    [HttpPost]
public async Task<ActionResult<Employee>> Create(EmployeeCreateRequest request)
{
    // Проверяем, существует ли уже пользователь с таким email
    try
    {
        var existingUser = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return BadRequest("Пользователь с таким email уже существует в Firebase");
        }
    }
    catch (FirebaseAuthException)
    {
        // пользователь не найден, можно создавать
    }

    // Создаём пользователя в Firebase
    var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs()
    {
        Email = request.Email,
        Password = request.Password,
        DisplayName = request.Name
    });

    var employee = new Employee
    {
        Name = request.Name,
        Email = request.Email,
        Password = request.Password,  // временно храним, потом уберём
        Position = request.Position
    };
    _context.Employees.Add(employee);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetAll), new { }, employee);
}

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, EmployeeCreateRequest request)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        employee.Name = request.Name;
        employee.Password = request.Password;
        employee.Position = request.Position;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}