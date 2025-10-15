using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Serilog;
using PropertyManagement.Application;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure;
using PropertyManagement.Infrastructure.Data;
using PropertyManagement.Infrastructure.Repositories;

// Crear el builder de la aplicación
var builder = WebApplication.CreateBuilder(args);

#region Logging - Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

//builder.Host.UseSerilog();
#endregion

#region Controllers
builder.Services.AddControllers();
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Property Management API",
        Version = "v1",
        Description = "API RESTful para gestión de propiedades y sincronización con OTAs."
    });

    // Autenticación JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // <-- Cambiar a Http
        Scheme = "bearer", // <-- En minúsculas
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce tu token JWT. Ejemplo: **Bearer {tu_token}**"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

#endregion

#region Authentication - JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

#region Database - Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(PropertyManagement.Application.Commands.CreatePropertyCommand).Assembly));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(PropertyManagement.Application.Mappings.MappingProfile));
#endregion

#region Repositories & UnitOfWork
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDomainEventRepository, DomainEventRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Application Services
builder.Services.AddScoped<
    PropertyManagement.Application.Services.Properties.IPropertyService,
    PropertyManagement.Application.Services.Properties.PropertyService>();

builder.Services.AddScoped<
    PropertyManagement.Application.Services.Auth.IAuthService,
    PropertyManagement.Application.Services.Auth.AuthService>();

builder.Services.AddScoped<
    PropertyManagement.Application.Services.Sync.ISyncService,
    PropertyManagement.Application.Services.Sync.SyncService>();
#endregion

// Construir la aplicación
var app = builder.Build();

#region Middleware
// Middleware de manejo global de errores, logging, etc. (opcional futuro)

// Swagger disponible en todos los entornos
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Property Management API v1");
    c.RoutePrefix = string.Empty; // Muestra Swagger en la raíz "/"
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion

// Iniciar la aplicación
app.Run();
