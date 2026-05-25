using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DescriptorReviewsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DescriptorReviewsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<DescriptorReview>>> GetAll()
    {
        return await _context.DescriptorsReviews
            .Include(dr => dr.Descriptor)
            .Include(dr => dr.Review)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<DescriptorReview>> AddLink(DescriptorReviewLinkRequest request)
    {
        if (!await _context.Descriptors.AnyAsync(d => d.Id == request.DescriptorId))
            return BadRequest("Дескриптор не найден");
        if (!await _context.Reviews.AnyAsync(r => r.Id == request.ReviewId))
            return BadRequest("Обзор не найден");

        var existing = await _context.DescriptorsReviews
            .FirstOrDefaultAsync(x => x.DescriptorId == request.DescriptorId && x.ReviewId == request.ReviewId);
        if (existing != null)
            return Conflict("Связь уже существует");

        var link = new DescriptorReview
        {
            DescriptorId = request.DescriptorId,
            ReviewId = request.ReviewId
        };
        _context.DescriptorsReviews.Add(link);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { }, link);
    }

    [HttpDelete("{descriptorId}/{reviewId}")]
    public async Task<IActionResult> DeleteLink(long descriptorId, long reviewId)
    {
        var link = await _context.DescriptorsReviews
            .FirstOrDefaultAsync(x => x.DescriptorId == descriptorId && x.ReviewId == reviewId);
        if (link == null) return NotFound();
        _context.DescriptorsReviews.Remove(link);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}