using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public IngredientsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Ingredient>>> GetAll()
    {
        return await _context.Ingredients.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredient>> GetById(long id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return NotFound();
        return ingredient;
    }

    [HttpPost]
    public async Task<ActionResult<Ingredient>> Create(IngredientCreateRequest request)
    {
        var ingredient = new Ingredient
        {
            Name = request.Name,
            Acidity = request.Acidity,
            SugarLevel = request.SugarLevel,
            Abv = request.Abv,
            ImageUrl = request.ImageUrl
        };

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, ingredient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, IngredientUpdateRequest request)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return NotFound();

        ingredient.Name = request.Name;
        ingredient.Acidity = request.Acidity;
        ingredient.SugarLevel = request.SugarLevel;
        ingredient.Abv = request.Abv;
        ingredient.ImageUrl = request.ImageUrl;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return NotFound();

        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}