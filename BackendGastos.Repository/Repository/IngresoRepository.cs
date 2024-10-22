using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public class IngresoRepository : IIngresoRepository
    {
        private ProyectoGastosTestContext _context;
        public IngresoRepository(ProyectoGastosTestContext context) 
        {
            _context = context;
        }

        public async Task Add(GastosIngreso entity)
            => await _context.GastosIngresos.AddAsync(entity);

        public void BajaLogica(GastosIngreso entity)
        {
            entity.Baja = true;
            _context.GastosIngresos.Attach(entity);
            _context.GastosIngresos.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(GastosIngreso entity)
            => _context.GastosIngresos.Remove(entity);

        public async Task<IEnumerable<GastosIngreso>> Get()
            => await _context.GastosIngresos
            .Include(c => c.CategoriaIngreso)
            .Include(c => c.SubcategoriaIngreso)
            .OrderByDescending(c => c.Id)
            .ToListAsync();

        public async Task<IEnumerable<GastosIngreso>> GetActive()
            => await _context.GastosIngresos
            .Where(c => c.Baja == false)
            .Include(c => c.CategoriaIngreso)
            .Include(c => c.SubcategoriaIngreso)
            .OrderByDescending(c => c.Id)
            .ToListAsync();

        public async Task<IEnumerable<GastosIngreso>> GetActiveByCategoriaIngreso(long idCategoriaIngreso)
        {
            var ingresos = await _context.GastosIngresos
                .Where(c => c.Baja == false && c.CategoriaIngresoId == idCategoriaIngreso)
                .Include(c => c.CategoriaIngreso)
                .Include(c => c.SubcategoriaIngreso)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return ingresos;
        }

        public async Task<GastosIngreso> GetActiveById(long id)
        {
            var ingreso = await _context.GastosIngresos.FindAsync(id);

            if (ingreso == null) return null;

            if (ingreso.Baja == false)
            {
                return ingreso;
            }
            return null;
        }

        public async Task<IEnumerable<GastosIngreso>> GetActiveBySubCategoriaIngreso(long idSubCategoriaIngreso)
        {
            var ingresos = await _context.GastosIngresos
                .Where(c => c.Baja == false && c.SubcategoriaIngresoId == idSubCategoriaIngreso)
                .Include(c => c.CategoriaIngreso)
                .Include(c => c.SubcategoriaIngreso)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return ingresos;
        }

        public async Task<IEnumerable<GastosIngreso>> GetActiveByUser(long idUser)
        {
            var ingresos = await _context.GastosIngresos
                .Where(c => c.Baja == false && c.UsuarioId == idUser)
                .Include(c => c.CategoriaIngreso)
                .Include(c => c.SubcategoriaIngreso)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return ingresos;
        }

        public async Task<GastosIngreso> GetById(long id)
            => await _context.GastosIngresos.FindAsync(id);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public void Update(GastosIngreso entity)
        {
            _context.GastosIngresos.Attach(entity);
            _context.GastosIngresos.Entry(entity).State = EntityState.Modified;
        }
        public async Task<IEnumerable<GastosIngreso>> GetActiveByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso)
        {
            var ingresos = await _context.GastosIngresos
                .Where(c => c.Baja == false && c.UsuarioId == idUser && c.CategoriaIngresoId == idCategoriaIngreso)
                .Include(c => c.CategoriaIngreso)
                .Include(c => c.SubcategoriaIngreso)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return ingresos;
        }

        public async Task<IEnumerable<GastosIngreso>> SearchActiveByDescripcionParcial(long idUser, string descripcion, int limit)
        {
            var ingresos = await _context.GastosIngresos
                .Where(g => g.UsuarioId == idUser && g.Baja == false && g.Descripcion.ToLower().Contains(descripcion.ToLower()))
                .GroupBy(g => g.Descripcion)
                .Select(g => g.OrderByDescending(x => x.Id).First()) // Ordena cada grupo por Id y selecciona el último
                .Take(limit)
                .ToListAsync();

            return ingresos;
        }

    }
}
