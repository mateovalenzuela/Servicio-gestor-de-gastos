using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.Services;
using BackendGastos.Validator.CategoriaIngreso;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Validator.CategoriaGasto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Entity Framework 
builder.Services.AddDbContext<ProyectoGastosTestContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("GastosDb"));
});

// Services
builder.Services.AddScoped<ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>, CategoriaIngresoService>();
builder.Services.AddScoped<ICommonService<CategoriaGastoDto, InsertUpdateCategoriaGastoDto>, CategoriaGastoService>();


// Validators 
builder.Services.AddScoped<IValidator<InsertUpdateCategoriaIngresoDto>, InsertUpdateCategoriaIngresoValidator>();
builder.Services.AddScoped<IValidator<CategoriaIngresoDto>, CategoriaIngresoValidator>();
builder.Services.AddScoped<IValidator<InsertUpdateCategoriaGastoDto>, InsertUpdateCategoriaGastoValidator>();
builder.Services.AddScoped<IValidator<CategoriaGastoDto>, CategoriaGastoValidator>();


// Repository
builder.Services.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
builder.Services.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

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
