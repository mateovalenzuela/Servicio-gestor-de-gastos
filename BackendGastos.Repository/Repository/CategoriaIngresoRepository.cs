using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public class CategoriaIngresoRepository : IRepository<GastosCategoriaigreso>
    {
        private ProyectoGastosTestContext _context;
        public CategoriaIngresoRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }


        public async Task Add(GastosCategoriaigreso categoriaIngreso)
            => await _context.GastosCategoriaigresos.AddAsync(categoriaIngreso);

        public void Delete(GastosCategoriaigreso categoriaIngreso)
        {
            _context.GastosCategoriaigresos.Remove(categoriaIngreso);
        }

        public async Task<IEnumerable<GastosCategoriaigreso>> Get()
        {
            var categoriasIngreso = await _context.GastosCategoriaigresos.ToListAsync();
            return categoriasIngreso;
        }

        public async Task<IEnumerable<GastosCategoriaigreso>> GetActive()
        {
            var categoriasIngreso = await _context.GastosCategoriaigresos.Where(c => c.Baja == false).ToListAsync();
            return categoriasIngreso;
        }

        public async Task<GastosCategoriaigreso> GetById(long id)
        {
            var categoriaIngreso = await _context.GastosCategoriaigresos.FindAsync(id);
            return categoriaIngreso;
        }

        public async Task<GastosCategoriaigreso> GetActiveById(long id)
        {
            var categoriaIngreso = await _context.GastosCategoriaigresos.FindAsync(id);
            if (categoriaIngreso.Baja == false)
            {
                return categoriaIngreso;
            }
            return null;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(GastosCategoriaigreso categoriaIngreso)
        {
            _context.GastosCategoriaigresos.Attach(categoriaIngreso);
            _context.GastosCategoriaigresos.Entry(categoriaIngreso).State = EntityState.Modified;
        }

        public void BajaLogica(GastosCategoriaigreso categoriaIngreso)
        {
            categoriaIngreso.Baja = true;
            _context.GastosCategoriaigresos.Attach(categoriaIngreso);
            _context.GastosCategoriaigresos.Entry(categoriaIngreso).State = EntityState.Modified;
        }
    }
}
