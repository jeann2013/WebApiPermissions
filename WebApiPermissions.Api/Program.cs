using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiPermissions.Application.Queries;
using WebApiPermissions.Infrastructure.Data;
using WebApiPermissions.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Configurar SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar Unit of Work y Repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configurar MediatR para CQRS (registrando TODOS los handlers)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPermissionsHandler).Assembly));


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
