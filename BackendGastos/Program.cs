using BackendGastos.Repository.Models;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.AutoMappers;
using BackendGastos.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

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
builder.Services.AddScoped<ISubCategoriaIngresoRepository, SubCategoriaIngresoRepository>();
builder.Services.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Gasto
builder.Services.AddScoped<IGastoService, GastoService>();
builder.Services.AddScoped<ISubCategoriaGastoRepository, SubCategoriaGastoRepository>();
builder.Services.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Transaccion
builder.Services.AddScoped<ITransaccionService, TransaccionService>();

// Reporte
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUtilidadesService, UtilidadesService>();


// Repository
builder.Services.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
builder.Services.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
builder.Services.AddScoped<ISubCategoriaIngresoRepository, SubCategoriaIngresoRepository>();
builder.Services.AddScoped<ISubCategoriaGastoRepository, SubCategoriaGastoRepository>();
builder.Services.AddScoped<IIngresoRepository, IngresoRepository>();
builder.Services.AddScoped<IGastoRepository, GastoRepository>();
builder.Services.AddScoped<IMonedaRepository, MonedaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();



// AutoMappers
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("https://*:8081");
