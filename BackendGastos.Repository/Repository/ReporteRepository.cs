using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public class ReporteRepository : IReporteRepository
    {
        private ProyectoGastosTestContext _context;

        public ReporteRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, decimal>> GetImporteTotalDeGastosEIngresos(long idUser)
        {
            var importesDict = new Dictionary<string, decimal>();

            var importeTotalIngresos = await _context.GastosIngresos
                .Where(i => i.UsuarioId == idUser &&
                i.Baja == false)
                .Select(i => i.Importe)
                .SumAsync();
            importesDict.Add("ImporteIngresos", importeTotalIngresos);

            var importeTotalGastos = await _context.GastosGastos
                .Where(g => g.UsuarioId == idUser && g.Baja == false)
                .Select (g => g.Importe)
                .SumAsync();
            importesDict.Add("ImporteGastos", importeTotalGastos);

            importesDict.Add("ImporteBalance", importeTotalIngresos - importeTotalGastos);
            return importesDict;
        }

        public async Task<Dictionary<string, decimal>> GetImporteTotalDeGastosEIngresos(long idUser, DateTime fechaLimite, DateTime fechaInicial)
        {
            var importesDict = new Dictionary<string, decimal>();

            var importeTotalIngresos = await _context.GastosIngresos
                .Where(i => i.UsuarioId == idUser && i.Baja == false && 
                i.FechaCreacion >= fechaInicial && 
                i.FechaCreacion <= fechaLimite)
                .Select(i => i.Importe)
                .SumAsync();
            importesDict.Add("ImporteIngresos", importeTotalIngresos);

            var importeTotalGastos = await _context.GastosGastos
                .Where(g => g.UsuarioId == idUser && g.Baja == false && 
                g.FechaCreacion >= fechaInicial && 
                g.FechaCreacion <= fechaLimite)
                .Select(g => g.Importe)
                .SumAsync();
            importesDict.Add("ImporteGastos", importeTotalGastos);

            importesDict.Add("ImporteBalance", importeTotalIngresos - importeTotalGastos);
            return importesDict;
        }

        public async Task<List<BalanceDiario>> GetBalanceDiarioPorUsuario(long idUser, DateTime fechaLimite, DateTime fechaInicial)
        {
            // Obtener ingresos agrupados por día
            var ingresosPorDia = await _context.GastosIngresos
                .Where(i => i.UsuarioId == idUser &&
                            i.Baja == false &&
                            i.FechaCreacion.Date >= fechaInicial.Date &&
                            i.FechaCreacion.Date <= fechaLimite.Date)
                .GroupBy(i => i.FechaCreacion.Date) // Eliminar uso de .Date aquí, ya que se filtrará en el Where
                .Select(g => new
                {
                    Fecha = g.Key,
                    TotalIngresos = g.Sum(i => i.Importe),
                    TotalGastos = 0M
                })
                .ToListAsync();

            // Obtener gastos agrupados por día
            var gastosPorDia = await _context.GastosGastos
                .Where(g => g.UsuarioId == idUser &&
                            g.Baja == false &&
                            g.FechaCreacion.Date >= fechaInicial.Date &&
                            g.FechaCreacion.Date <= fechaLimite.Date)
                .GroupBy(g => g.FechaCreacion.Date)
                .Select(g => new
                {
                    Fecha = g.Key,
                    TotalIngresos = 0M,
                    TotalGastos = g.Sum(g => g.Importe)
                })
                .ToListAsync();

            // Unir ingresos y gastos por fecha
            var balanceDiario = ingresosPorDia
                .Union(gastosPorDia)
                .GroupBy(x => x.Fecha.Date)
                .Select(g => new BalanceDiario
                {
                    Fecha = g.Key.Date,
                    Balance = g.Sum(x => x.TotalIngresos) - g.Sum(x => x.TotalGastos)
                })
                .OrderBy(b => b.Fecha)
                .ToList();

            return balanceDiario;
        }


    }
}
