using Elastic.Clients.Elasticsearch;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiPermissions.Application.Handlers;
using WebApiPermissions.Application.Queries;
using WebApiPermissions.Infrastructure.Data;
using WebApiPermissions.Infrastructure.Services;
using WebApiPermissions.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Configurar ElasticsearchClient
var elasticSettings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
var elasticClient = new ElasticsearchClient(elasticSettings);

//Registrar ElasticsearchClient como singleton
builder.Services.AddSingleton(elasticClient);

// Registrar ElasticsearchService como singleton
builder.Services.AddSingleton<ElasticsearchService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

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
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RequestPermissionHandler).Assembly));



var app = builder.Build();

app.UseAuthentication();

// Usar CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
