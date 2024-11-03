using Ass2.DAL.Repository.IRepository;
using Ass2.DAL.Repository;
using Ass2.Model.Data;
using Ass2.Model.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using Ass2.BLL.Service.IService;
using Ass2.BLL.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.OData;
using Ass2.Model.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ODATA
builder.Services.AddControllers()
    .AddOData(opt => opt.Select().Filter().OrderBy().Expand().SetMaxTop(100).Count()
    .AddRouteComponents("odata", GetEdmModel()));

// Add DB
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<EnglishPremierLeague2024DbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("DB");
    option.UseSqlServer(connectionString);
});

// Inject app Dependency Injection
builder.Services.AddScoped<EnglishPremierLeague2024DbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddScoped<IFootballClubRepository, FootballClubRepository>();
builder.Services.AddScoped<IFootballPlayerRepository, FootballPlayerRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPremierLeagueAccountRepository, PremierLeagueAccountRepository>();

builder.Services.AddScoped<IFootballPlayerService, FootballPlayerService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});


// Bear
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: Bearer Generated-JWT-Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[] { }
                    }
                });
});

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
app.UseCors("AllowSpecificOrigin");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (!app.Environment.IsDevelopment())
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API");
        c.RoutePrefix = string.Empty;
    }
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// OData model setup
IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<FootballPlayer>("FootballPlayers");
    builder.EntityType<FootballPlayer>().HasKey(p => p.FootballPlayerId);
    builder.EntitySet<FootballClub>("FootballClubs");
    return builder.GetEdmModel();
}
