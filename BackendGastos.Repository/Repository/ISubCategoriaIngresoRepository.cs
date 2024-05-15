using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    internal interface ISubCategoriaIngresoRepository<TEntity> : IRepository<TEntity>
    {

        Task<IEnumerable<TEntity>> GetActiveByUser(long idUser);

        Task<IEnumerable<TEntity>> GetActiveByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso);

        Task<IEnumerable<TEntity>> GetActiveByCategoriaIngreso(long idCategoriaIngreso);
    }
}
