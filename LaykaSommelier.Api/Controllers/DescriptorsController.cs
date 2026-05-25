using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DescriptorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DescriptorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Descriptor>>> GetAll()
    {
        return await _context.Descriptors
            .Include(d => d.Category)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Descriptor>> GetById(long id)
    {
        var descriptor = await _context.Descriptors
            .Include(d => d.Category)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (descriptor == null) return NotFound();
        return descriptor;
    }

    [HttpPost]
    public async Task<ActionResult<Descriptor>> Create(DescriptorCreateRequest request)
    {
        if (!await _context.DescriptorCategories.AnyAsync(c => c.Id == request.CategoryId))
            return BadRequest("Указанная категория не существует");

        var descriptor = new Descriptor
        {
            Name = request.Name,
            CategoryId = request.CategoryId
        };
        _context.Descriptors.Add(descriptor);
        await _context.SaveChangesAsync();
        await _context.Entry(descriptor).Reference(d => d.Category).LoadAsync();

        return CreatedAtAction(nameof(GetById), new { id = descriptor.Id }, descriptor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, DescriptorUpdateRequest request)
    {
        var descriptor = await _context.Descriptors.FindAsync(id);
        if (descriptor == null) return NotFound();

        if (!await _context.DescriptorCategories.AnyAsync(c => c.Id == request.CategoryId))
            return BadRequest("Указанная категория не существует");

        descriptor.Name = request.Name;
        descriptor.CategoryId = request.CategoryId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var descriptor = await _context.Descriptors.FindAsync(id);
        if (descriptor == null) return NotFound();
        _context.Descriptors.Remove(descriptor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}