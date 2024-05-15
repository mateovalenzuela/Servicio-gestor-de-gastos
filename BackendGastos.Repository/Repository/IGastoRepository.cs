using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    internal interface IGastoRepository<TEntity> : IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetActiveByUser(long idUser);

        Task<IEnumerable<TEntity>> GetActiveByCategoriaGasto(long idCategoriaGasto);

        Task<IEnumerable<TEntity>> GetActiveBySubCategoriaGasto(long idSubCategoriaGasto);
    }
}
