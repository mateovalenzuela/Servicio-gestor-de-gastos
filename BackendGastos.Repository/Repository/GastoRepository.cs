using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendGastos.Repository.Repository
{
    public class GastoRepository : IGastoRepository
    {
        private ProyectoGastosTestContext _context;

        public GastoRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }

        public async Task Add(GastosGasto entity)
            => await _context.GastosGastos.AddAsync(entity);

        public void BajaLogica(GastosGasto entity)
        {
            entity.Baja = true;
            _context.GastosGastos.Attach(entity);
            _context.GastosGastos.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(GastosGasto entity)
            => _context.GastosGastos.Remove(entity);

        public async Task<IEnumerable<GastosGasto>> Get()
            => await _context.GastosGastos.ToListAsync();

        public async Task<IEnumerable<GastosGasto>> GetActive()
            => await _context.GastosGastos
            .Where(g => g.Baja == false)
            .Include(g => g.CategoriaGasto)
            .Include(g => g.SubcategoriaGasto)
            .OrderByDescending(c => c.Id)
            .ToListAsync();

        public async Task<IEnumerable<GastosGasto>> GetActiveByCategoriaGasto(long idCategoriaGasto)
        {
            var gastos = await _context.GastosGastos
                .Where(g => g.Baja == false && g.CategoriaGastoId == idCategoriaGasto)
                .Include(g => g.CategoriaGasto)
                .Include(g => g.SubcategoriaGasto)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return gastos;
        }

        public async Task<GastosGasto> GetActiveById(long id)
        {
            var gasto = await _context.GastosGastos.FindAsync(id);

            if (gasto == null) return null;

            if (gasto.Baja == false)
            {
                return gasto;
            }
            return null;
        }

        public async Task<IEnumerable<GastosGasto>> GetActiveBySubCategoriaGasto(long idSubCategoriaGasto)
        {
            var gastos = await _context.GastosGastos
                .Where(g => g.Baja == false && g.SubcategoriaGastoId == idSubCategoriaGasto)
                .Include(g => g.CategoriaGasto)
                .Include(g => g.SubcategoriaGasto)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return gastos;
        }

        public async Task<IEnumerable<GastosGasto>> GetActiveByUser(long idUser)
        {
            var gastos = await _context.GastosGastos
                .Where(g => g.Baja == false && g.UsuarioId == idUser)
                .Include(g => g.CategoriaGasto)
                .Include(g => g.SubcategoriaGasto)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return gastos;
        }

        public async Task<IEnumerable<GastosGasto>> GetActiveByUserAndCategoriaGasto(long idUser, long idCategoriaGasto)
        {
            var gastos = await _context.GastosGastos
                .Where(g => g.Baja == false && g.UsuarioId == idUser && g.CategoriaGastoId == idCategoriaGasto)
                .Include(g => g.CategoriaGasto)
                .Include(g => g.SubcategoriaGasto)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return gastos;
        }

        public async Task<GastosGasto> GetById(long id)
            => await _context.GastosGastos.FindAsync(id);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<GastosGasto> Search(Func<GastosGasto, bool> filter, int limit)
            => _context.GastosGastos.Where(filter).Take(limit).ToList();

        public async Task<IEnumerable<GastosGasto>> SearchActiveByDescripcionParcial(long idUser, string descripcion, int limit)
        {
            var gastos = await _context.GastosGastos
                .Where(g => g.UsuarioId == idUser && g.Baja == false && g.Descripcion.ToLower().Contains(descripcion.ToLower()))
                .GroupBy(g => g.Descripcion)
                .Select(g => g.OrderByDescending(x => x.Id).First()) // Ordena cada grupo por Id y selecciona el último
                .Take(limit)
                .ToListAsync();

            return gastos;
        }

        public void Update(GastosGasto entity)
        {
            _context.GastosGastos.Attach(entity);
            _context.GastosGastos.Entry(entity).State = EntityState.Modified;
        }
    }
}
