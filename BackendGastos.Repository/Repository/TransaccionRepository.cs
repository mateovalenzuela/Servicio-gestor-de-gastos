using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendGastos.Repository.Repository
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private ProyectoGastosTestContext _context;

        public TransaccionRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GastosEIngresos>> GetActiveGastosEIngresos(long idUser, int cantidad)
        {
            var gastos = await _context.GastosGastos.Where(g => g.UsuarioId == idUser && g.Baja == false)
                .Include(g => g.CategoriaGasto)
                .Include(g => g.SubcategoriaGasto)
                .OrderByDescending(g => g.Id)
                .ToListAsync();

            var ingresos = await _context.GastosIngresos.Where(i => i.UsuarioId == idUser && i.Baja == false)
                .Include(i => i.CategoriaIngreso)
                .Include(i => i.SubcategoriaIngreso)
                .OrderByDescending(i => i.Id)
                .ToListAsync();

            var listaDeTransaciones = new List<GastosEIngresos>();

            foreach (var gasto in gastos)
            {
                var transaccion = new GastosEIngresos
                {
                    IsIngreso = false,
                    Gasto = gasto,
                    Ingreso = null
                };
                listaDeTransaciones.Add(transaccion);
            }

            foreach (var ingreso in ingresos)
            {
                var transaccion = new GastosEIngresos
                {
                    IsIngreso = true,
                    Ingreso = ingreso,
                    Gasto = null
                };
                listaDeTransaciones.Add(transaccion);
            }

            listaDeTransaciones = listaDeTransaciones
                .OrderByDescending(t => t.IsIngreso ? t.Ingreso?.FechaCreacion : t.Gasto?.FechaCreacion)
                .ToList();

            if (cantidad > 0)
            {
                listaDeTransaciones = listaDeTransaciones.Take(cantidad).ToList();
            }

            return listaDeTransaciones;
        }

        public async Task<Dictionary<string, decimal>> GetImportesGastosEIngresos(long idUser)
        {
            var importeGastos = await _context.GastosGastos
                .Where(g => g.UsuarioId == idUser && g.Baja == false)
                .Select(g => g.Importe)
                .SumAsync();

            var importeIngresos = await _context.GastosIngresos
                .Where(i => i.UsuarioId == idUser && i.Baja == false)
                .Select(i => i.Importe)
                .SumAsync();

            var dicImportes = new Dictionary<string, decimal>
            {
                { "ImporteGastos", importeGastos },
                { "ImporteIngresos", importeIngresos },
                { "ImporteTotal", importeIngresos - importeGastos }
            };

            return dicImportes;
        }
    }
}
