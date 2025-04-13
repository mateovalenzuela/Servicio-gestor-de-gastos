using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendGastos.Repository.Repository
{
    public class MonedaRepository : IMonedaRepository
    {
        private ProyectoGastosTestContext _context;
        public MonedaRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }

        public async Task Add(GastosMonedum entity)
            => await _context.GastosMoneda.AddAsync(entity);

        public void BajaLogica(GastosMonedum entity)
        {
            entity.Baja = true;
            _context.GastosMoneda.Attach(entity);
            _context.GastosMoneda.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(GastosMonedum entity)
            => _context.GastosMoneda.Remove(entity);

        public async Task<IEnumerable<GastosMonedum>> Get()
            => await _context.GastosMoneda.ToListAsync();

        public async Task<IEnumerable<GastosMonedum>> GetActive()
            => await _context.GastosMoneda.Where(m => m.Baja == false).ToListAsync();

        public async Task<GastosMonedum> GetActiveById(long id)
        {
            var moneda = await _context.GastosMoneda.FindAsync(id);

            if (moneda == null) return null;

            if (moneda.Baja == false)
            {
                return moneda;
            }
            return null;
        }

        public async Task<GastosMonedum> GetById(long id)
            => await _context.GastosMoneda.FindAsync(id);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<GastosMonedum> Search(Func<GastosMonedum, bool> filter)
            => _context.GastosMoneda.Where(filter).ToList();

        public void Update(GastosMonedum entity)
        {
            _context.GastosMoneda.Attach(entity);
            _context.GastosMoneda.Entry(entity).State = EntityState.Modified;
        }
    }
}
