using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface IIngresoRepository : IRepository<GastosIngreso>
    {
        Task<IEnumerable<GastosIngreso>> GetActiveByUser(long idUser);

        Task<IEnumerable<GastosIngreso>> GetActiveByCategoriaIngreso(long idCategoriaIngreso);

        Task<IEnumerable<GastosIngreso>> GetActiveBySubCategoriaIngreso(long idSubCategoriaIngreso);

        Task<IEnumerable<GastosIngreso>> GetActiveByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso);

        Task<IEnumerable<GastosIngreso>> SearchActiveByDescripcionParcial(long idUser, string descripcion, int limit);

    }
}
