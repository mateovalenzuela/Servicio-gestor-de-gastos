using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendGastos.Repository.Repository
{
    public class SubCategoriaIngresoRepository : ISubCategoriaIngresoRepository
    {
        private ProyectoGastosTestContext _context;

        public SubCategoriaIngresoRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }


        public async Task Add(GastosSubcategoriaingreso subCategoriaIngreso)
            => await _context.GastosSubcategoriaingresos.AddAsync(subCategoriaIngreso);

        public void Delete(GastosSubcategoriaingreso subCategoriaIngreso)
        {
            _context.GastosSubcategoriaingresos.Remove(subCategoriaIngreso);
        }

        public async Task<IEnumerable<GastosSubcategoriaingreso>> Get()
        {
            var subCategoriasIngreso = await _context.GastosSubcategoriaingresos.ToListAsync();
            return subCategoriasIngreso;
        }

        public async Task<IEnumerable<GastosSubcategoriaingreso>> GetActive()
        {
            var subCategoriasIngreso = await _context.GastosSubcategoriaingresos.Where(c => c.Baja == false).ToListAsync();
            return subCategoriasIngreso;
        }

        public async Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByUser(long idUser)
        {
            var subCategoriasIngreso = await _context.GastosSubcategoriaingresos.Where(c => c.Baja == false && c.UsuarioId == idUser).ToListAsync();
            return subCategoriasIngreso;
        }

        public async Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByCategoriaIngreso(long idCategoriaIngreso)
        {
            var subCategoriasIngreso = await _context.GastosSubcategoriaingresos.Where(c => c.Baja == false &&
                                                                                       c.CategoriaIngresoId == idCategoriaIngreso
                                                                                       ).ToListAsync();
            return subCategoriasIngreso;
        }

        public async Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByUserAndCategoriaIngreso(long idUser, long IdCategoriaIngreso)
        {
            var subCategoriasIngreso = await _context.GastosSubcategoriaingresos.Where(c => c.Baja == false &&
                                                                                       c.UsuarioId == idUser &&
                                                                                       c.CategoriaIngresoId == IdCategoriaIngreso
                                                                                       ).ToListAsync();
            return subCategoriasIngreso;
        }

        public async Task<GastosSubcategoriaingreso> GetById(long id)
        {
            var subCategoriaIngreso = await _context.GastosSubcategoriaingresos.FindAsync(id);
            return subCategoriaIngreso;
        }

        public async Task<GastosSubcategoriaingreso> GetActiveById(long id)
        {
            var subCategoriaIngreso = await _context.GastosSubcategoriaingresos.FindAsync(id);

            if (subCategoriaIngreso == null) return null;

            if (subCategoriaIngreso.Baja == false)
            {
                return subCategoriaIngreso;
            }
            return null;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(GastosSubcategoriaingreso subCategoriaIngreso)
        {
            _context.GastosSubcategoriaingresos.Attach(subCategoriaIngreso);
            _context.GastosSubcategoriaingresos.Entry(subCategoriaIngreso).State = EntityState.Modified;
        }

        public void BajaLogica(GastosSubcategoriaingreso subCategoriaIngreso)
        {
            subCategoriaIngreso.Baja = true;
            _context.GastosSubcategoriaingresos.Attach(subCategoriaIngreso);
            _context.GastosSubcategoriaingresos.Entry(subCategoriaIngreso).State = EntityState.Modified;
        }

        public IEnumerable<GastosSubcategoriaingreso> Search(Func<GastosSubcategoriaingreso, bool> filter)
            => _context.GastosSubcategoriaingresos.Where(filter).ToList();
    }
}
