using Microsoft.EntityFrameworkCore;
using WebApiPermissions.Infrastructure;
using WebApiPermissions.Infrastructure.Repositories;
using WebApiPermissions.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar Unit of Work y Repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configurar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
