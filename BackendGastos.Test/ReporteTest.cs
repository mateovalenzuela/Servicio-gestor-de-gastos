using BackendGastos.Controller.Controllers;
using BackendGastos.Repository.Models;
using BackendGastos.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BackendGastos.Test
{
    public class ReporteTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ReporteController _controller;
        private readonly ProyectoGastosTestContext _context;

        public ReporteTest()
        {
            var _serviceProvider = ProgramTest.GetServices(true);
            var _context = _serviceProvider.GetRequiredService<ProyectoGastosTestContext>();

            var _reporteService = _serviceProvider.GetRequiredService<IReporteService>();
            var _utilidadesService = _serviceProvider.GetRequiredService<IUtilidadesService>();


            _controller = new ReporteController(
                _reporteService, _utilidadesService
                );

        }

        private async void ArrangeAddSetup()
        {
            var usuarios = new List<AuthenticationUsuario>
            {
                new() { Id = 1, Username = "Test 1", Email = "usuario_test1@example.com", Password = "password1",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
                new() { Id = 2, Username = "Test 2", Email = "usuario_test2@example.com", Password = "password2",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
            };
            await _context.AuthenticationUsuarios.AddRangeAsync(usuarios);
            await _context.SaveChangesAsync();

            var monedas = new List<GastosMonedum>
            {
                new() {Id = 1, Descripcion = "ARS", FechaCreacion = DateTime.UtcNow, Baja = false},
                new() {Id = 2, Descripcion = "USD", FechaCreacion = DateTime.UtcNow, Baja = false}
            };



            await _context.GastosMoneda.AddRangeAsync(monedas);
            await _context.SaveChangesAsync();

            var categoriasIngresos = new List<GastosCategoriaigreso>
            {
                new() {Id = 1, Descripcion ="Trabajo", FechaCreacion = DateTime.Now, Baja = false},
                new() {Id = 2, Descripcion ="Inversiones", FechaCreacion = DateTime.Now, Baja = false},
            };
            await _context.GastosCategoriaigresos.AddRangeAsync(categoriasIngresos);
            await _context.SaveChangesAsync();

            var categoriasGastos = new List<GastosCategoriagasto>
            {
                new() {Id = 1, Descripcion="Supermercado", FechaCreacion=DateTime.Now, Baja=false},
                new() {Id = 2, Descripcion="Servicios Digitales", FechaCreacion=DateTime.Now, Baja=false},
            };
            await _context.GastosCategoriagastos.AddRangeAsync(categoriasGastos);
            await _context.SaveChangesAsync();

            var subcategoriasIngresos = new List<GastosSubcategoriaingreso>
            {
                new() {Id = 1, Descripcion="Principal", CategoriaIngreso = categoriasIngresos[0], CategoriaIngresoId = categoriasIngresos[0].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false},
                new() {Id = 2, Descripcion="Secundario", CategoriaIngreso = categoriasIngresos[0], CategoriaIngresoId = categoriasIngresos[0].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false},
                new() {Id = 3, Descripcion="Dividendos", CategoriaIngreso = categoriasIngresos[1], CategoriaIngresoId = categoriasIngresos[1].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false},
                new() {Id = 4, Descripcion="Dividendos", CategoriaIngreso = categoriasIngresos[1], CategoriaIngresoId = categoriasIngresos[1].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[1], UsuarioId=usuarios[1].Id,Baja = false},
            };
            await _context.GastosSubcategoriaingresos.AddRangeAsync(subcategoriasIngresos);
            await _context.SaveChangesAsync();


            var subcategoriasGastos = new List<GastosSubcategoriagasto>
            {
                new() {Id = 1, Descripcion="La Anonima", CategoriaGasto = categoriasGastos[0], CategoriaGastoId = categoriasGastos[0].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false},
                new() {Id = 2, Descripcion="Libertad", CategoriaGasto = categoriasGastos[0], CategoriaGastoId = categoriasGastos[0].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false},
                new() {Id = 3, Descripcion="Netflix", CategoriaGasto = categoriasGastos[1], CategoriaGastoId = categoriasGastos[1].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false},
                new() {Id = 4, Descripcion="Netflix", CategoriaGasto = categoriasGastos[1], CategoriaGastoId = categoriasGastos[1].Id, FechaCreacion = DateTime.Now, Usuario = usuarios[1], UsuarioId=usuarios[1].Id,Baja = false},
            };
            await _context.GastosSubcategoriagastos.AddRangeAsync(subcategoriasGastos);
            await _context.SaveChangesAsync();

            var ingresos = new List<GastosIngreso>
            {
                new() {
                    Id = 1, Descripcion="sueldo", Importe =200000.0M, Moneda = monedas[0], MonedaId = monedas[0].Id,
                    CategoriaIngreso = categoriasIngresos[0], SubcategoriaIngreso = subcategoriasIngresos[0],
                    SubcategoriaIngresoId= subcategoriasIngresos[0].Id, CategoriaIngresoId = categoriasIngresos[0].Id,
                    FechaCreacion = new DateTime(day:01, month:10, year: 2024), Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false
                },
                new() {
                    Id = 1, Descripcion="sueldo changa", Importe = 100000.0M, Moneda = monedas[0], MonedaId = monedas[0].Id,
                    CategoriaIngreso = categoriasIngresos[0], SubcategoriaIngreso = subcategoriasIngresos[1],
                    SubcategoriaIngresoId= subcategoriasIngresos[1].Id, CategoriaIngresoId = categoriasIngresos[0].Id,
                    FechaCreacion = new DateTime(day : 01, month : 10, year : 2024), Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false
                },
                new() {
                    Id = 1, Descripcion="sueldo borrado", Importe = 100000.0M, Moneda = monedas[0], MonedaId = monedas[0].Id,
                    CategoriaIngreso = categoriasIngresos[0], SubcategoriaIngreso = subcategoriasIngresos[0],
                    SubcategoriaIngresoId= subcategoriasIngresos[0].Id, CategoriaIngresoId = categoriasIngresos[0].Id,
                    FechaCreacion = new DateTime(day:01, month:10, year: 2024), Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = true
                },
                new() {
                    Id = 1, Descripcion="Dividendos", Importe = 100000.0M, Moneda = monedas[1], MonedaId = monedas[1].Id,
                    CategoriaIngreso = categoriasIngresos[1], SubcategoriaIngreso = subcategoriasIngresos[2],
                    SubcategoriaIngresoId= subcategoriasIngresos[2].Id, CategoriaIngresoId = categoriasIngresos[1].Id,
                    FechaCreacion = new DateTime(day : 03, month : 10, year : 2024), Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false
                },
                new() {
                    Id = 1, Descripcion="Dividendos", Importe = 750000.0M, Moneda = monedas[1], MonedaId = monedas[1].Id,
                    CategoriaIngreso = categoriasIngresos[1], SubcategoriaIngreso = subcategoriasIngresos[3],
                    SubcategoriaIngresoId= subcategoriasIngresos[3].Id, CategoriaIngresoId = categoriasIngresos[1].Id,
                    FechaCreacion = new DateTime(day : 03, month : 10, year : 2024), Usuario = usuarios[1], UsuarioId=usuarios[1].Id,Baja = false
                },
                new() {
                    Id = 1, Descripcion="Dividendos", Importe = 100000.0M, Moneda = monedas[1], MonedaId = monedas[1].Id,
                    CategoriaIngreso = categoriasIngresos[1], SubcategoriaIngreso = subcategoriasIngresos[2],
                    SubcategoriaIngresoId= subcategoriasIngresos[2].Id, CategoriaIngresoId = categoriasIngresos[1].Id,
                    FechaCreacion = new DateTime(day : 03, month : 10, year : 2024), Usuario = usuarios[0], UsuarioId=usuarios[0].Id,Baja = false
                },
                new() {
                    Id = 1, Descripcion="Dividendos", Importe = 750000.0M, Moneda = monedas[1], MonedaId = monedas[1].Id,
                    CategoriaIngreso = categoriasIngresos[1], SubcategoriaIngreso = subcategoriasIngresos[3],
                    SubcategoriaIngresoId= subcategoriasIngresos[3].Id, CategoriaIngresoId = categoriasIngresos[1].Id,
                    FechaCreacion = new DateTime(day : 03, month : 10, year : 2024), Usuario = usuarios[1], UsuarioId=usuarios[1].Id,Baja = false
                },
                new() {
                    Id = 1, Descripcion="Dividendo Borrado", Importe = 50000.0M, Moneda = monedas[1], MonedaId = monedas[1].Id,
                    CategoriaIngreso = categoriasIngresos[1], SubcategoriaIngreso = subcategoriasIngresos[3],
                    SubcategoriaIngresoId= subcategoriasIngresos[3].Id, CategoriaIngresoId = categoriasIngresos[1].Id,
                    FechaCreacion = new DateTime(day : 01, month : 10, year : 2024), Usuario = usuarios[1], UsuarioId=usuarios[1].Id,Baja = true
                },
            };
            await _context.GastosIngresos.AddRangeAsync(ingresos);
            await _context.SaveChangesAsync();

            var importeIngresosUsuario1 = ingresos
                .Where(i => i.UsuarioId == usuarios[0].Id &&
                i.Baja == false)
                .Select(i => i.Importe)
                .Sum();

            var importeIngresosUsuario2 = ingresos
                .Where(i => i.UsuarioId == usuarios[1].Id &&
                i.Baja == false)
                .Select(i => i.Importe)
                .Sum();



            var gastos = new List<GastosGasto>
            {
               new () {
                   Id = 1, Descripcion="Comida 15 dias", Importe=100000.0M, CategoriaGasto = categoriasGastos[0],
                   CategoriaGastoId = categoriasGastos[0].Id, SubcategoriaGasto=subcategoriasGastos[0],
                   SubcategoriaGastoId=subcategoriasGastos[0].Id ,Moneda=monedas[0], MonedaId=monedas[0].Id,
                   Usuario=usuarios[0], UsuarioId=usuarios[0].Id, Baja=false, FechaCreacion=new DateTime(day:01, month:10, year: 2024)
               },
               new () {
                   Id = 1, Descripcion="Comida 5 dias", Importe=50000.0M, CategoriaGasto = categoriasGastos[0],
                   CategoriaGastoId = categoriasGastos[0].Id, SubcategoriaGasto=subcategoriasGastos[1],
                   SubcategoriaGastoId=subcategoriasGastos[1].Id ,Moneda=monedas[0], MonedaId=monedas[0].Id,
                   Usuario=usuarios[0], UsuarioId=usuarios[0].Id, Baja=false, FechaCreacion=new DateTime(day:03, month:10, year: 2024)
               },
               new () {
                   Id = 1, Descripcion="suscripción", Importe=10000.0M, CategoriaGasto = categoriasGastos[1],
                   CategoriaGastoId = categoriasGastos[1].Id, SubcategoriaGasto=subcategoriasGastos[3],
                   SubcategoriaGastoId=subcategoriasGastos[3].Id ,Moneda=monedas[1], MonedaId=monedas[1].Id,
                   Usuario=usuarios[1], UsuarioId=usuarios[1].Id, Baja=false, FechaCreacion=new DateTime(day:01, month:10, year: 2024)
               },

               new () {
                   Id = 1, Descripcion="Comida 15 dias", Importe=100000.0M, CategoriaGasto = categoriasGastos[0],
                   CategoriaGastoId = categoriasGastos[0].Id, SubcategoriaGasto=subcategoriasGastos[0],
                   SubcategoriaGastoId=subcategoriasGastos[0].Id ,Moneda=monedas[0], MonedaId=monedas[0].Id,
                   Usuario=usuarios[0], UsuarioId=usuarios[0].Id, Baja=false, FechaCreacion=new DateTime(day:01, month:10, year: 2024)
               },
               new () {
                   Id = 1, Descripcion="Comida 5 dias", Importe=50000.0M, CategoriaGasto = categoriasGastos[0],
                   CategoriaGastoId = categoriasGastos[0].Id, SubcategoriaGasto=subcategoriasGastos[1],
                   SubcategoriaGastoId=subcategoriasGastos[1].Id ,Moneda=monedas[0], MonedaId=monedas[0].Id,
                   Usuario=usuarios[0], UsuarioId=usuarios[0].Id, Baja=false, FechaCreacion=new DateTime(day:03, month:10, year: 2024)
               },
               new () {
                   Id = 1, Descripcion="suscripción", Importe=10000.0M, CategoriaGasto = categoriasGastos[1],
                   CategoriaGastoId = categoriasGastos[1].Id, SubcategoriaGasto=subcategoriasGastos[3],
                   SubcategoriaGastoId=subcategoriasGastos[3].Id ,Moneda=monedas[1], MonedaId=monedas[1].Id,
                   Usuario=usuarios[1], UsuarioId=usuarios[1].Id, Baja=false, FechaCreacion=new DateTime(day:01, month:10, year: 2024)
               },
               new () {
                   Id = 1, Descripcion="suscripción", Importe=100000.0M, CategoriaGasto = categoriasGastos[1],
                   CategoriaGastoId = categoriasGastos[1].Id, SubcategoriaGasto=subcategoriasGastos[2],
                   SubcategoriaGastoId=subcategoriasGastos[2].Id ,Moneda=monedas[1], MonedaId=monedas[1].Id,
                   Usuario=usuarios[0], UsuarioId=usuarios[0].Id, Baja=false, FechaCreacion=new DateTime(day : 05, month : 10, year : 2024)
               },
               new () {
                   Id = 1, Descripcion="Comida 15 dias borrado", Importe=100000.0M, CategoriaGasto = categoriasGastos[0],
                   CategoriaGastoId = categoriasGastos[0].Id, SubcategoriaGasto=subcategoriasGastos[0],
                   SubcategoriaGastoId=subcategoriasGastos[0].Id ,Moneda=monedas[0], MonedaId=monedas[0].Id,
                   Usuario=usuarios[0], UsuarioId=usuarios[0].Id, Baja=true, FechaCreacion=new DateTime(day : 01, month : 10, year : 2024)
               },
               new () {
                   Id = 1, Descripcion="Comida 15 dias borrado", Importe=100000.0M, CategoriaGasto = categoriasGastos[0],
                   CategoriaGastoId = categoriasGastos[0].Id, SubcategoriaGasto=subcategoriasGastos[0],
                   SubcategoriaGastoId=subcategoriasGastos[0].Id ,Moneda=monedas[0], MonedaId=monedas[0].Id,
                   Usuario=usuarios[1], UsuarioId=usuarios[1].Id, Baja=true, FechaCreacion=new DateTime(day : 01, month : 10, year : 2024)
               },
            };

            await _context.GastosGastos.AddRangeAsync(gastos);
            await _context.SaveChangesAsync();

            var importeGastosUsuario1 = gastos
                .Where(g => g.UsuarioId == usuarios[0].Id && g.Baja == false)
                .Select(g => g.Importe)
                .Sum();

            var importeGastosUsuario2 = gastos
                .Where(g => g.UsuarioId == usuarios[1].Id && g.Baja == false)
                .Select(g => g.Importe)
                .Sum();

            var importesUsuario1 = new Dictionary<string, decimal>
            {
                { "ImporteIngresos", importeIngresosUsuario1 },
                { "ImporteGastos", importeGastosUsuario1 },
                { "ImporteBalance", importeIngresosUsuario1 - importeGastosUsuario1 }
            };

            var importesUsuario2 = new Dictionary<string, decimal>
            {
                { "ImporteIngresos", importeIngresosUsuario2 },
                { "ImporteGastos", importeGastosUsuario2 },
                { "ImporteBalance", importeIngresosUsuario2 - importeGastosUsuario2 }
            };
        }

    }
}
