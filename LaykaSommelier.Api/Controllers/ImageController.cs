using Microsoft.AspNetCore.Mvc;

namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public ImagesController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file");

        var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var url = $"/images/{uniqueName}";
        return Ok(new { url });
    }
}