using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MakingMethodsController : ControllerBase
{
    private readonly AppDbContext _context;

    public MakingMethodsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<MakingMethod>>> GetAll()
    {
        return await _context.MakingMethods.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<MakingMethod>> Create(MakingMethodCreateRequest request)
    {
        var method = new MakingMethod
        {
            Name = request.Name,
            Dilution = request.Dilution
        };
        _context.MakingMethods.Add(method);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { }, method);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, MakingMethodCreateRequest request)
    {
        var method = await _context.MakingMethods.FindAsync(id);
        if (method == null) return NotFound();
        method.Name = request.Name;
        method.Dilution = request.Dilution;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var method = await _context.MakingMethods.FindAsync(id);
        if (method == null) return NotFound();
        _context.MakingMethods.Remove(method);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

public class MakingMethodCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public double Dilution { get; set; }
}