using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    internal interface ISubCategoriaGastoRepository<TEntity> : IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetActiveByUser(long idUser);

        Task<IEnumerable<TEntity>> GetActiveByUserAndCategoriaGasto(long idUser, long idCategoriaGasto);

        Task<IEnumerable<TEntity>> GetActiveByCategoriaGasto(long idCategoriaGasto);

        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);

    }
}
