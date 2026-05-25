using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using LaykaSommelier.Api.Models;
using LaykaSommelier.Api.Models.Requests;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
namespace LaykaSommelier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuggestionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SuggestionsController(AppDbContext context) => _context = context;

    // Вспомогательный метод для получения текущего сотрудника по токену Firebase
    private async Task<Employee?> GetCurrentEmployee()
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return null;

        var token = authHeader.Substring("Bearer ".Length);
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            var email = decodedToken.Claims.ContainsKey("email") ? decodedToken.Claims["email"].ToString() : "";
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
        catch
        {
            return null;
        }
    }

    // GET: api/suggestions
    // Можно указать параметр status для фильтрации
    [HttpGet]
    public async Task<ActionResult<List<Suggestion>>> GetAll([FromQuery] string? status)
    {
        var query = _context.Suggestions
            .Include(s => s.Employee)
            .Include(s => s.Cocktail)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
            query = query.Where(s => s.Status == status);

        return await query.ToListAsync();
    }

    // POST: api/suggestions
    [HttpPost]
public async Task<ActionResult<Suggestion>> Create(SuggestionCreateRequest request)
{
    if (!await _context.Cocktails.AnyAsync(c => c.Id == request.CocktailId))
        return BadRequest("Коктейль не найден");
    if (!await _context.Employees.AnyAsync(e => e.Id == request.EmployeeId))
        return BadRequest("Сотрудник не найден");

    var suggestion = new Suggestion
    {
        CocktailId = request.CocktailId,
        EmployeeId = request.EmployeeId,
        Status = request.Status
    };
    _context.Suggestions.Add(suggestion);
    await _context.SaveChangesAsync();
    await _context.Entry(suggestion).Reference(s => s.Employee).LoadAsync();
    await _context.Entry(suggestion).Reference(s => s.Cocktail).LoadAsync();

    // === ОТПРАВКА УВЕДОМЛЕНИЯ МЕНЕДЖЕРАМ ===
    try
{
    var message = new Message()
    {
        Topic = "all_users",
        Notification = new Notification
        {
            Title = "Новая заявка",
            Body = $"Коктейль «{suggestion.Cocktail?.Name ?? "?"}» от {suggestion.Employee?.Name ?? "?"}"
        },
        Data = new Dictionary<string, string>
        {
            { "type", "new_suggestion" },
            { "suggestionId", suggestion.Id.ToString() }
        }
    };
    var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
    Console.WriteLine($"FCM sent: {response}");
}
catch (Exception ex)
{
    Console.WriteLine($"FCM error: {ex.Message}");
    // Если нужно, сохраните в лог-файл
}

    return CreatedAtAction(nameof(GetAll), new { }, suggestion);
}

    // PUT: api/suggestions/{id}/status
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] SuggestionStatusUpdateRequest request)
    {
        // Проверка прав: только менеджер (head_bartender) может менять статус
        var employee = await GetCurrentEmployee();
        if (employee == null || employee.Position != "Менеджер")
            return Unauthorized("Только менеджер может менять статус заявки");

        var suggestion = await _context.Suggestions.FindAsync(id);
        if (suggestion == null) return NotFound();

        if (request.Status != "approved" && request.Status != "rejected")
            return BadRequest("Допустимые статусы: approved, rejected");

        suggestion.Status = request.Status;
        await _context.SaveChangesAsync();
        return NoContent();
    }

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(long id)
{
    // Проверка прав – только менеджер
    var employee = await GetCurrentEmployee();
    if (employee == null || employee.Position != "Менеджер")
        return Unauthorized("Только менеджер может удалять заявки");

    var suggestion = await _context.Suggestions.FindAsync(id);
    if (suggestion == null) return NotFound();

    long cocktailId = suggestion.CocktailId;

    _context.Suggestions.Remove(suggestion);
    await _context.SaveChangesAsync();

    // Если не осталось других заявок на этот коктейль – удаляем сам коктейль
    bool hasOtherSuggestions = await _context.Suggestions.AnyAsync(s => s.CocktailId == cocktailId);
    if (!hasOtherSuggestions)
    {
        var cocktail = await _context.Cocktails.FindAsync(cocktailId);
        if (cocktail != null)
        {
            // При удалении коктейля каскадно удалятся связанные CocktailIngredients (если настроен CASCADE)
            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();
        }
    }

    return NoContent();
}
}