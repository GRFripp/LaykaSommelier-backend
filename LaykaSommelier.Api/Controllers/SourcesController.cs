using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SourcesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SourcesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Source>>> GetAll()
    {
        return await _context.Sources.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Source>> Create(SourceCreateRequest request)
    {
        var source = new Source { Name = request.Name, Url = request.Url };
        _context.Sources.Add(source);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { }, source);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, SourceCreateRequest request)
    {
        var source = await _context.Sources.FindAsync(id);
        if (source == null) return NotFound();
        source.Name = request.Name;
        source.Url = request.Url;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var source = await _context.Sources.FindAsync(id);
        if (source == null) return NotFound();
        _context.Sources.Remove(source);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}