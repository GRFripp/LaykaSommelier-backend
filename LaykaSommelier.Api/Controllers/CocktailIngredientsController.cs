using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CocktailIngredientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CocktailIngredientsController(AppDbContext context)
    {
        _context = context;
    }

    // Получить все связи (как и раньше)
    [HttpGet]
    public async Task<ActionResult<List<CocktailIngredient>>> GetAll()
    {
        return await _context.CocktailsIngredients
            .Include(ci => ci.Cocktail)
            .Include(ci => ci.Ingredient)
            .ToListAsync();
    }

    // Добавить ингредиент в коктейль
    [HttpPost]
    public async Task<ActionResult<CocktailIngredient>> AddLink(CocktailIngredientLinkRequest request)
    {
        // Проверяем, существуют ли коктейль и ингредиент
        if (!await _context.Cocktails.AnyAsync(c => c.Id == request.CocktailId))
            return BadRequest("Коктейль не найден");
        if (!await _context.Ingredients.AnyAsync(i => i.Id == request.IngredientId))
            return BadRequest("Ингредиент не найден");

        var existing = await _context.CocktailsIngredients
            .FirstOrDefaultAsync(ci => ci.CocktailId == request.CocktailId && ci.IngredientId == request.IngredientId);
        if (existing != null)
            return Conflict("Связь уже существует");

        var link = new CocktailIngredient
        {
            CocktailId = request.CocktailId,
            IngredientId = request.IngredientId,
            Volume = request.Volume
        };

        _context.CocktailsIngredients.Add(link);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { }, link);
    }

    // Удалить конкретную связь (по cocktailId и ingredientId)
    [HttpDelete("{cocktailId}/{ingredientId}")]
    public async Task<IActionResult> DeleteLink(long cocktailId, long ingredientId)
    {
        var link = await _context.CocktailsIngredients
            .FirstOrDefaultAsync(ci => ci.CocktailId == cocktailId && ci.IngredientId == ingredientId);
        if (link == null) return NotFound();

        _context.CocktailsIngredients.Remove(link);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

// Вспомогательный DTO
public class CocktailIngredientLinkRequest
{
    public long CocktailId { get; set; }
    public long IngredientId { get; set; }
    public double Volume { get; set; }
}