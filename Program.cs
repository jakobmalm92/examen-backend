using examensarbeteBackend.Data;
using examensarbeteBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Lägg till DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Lägg till CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Lägg till JWT-konfiguration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddSingleton(new JwtTokenService(
    jwtSettings["SecretKey"],
    jwtSettings["Issuer"],
    jwtSettings["Audience"]
));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });

// Lägg till controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware för CORS
app.UseCors();

// Middleware för autentisering och auktorisering
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

// Mappa controllers
app.MapControllers();

app.Run();
