using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CocktailsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CocktailsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
public async Task<ActionResult<List<Cocktail>>> GetAll()
{
    var cocktails = await _context.Cocktails
        .Include(c => c.MakingMethod)
        .ToListAsync();

    return cocktails;
}

    [HttpGet("{id}")]
    public async Task<ActionResult<Cocktail>> GetById(long id)
    {
        var cocktail = await _context.Cocktails
            .Include(c => c.MakingMethod)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (cocktail == null) return NotFound();
        return cocktail;
    }

    [HttpPost]
    public async Task<ActionResult<Cocktail>> Create(CocktailCreateRequest request)
    {
        var cocktail = new Cocktail
        {
            Name = request.Name,
            Volume = request.Volume,
            Acidity = request.Acidity,
            SugarLevel = request.SugarLevel,
            Abv = request.Abv,
            Glass = request.Glass,
            MakingMethodId = request.MakingMethodId,
            Description = request.Description,
            Author = request.Author,
            Serving = request.Serving,
            ImageUrl = request.ImageUrl
        };

        _context.Cocktails.Add(cocktail);
        await _context.SaveChangesAsync();

        // Загружаем связанный MakingMethod для ответа
        await _context.Entry(cocktail).Reference(c => c.MakingMethod).LoadAsync();

        return CreatedAtAction(nameof(GetById), new { id = cocktail.Id }, cocktail);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, CocktailUpdateRequest request)
    {
        var cocktail = await _context.Cocktails.FindAsync(id);
        if (cocktail == null) return NotFound();

        cocktail.Name = request.Name;
        cocktail.Volume = request.Volume;
        cocktail.Acidity = request.Acidity;
        cocktail.SugarLevel = request.SugarLevel;
        cocktail.Abv = request.Abv;
        cocktail.Glass = request.Glass;
        cocktail.MakingMethodId = request.MakingMethodId;
        cocktail.Description = request.Description;
        cocktail.Author = request.Author;
        cocktail.Serving = request.Serving;
        cocktail.ImageUrl = request.ImageUrl;

        await _context.SaveChangesAsync();
        return NoContent();
    }

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(long id)
{
    var cocktail = await _context.Cocktails.FindAsync(id);
    if (cocktail == null) return NotFound();

    _context.Cocktails.Remove(cocktail);
    await _context.SaveChangesAsync();
    return NoContent();
}
}