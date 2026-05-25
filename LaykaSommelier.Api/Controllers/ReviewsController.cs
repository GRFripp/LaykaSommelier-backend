using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReviewsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Review>>> GetAll()
    {
        return await _context.Reviews
            .Include(r => r.Drink)
            .Include(r => r.Source)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Review>> Create(ReviewCreateRequest request)
    {
        if (!await _context.Drinks.AnyAsync(d => d.Id == request.ReviewedDrinkId))
            return BadRequest("Напиток не найден");
        if (!await _context.Sources.AnyAsync(s => s.Id == request.SourceId))
            return BadRequest("Источник не найден");

        var review = new Review
        {
            ReviewedDrinkId = request.ReviewedDrinkId,
            SourceId = request.SourceId,
            Url = request.Url
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        // загружаем связи для ответа
        await _context.Entry(review).Reference(r => r.Drink).LoadAsync();
        await _context.Entry(review).Reference(r => r.Source).LoadAsync();

        return CreatedAtAction(nameof(GetAll), new { }, review);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, ReviewCreateRequest request)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return NotFound();

        if (!await _context.Drinks.AnyAsync(d => d.Id == request.ReviewedDrinkId))
            return BadRequest("Напиток не найден");
        if (!await _context.Sources.AnyAsync(s => s.Id == request.SourceId))
            return BadRequest("Источник не найден");

        review.ReviewedDrinkId = request.ReviewedDrinkId;
        review.SourceId = request.SourceId;
        review.Url = request.Url;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return NotFound();
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}