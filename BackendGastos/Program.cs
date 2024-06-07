using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.Services;
using BackendGastos.Validator.CategoriaIngreso;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Service.AutoMappers;
using BackendGastos.Validator.CategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Validator.SubCategoriaIngreso;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Validator.SubCategoriaGasto;
using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Validator.Ingreso;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Entity Framework 
builder.Services.AddDbContext<ProyectoGastosTestContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("GastosDb"));
});

// Services
builder.Services.AddScoped<ICategoriaIngresoService, CategoriaIngresoService>();
builder.Services.AddScoped<ICategoriaGastoService, CategoriaGastoService>();
builder.Services.AddScoped<ISubCategoriaIngresoService, SubCategoriaIngresoService>();
builder.Services.AddScoped<ISubCategoriaGastoService, SubCategoriaGastoService>();
builder.Services.AddScoped<IIngresoService, IngresoService>();


// Validators 
builder.Services.AddScoped<IValidator<InsertUpdateCategoriaIngresoDto>, InsertUpdateCategoriaIngresoValidator>();
builder.Services.AddScoped<IValidator<CategoriaIngresoDto>, CategoriaIngresoValidator>();

builder.Services.AddScoped<IValidator<InsertUpdateCategoriaGastoDto>, InsertUpdateCategoriaGastoValidator>();
builder.Services.AddScoped<IValidator<CategoriaGastoDto>, CategoriaGastoValidator>();

builder.Services.AddScoped<IValidator<InsertUpdateSubCategoriaIngresoDto>, InsertUpdateSubCategoriaIngresoValidator>();
builder.Services.AddScoped<IValidator<SubCategoriaIngresoDto>, SubCategoriaIngresoValidator>();

builder.Services.AddScoped<IValidator<InsertUpdateSubCategoriaGastoDto>, InsertUpdateSubCategoriaGastoValidator>();
builder.Services.AddScoped<IValidator<SubCategoriaGastoDto>, SubCategoriaGastoValidator>();

builder.Services.AddScoped<IValidator<InsertUpdateIngresoDto>, InsertUpdateIngresoValidator>();
builder.Services.AddScoped<IValidator<IngresoDto>, IngresoValidator>();


// Repository
builder.Services.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
builder.Services.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
builder.Services.AddScoped<ISubCategoriaIngresoRepository,  SubCategoriaIngresoRepository>();
builder.Services.AddScoped<ISubCategoriaGastoRepository, SubCategoriaGastoRepository>();
builder.Services.AddScoped<IIngresoRepository, IngresoRepository>();



// AutoMappers
builder.Services.AddAutoMapper(typeof(MappingProfile));


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
