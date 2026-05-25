using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinksController : ControllerBase
{
    private readonly AppDbContext _context;

    public DrinksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Drink>>> GetAll()
    {
        return await _context.Drinks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Drink>> GetById(long id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null) return NotFound();
        return drink;
    }

    [HttpPost]
    public async Task<ActionResult<Drink>> Create(DrinkCreateRequest request)
    {
        var drink = new Drink
        {
            Name = request.Name,
            Type = request.Type,
            SubType = request.SubType,
            Country = request.Country,
            Producer = request.Producer,
            Aged = request.Aged,
            Abv = request.Abv,
            ImageUrl = request.ImageUrl
        };

        _context.Drinks.Add(drink);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = drink.Id }, drink);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, DrinkUpdateRequest request)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null) return NotFound();

        drink.Name = request.Name;
        drink.Type = request.Type;
        drink.SubType = request.SubType;
        drink.Country = request.Country;
        drink.Producer = request.Producer;
        drink.Aged = request.Aged;
        drink.Abv = request.Abv;
        drink.ImageUrl = request.ImageUrl;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null) return NotFound();

        _context.Drinks.Remove(drink);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}