using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public class CategoriaIngresoRepository : ICategoriaIngresoRepository
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

            if (categoriaIngreso == null) return null;

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

        public IEnumerable<GastosCategoriaigreso> Search(Func<GastosCategoriaigreso, bool> filter)
            => _context.GastosCategoriaigresos.Where(filter).ToList();

        public async Task<IEnumerable<CategoriaingresoAmount>> GetActiveWithAmount(long idUser)
        {
            var categoriasIngreso = await _context.GastosCategoriaigresos.Where(c => c.Baja == false)
                .GroupJoin(
                _context.GastosIngresos.Where(g => g.UsuarioId == idUser && g.Baja == false),
                categoria => categoria.Id,
                ingreso => ingreso.CategoriaIngresoId,
                (categoria, ingreso) => new CategoriaingresoAmount()
                {
                    Id = categoria.Id,
                    Descripcion = categoria.Descripcion,
                    ImporteTotal = ingreso.Sum(g => (decimal?)g.Importe) ?? 0 // Suma los importes de los ingresos, si no hay, devuelve 0
                }
                ).ToListAsync();
            return categoriasIngreso;
        }

    }
}
