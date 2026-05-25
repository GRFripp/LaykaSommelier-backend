using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientDescriptorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public IngredientDescriptorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<IngredientDescriptor>>> GetAll()
    {
        return await _context.IngredientsDescriptors
            .Include(id => id.Ingredient)
            .Include(id => id.Descriptor)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<IngredientDescriptor>> AddLink(IngredientDescriptorLinkRequest request)
    {
        if (!await _context.Ingredients.AnyAsync(i => i.Id == request.IngredientId))
            return BadRequest("Ингредиент не найден");
        if (!await _context.Descriptors.AnyAsync(d => d.Id == request.DescriptorId))
            return BadRequest("Дескриптор не найден");

        var existing = await _context.IngredientsDescriptors
            .FirstOrDefaultAsync(x => x.IngredientId == request.IngredientId && x.DescriptorId == request.DescriptorId);
        if (existing != null)
            return Conflict("Связь уже существует");

        var link = new IngredientDescriptor
        {
            IngredientId = request.IngredientId,
            DescriptorId = request.DescriptorId
        };
        _context.IngredientsDescriptors.Add(link);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { }, link);
    }

    [HttpDelete("{ingredientId}/{descriptorId}")]
    public async Task<IActionResult> DeleteLink(long ingredientId, long descriptorId)
    {
        var link = await _context.IngredientsDescriptors
            .FirstOrDefaultAsync(x => x.IngredientId == ingredientId && x.DescriptorId == descriptorId);
        if (link == null) return NotFound();

        _context.IngredientsDescriptors.Remove(link);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}