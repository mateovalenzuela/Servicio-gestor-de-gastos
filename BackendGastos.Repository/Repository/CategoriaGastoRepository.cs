using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public class CategoriaGastoRepository : ICategoriaGastoRepository
    {
        private ProyectoGastosTestContext _context;
        public CategoriaGastoRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }


        public async Task Add(GastosCategoriagasto categoriaGasto)
            => await _context.GastosCategoriagastos.AddAsync(categoriaGasto);

        public void Delete(GastosCategoriagasto categoriaGasto)
        {
            _context.GastosCategoriagastos.Remove(categoriaGasto);
        }

        public async Task<IEnumerable<GastosCategoriagasto>> Get()
        {
            var categoriasGasto = await _context.GastosCategoriagastos.ToListAsync();
            return categoriasGasto;
        }

        public async Task<IEnumerable<GastosCategoriagasto>> GetActive()
        {
            var categoriasGasto = await _context.GastosCategoriagastos.Where(c => c.Baja == false).ToListAsync();
            return categoriasGasto;
        }

        public async Task<GastosCategoriagasto> GetById(long id)
        {
            var categoriaGasto = await _context.GastosCategoriagastos.FindAsync(id);
            return categoriaGasto;
        }

        public async Task<GastosCategoriagasto> GetActiveById(long id)
        {
            var categoriaGasto = await _context.GastosCategoriagastos.FindAsync(id);

            if (categoriaGasto == null) return null;

            if (categoriaGasto.Baja == false)
            {
                return categoriaGasto;
            }
            return null;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(GastosCategoriagasto categoriaGasto)
        {
            _context.GastosCategoriagastos.Attach(categoriaGasto);
            _context.GastosCategoriagastos.Entry(categoriaGasto).State = EntityState.Modified;
        }

        public void BajaLogica(GastosCategoriagasto categoriaGasto)
        {
            categoriaGasto.Baja = true;
            _context.GastosCategoriagastos.Attach(categoriaGasto);
            _context.GastosCategoriagastos.Entry(categoriaGasto).State = EntityState.Modified;
        }

        public IEnumerable<GastosCategoriagasto> Search(Func<GastosCategoriagasto, bool> filter)
            => _context.GastosCategoriagastos.Where(filter).ToList();
    }
}
