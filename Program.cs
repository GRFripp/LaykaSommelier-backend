using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Data;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:8080");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var firebaseJson = Environment.GetEnvironmentVariable("FIREBASE_ADMIN_SDK_JSON");
if (!string.IsNullOrEmpty(firebaseJson))
{
    // Пробуем распознать Base64 (если строка не начинается с '{')
    string jsonContent;
    if (firebaseJson.TrimStart().StartsWith("{"))
    {
        jsonContent = firebaseJson;
    }
    else
    {
        // Декодируем из Base64
        var bytes = Convert.FromBase64String(firebaseJson);
        jsonContent = System.Text.Encoding.UTF8.GetString(bytes);
    }
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromJson(jsonContent)
    });
}
else
{
    // Локальная разработка через файл
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile("firebase-admin-sdk.json")
    });
}

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