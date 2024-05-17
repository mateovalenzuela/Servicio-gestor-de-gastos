using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.Services;
using BackendGastos.Validator.CategoriaIngreso;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services
builder.Services.AddScoped<ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>, CategoriaIngresoService>();





// Validators 
builder.Services.AddScoped<IValidator<InsertUpdateCategoriaIngresoDto>, InsertUpdateCategoriaIngresoValidator>();


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
