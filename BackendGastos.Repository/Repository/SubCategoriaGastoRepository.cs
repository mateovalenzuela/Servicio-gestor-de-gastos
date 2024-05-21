using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public class SubCategoriaGastoRepository : ISubCategoriaGastoRepository<GastosSubcategoriagasto>
    {
        private ProyectoGastosTestContext _context;
        public SubCategoriaGastoRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }


        public async Task Add(GastosSubcategoriagasto subCategoriaGasto)
            => await _context.GastosSubcategoriagastos.AddAsync(subCategoriaGasto);

        public void Delete(GastosSubcategoriagasto subCategoriaGasto)
        {
            _context.GastosSubcategoriagastos.Remove(subCategoriaGasto);
        }

        public async Task<IEnumerable<GastosSubcategoriagasto>> Get()
        {
            var subCategoriasGasto = await _context.GastosSubcategoriagastos.ToListAsync();
            return subCategoriasGasto;
        }

        public async Task<IEnumerable<GastosSubcategoriagasto>> GetActive()
        {
            var subCategoriasGasto = await _context.GastosSubcategoriagastos.Where(c => c.Baja == false).ToListAsync();
            return subCategoriasGasto;
        }

        public async Task<IEnumerable<GastosSubcategoriagasto>> GetActiveByUser(long idUser)
        {
            var subCategoriasGasto = await _context.GastosSubcategoriagastos.Where(c => c.Baja == false && c.UsuarioId == idUser).ToListAsync();
            return subCategoriasGasto;
        }

        public async Task<IEnumerable<GastosSubcategoriagasto>> GetActiveByCategoriaGasto(long idCategoriaGasto)
        {
            var subCategoriasGasto = await _context.GastosSubcategoriagastos.Where(c => c.Baja == false &&
                                                                                       c.CategoriaGastoId == idCategoriaGasto
                                                                                       ).ToListAsync();
            return subCategoriasGasto;
        }

        public async Task<IEnumerable<GastosSubcategoriagasto>> GetActiveByUserAndCategoriaGasto(long idUser, long idCategoriaGasto)
        {
            var subCategoriasGasto = await _context.GastosSubcategoriagastos.Where(c => c.Baja == false &&
                                                                                       c.UsuarioId == idUser &&
                                                                                       c.CategoriaGastoId == idCategoriaGasto
                                                                                       ).ToListAsync();
            return subCategoriasGasto;
        }

        public async Task<GastosSubcategoriagasto> GetById(long id)
        {
            var subCategoriaGasto = await _context.GastosSubcategoriagastos.FindAsync(id);
            return subCategoriaGasto;
        }

        public async Task<GastosSubcategoriagasto> GetActiveById(long id)
        {
            var subCategoriaGasto = await _context.GastosSubcategoriagastos.FindAsync(id);

            if (subCategoriaGasto == null) return null;

            if (subCategoriaGasto.Baja == false)
            {
                return subCategoriaGasto;
            }
            return null;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(GastosSubcategoriagasto subCategoriaGasto)
        {
            _context.GastosSubcategoriagastos.Attach(subCategoriaGasto);
            _context.GastosSubcategoriagastos.Entry(subCategoriaGasto).State = EntityState.Modified;
        }

        public void BajaLogica(GastosSubcategoriagasto subCategoriaGasto)
        {
            subCategoriaGasto.Baja = true;
            _context.GastosSubcategoriagastos.Attach(subCategoriaGasto);
            _context.GastosSubcategoriagastos.Entry(subCategoriaGasto).State = EntityState.Modified;
        }

        public IEnumerable<GastosSubcategoriagasto> Search(Func<GastosSubcategoriagasto, bool> filter)
            => _context.GastosSubcategoriagastos.Where(filter).ToList();
    }
}
