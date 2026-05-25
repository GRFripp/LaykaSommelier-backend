using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ---------- ПОРТ 8080 (Amvera) ----------
builder.WebHost.UseUrls("http://*:8080");

// ---------- СЕРВИСЫ ----------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Подключение БД (строка берётся из переменной окружения)
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Инициализация Firebase Admin (из переменной окружения)
var firebaseJson = Environment.GetEnvironmentVariable("FIREBASE_ADMIN_SDK_JSON");
if (!string.IsNullOrEmpty(firebaseJson))
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromJson(firebaseJson)
    });
}
else
{
    // fallback для локальной разработки
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile("firebase-admin-sdk.json")
    });
}

// JWT-аутентификация (Firebase)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/laykasommelier";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/laykasommelier",
            ValidateAudience = true,
            ValidAudience = "laykasommelier",
            ValidateLifetime = true
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// ---------- СБОРКА ----------
var app = builder.Build();

// Автомиграция
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ---------- MIDDLEWARE ----------
app.UseSwagger();
app.UseSwaggerUI();   // в облаке тоже оставим для диагностики
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();