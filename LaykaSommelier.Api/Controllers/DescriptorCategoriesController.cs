using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DescriptorCategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public DescriptorCategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<DescriptorCategory>>> GetAll()
    {
        return await _context.DescriptorCategories.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DescriptorCategory>> GetById(long id)
    {
        var category = await _context.DescriptorCategories.FindAsync(id);
        if (category == null) return NotFound();
        return category;
    }

    [HttpPost]
    public async Task<ActionResult<DescriptorCategory>> Create(DescriptorCategoryCreateRequest request)
    {
        var category = new DescriptorCategory
        {
            Name = request.Name,
            Color = request.Color
        };
        _context.DescriptorCategories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, DescriptorCategoryUpdateRequest request)
    {
        var category = await _context.DescriptorCategories.FindAsync(id);
        if (category == null) return NotFound();
        category.Name = request.Name;
        category.Color = request.Color;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var category = await _context.DescriptorCategories.FindAsync(id);
        if (category == null) return NotFound();
        _context.DescriptorCategories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}