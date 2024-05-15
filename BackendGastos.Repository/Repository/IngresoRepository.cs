using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    internal class IngresoRepository : IIngresoRepository<GastosIngreso>
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
            => await _context.GastosIngresos.ToListAsync();

        public async Task<IEnumerable<GastosIngreso>> GetActive()
            => await _context.GastosIngresos.Where(c => c.Baja == false).ToListAsync();

        public async Task<IEnumerable<GastosIngreso>> GetActiveByCategoriaIngreso(long idCategoriaIngreso)
        {
            var ingresos = await _context.GastosIngresos.Where(c => c.Baja == false &&
                                                                    c.CategoriaIngresoId == idCategoriaIngreso
                                                                    ).ToListAsync();
            return ingresos;
        }

        public async Task<GastosIngreso> GetActiveById(long id)
        {
            var ingreso = await _context.GastosIngresos.FindAsync(id);
            if (ingreso.Baja == false)
            {
                return ingreso;
            }
            return null;
        }

        public async Task<IEnumerable<GastosIngreso>> GetActiveBySubCategoriaIngreso(long idSubCategoriaIngreso)
        {
            var ingresos = await _context.GastosIngresos.Where(c => c.Baja == false &&
                                                                    c.SubcategoriaIngresoId == idSubCategoriaIngreso
                                                                    ).ToListAsync();
            return ingresos;
        }

        public async Task<IEnumerable<GastosIngreso>> GetActiveByUser(long idUser)
        {
            var ingresos = await _context.GastosIngresos.Where(c => c.Baja == false &&
                                                                    c.UsuarioId == idUser
                                                                    ).ToListAsync();
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
    }
}
