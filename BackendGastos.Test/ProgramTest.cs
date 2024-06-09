using BackendGastos.Repository.Models;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.AutoMappers;
using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Service.Services;
using BackendGastos.Validator.CategoriaGasto;
using BackendGastos.Validator.CategoriaIngreso;
using BackendGastos.Validator.Ingreso;
using BackendGastos.Validator.SubCategoriaGasto;
using BackendGastos.Validator.SubCategoriaIngreso;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Test
{
    internal static class ProgramTest
    {
        internal static ServiceProvider GetServices(bool inMemoryDb)
        {
            var builder = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Services

            // CategoriaIngreso
            builder.AddScoped<ICategoriaIngresoService, CategoriaIngresoService>();

            // CategoriaGasto
            builder.AddScoped<ICategoriaGastoService, CategoriaGastoService>();

            // SubCategoriaIngreso
            builder.AddScoped<ISubCategoriaIngresoService, SubCategoriaIngresoService>();
            builder.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
            builder.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // SubCategoriaGasto
            builder.AddScoped<ISubCategoriaGastoService, SubCategoriaGastoService>();
            builder.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
            builder.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Ingreso
            builder.AddScoped<IIngresoService, IngresoService>();
            builder.AddScoped<ISubCategoriaIngresoRepository, SubCategoriaIngresoRepository>();
            builder.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
            builder.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Gasto
            builder.AddScoped<IGastoService, GastoService>();
            builder.AddScoped<ISubCategoriaGastoRepository, SubCategoriaGastoRepository>();
            builder.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
            builder.AddScoped<IUsuarioRepository, UsuarioRepository>();




            // Validators 
            builder.AddScoped<IValidator<InsertUpdateCategoriaIngresoDto>, InsertUpdateCategoriaIngresoValidator>();
            builder.AddScoped<IValidator<CategoriaIngresoDto>, CategoriaIngresoValidator>();

            builder.AddScoped<IValidator<InsertUpdateCategoriaGastoDto>, InsertUpdateCategoriaGastoValidator>();
            builder.AddScoped<IValidator<CategoriaGastoDto>, CategoriaGastoValidator>();

            builder.AddScoped<IValidator<InsertUpdateSubCategoriaIngresoDto>, InsertUpdateSubCategoriaIngresoValidator>();
            builder.AddScoped<IValidator<SubCategoriaIngresoDto>, SubCategoriaIngresoValidator>();

            builder.AddScoped<IValidator<InsertUpdateSubCategoriaGastoDto>, InsertUpdateSubCategoriaGastoValidator>();
            builder.AddScoped<IValidator<SubCategoriaGastoDto>, SubCategoriaGastoValidator>();

            builder.AddScoped<IValidator<InsertUpdateIngresoDto>, InsertUpdateIngresoValidator>();
            builder.AddScoped<IValidator<IngresoDto>, IngresoValidator>();


            // Repository
            builder.AddScoped<ICategoriaIngresoRepository, CategoriaIngresoRepository>();
            builder.AddScoped<ICategoriaGastoRepository, CategoriaGastoRepository>();
            builder.AddScoped<ISubCategoriaIngresoRepository, SubCategoriaIngresoRepository>();
            builder.AddScoped<ISubCategoriaGastoRepository, SubCategoriaGastoRepository>();
            builder.AddScoped<IIngresoRepository, IngresoRepository>();
            builder.AddScoped<IMonedaRepository, MonedaRepository>();
            builder.AddScoped<IUsuarioRepository, UsuarioRepository>();



            // AutoMappers
            builder.AddAutoMapper(typeof(MappingProfile));

            if (!inMemoryDb)
            {
                // Entity Framework 
                builder.AddDbContext<ProyectoGastosTestContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("GastosDb"));
                });
            }
            else
            {
                // Entity Framework 
                builder.AddDbContext<ProyectoGastosTestContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                });
            }

            var serviceProvider = builder.BuildServiceProvider();

            // Initialize database with seed data if using in-memory database
            
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ProyectoGastosTestContext>();
                DatabaseInitializer.Initialize(context);
            }
            

            return serviceProvider;
        }
    }
}
