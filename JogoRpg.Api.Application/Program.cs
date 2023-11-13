using JogoRpg.Data.Context;
using JogoRpg.Data.Repositories;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;
using JogoRpg.Service.Services;
using JogoRpg.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
var jwtSettings = configuration.GetSection("AppSettings:Jwt").Get<JwtSettings>() ?? new JwtSettings();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton(configuration);
builder.Services.AddScoped<EntityContext>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<EntityContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = string.IsNullOrEmpty(jwtSettings.Issuer) ? "DefaultIssuer" : jwtSettings.Issuer,
            ValidAudience = string.IsNullOrEmpty(jwtSettings.Audience) ? "DefaultAudience" : jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(string.IsNullOrEmpty(jwtSettings.Key) ? "DefaultKey" : jwtSettings.Key))
        };
    });

var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();