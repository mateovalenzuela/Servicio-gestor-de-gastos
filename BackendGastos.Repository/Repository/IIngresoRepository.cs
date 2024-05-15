using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    internal interface IIngresoRepository<TEntity> : IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetActiveByUser(long idUser);

        Task<IEnumerable<TEntity>> GetActiveByCategoriaIngreso(long idCategoriaIngreso);

        Task<IEnumerable<TEntity>> GetActiveBySubCategoriaIngreso(long idSubCategoriaIngreso);
    }
}
