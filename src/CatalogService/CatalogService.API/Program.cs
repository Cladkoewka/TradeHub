using CatalogService.Application.Mapping;
using CatalogService.Application.Services;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Mapster
MapsterConfig.RegisterMappings();
services.AddMapster();

// Db Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(connectionString));

// Repository, Service
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ProductService>();
    
// Controllers, swagger
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();