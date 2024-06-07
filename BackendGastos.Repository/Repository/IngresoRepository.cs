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

            if (ingreso == null) return null;

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

        public async Task<GastosCategoriaigreso?> GetCategoriaIngresoById(long id)
        {
            var categoriaIngresos = await _context.GastosCategoriaigresos.FindAsync(id);
            if (categoriaIngresos == null) return null;

            if (categoriaIngresos.Baja == false)
            {
                return categoriaIngresos;
            }

            return null;
        }

        public async Task<AuthenticationUsuario?> GetUsuarioById(long id)
        {
            var user = await _context.AuthenticationUsuarios.FindAsync(id);
            if (user == null) return null;

            if (user.IsActive == true)
            {
                return user;
            }

            return null;
        }

        public async Task<GastosSubcategoriaingreso?> GetSubCategoriaIngresoById(long id)
        {
            var subcategoria = await _context.GastosSubcategoriaingresos.FindAsync(id);

            if (subcategoria == null) return null;

            if (subcategoria.Baja == false)
            {
                return subcategoria;
            }
            return null;           
        }

        public async Task<GastosMonedum?> GetMonedaById(long idMoneda)
        {
            var moneda = await _context.GastosMoneda.FindAsync(idMoneda);

            if (moneda == null) return null;

            if (moneda.Baja == false)
            {
                return moneda;
            }
            return null;
        }

        public async Task<IEnumerable<GastosIngreso>> GetActiveByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso)
        {
            var ingresos = await _context.GastosIngresos.Where(c => c.Baja == false &&
                                                                    c.UsuarioId == idUser &&
                                                                    c.CategoriaIngresoId == idCategoriaIngreso
                                                                    ).ToListAsync();
            return ingresos;
        }
    }
}
