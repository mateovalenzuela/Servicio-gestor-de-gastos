using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            => await _context.GastosGastos.Where(g => g.Baja == false).ToListAsync();

        public async Task<IEnumerable<GastosGasto>> GetActiveByCategoriaGasto(long idCategoriaGasto)
        {
            var gastos = await _context.GastosGastos.Where(g => g.Baja == false &&
                                                                g.CategoriaGastoId == idCategoriaGasto
                                                                ).ToListAsync();
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
            var gastos = await _context.GastosGastos.Where(g => g.Baja == false &&
                                                                g.SubcategoriaGastoId == idSubCategoriaGasto
                                                                ).ToListAsync();
            return gastos;
        }

        public async Task<IEnumerable<GastosGasto>> GetActiveByUser(long idUser)
        {
            var gastos = await _context.GastosGastos.Where(g => g.Baja == false &&
                                                                g.UsuarioId == idUser
                                                                ).ToListAsync();
            return gastos;
        }

        public async Task<IEnumerable<GastosGasto>> GetActiveByUserAndCategoriaGasto(long idUser, long idCategoriaGasto)
        {
            var gastos = await _context.GastosGastos.Where(g => g.Baja == false &&
                                                                g.UsuarioId == idUser &&        
                                                                g.CategoriaGastoId == idCategoriaGasto
                                                                ).ToListAsync();
            return gastos;
        }

        public async Task<GastosGasto> GetById(long id)
            => await _context.GastosGastos.FindAsync(id);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public void Update(GastosGasto entity)
        {
            _context.GastosGastos.Attach(entity);
            _context.GastosGastos.Entry(entity).State = EntityState.Modified;
        }
    }
}
