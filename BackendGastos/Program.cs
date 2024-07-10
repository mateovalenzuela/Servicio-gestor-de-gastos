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
using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Validator.Gasto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Entity Framework 
builder.Services.AddDbContext<ProyectoGastosTestContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("GastosDb"));
});

// Services

// CategoriaIngreso
builder.Services.AddScoped<ICategoriaIngresoService, CategoriaIngresoService>();

// CategoriaGasto
builder.Services.AddScoped<ICategoriaGastoService, CategoriaGastoService>();

// SubCategoriaIngreso
builder.Services.AddScoped<ISubCategoriaIngresoService, SubCategoriaIngresoService>();
builder.Services.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// SubCategoriaGasto
builder.Services.AddScoped<ISubCategoriaGastoService, SubCategoriaGastoService>();
builder.Services.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Ingreso
builder.Services.AddScoped<IIngresoService, IngresoService>();
builder.Services.AddScoped<ISubCategoriaIngresoRepository,  SubCategoriaIngresoRepository>();
builder.Services.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Gasto
builder.Services.AddScoped<IGastoService, GastoService>();
builder.Services.AddScoped<ISubCategoriaGastoRepository, SubCategoriaGastoRepository>();
builder.Services.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


// Validators 
// CategoriaIngreso
builder.Services.AddScoped<IValidator<InsertUpdateCategoriaIngresoDto>, InsertUpdateCategoriaIngresoValidator>();
builder.Services.AddScoped<IValidator<CategoriaIngresoDto>, CategoriaIngresoValidator>();
// CategoriaGasto
builder.Services.AddScoped<IValidator<InsertUpdateCategoriaGastoDto>, InsertUpdateCategoriaGastoValidator>();
builder.Services.AddScoped<IValidator<CategoriaGastoDto>, CategoriaGastoValidator>();
// SubCategoriaIngreso
builder.Services.AddScoped<IValidator<InsertUpdateSubCategoriaIngresoDto>, InsertUpdateSubCategoriaIngresoValidator>();
builder.Services.AddScoped<IValidator<SubCategoriaIngresoDto>, SubCategoriaIngresoValidator>();
// SubCategoriaGasto
builder.Services.AddScoped<IValidator<InsertUpdateSubCategoriaGastoDto>, InsertUpdateSubCategoriaGastoValidator>();
builder.Services.AddScoped<IValidator<SubCategoriaGastoDto>, SubCategoriaGastoValidator>();
// Ingreso
builder.Services.AddScoped<IValidator<InsertUpdateIngresoDto>, InsertUpdateIngresoValidator>();
builder.Services.AddScoped<IValidator<IngresoDto>, IngresoValidator>();
// Gasto
builder.Services.AddScoped<IValidator<InsertUpdateGastoDto>, InsertUpdateGastoValidator>();
builder.Services.AddScoped<IValidator<GastoDto>, GastoValidator>();


// Repository
builder.Services.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
builder.Services.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
builder.Services.AddScoped<ISubCategoriaIngresoRepository,  SubCategoriaIngresoRepository>();
builder.Services.AddScoped<ISubCategoriaGastoRepository, SubCategoriaGastoRepository>();
builder.Services.AddScoped<IIngresoRepository, IngresoRepository>();
builder.Services.AddScoped<IGastoRepository, GastoRepository>();
builder.Services.AddScoped<IMonedaRepository, MonedaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();



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

app.Run("https://*:8081");
