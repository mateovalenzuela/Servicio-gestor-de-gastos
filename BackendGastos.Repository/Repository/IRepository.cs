using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    internal interface IRepository<TEntity>
    {
    
        Task<IEnumerable<TEntity>> Get();

        Task<IEnumerable<TEntity>> GetActive();

        Task<TEntity> GetById(long id);

        Task<TEntity> GetActiveById(long id);

        Task Add(TEntity entity);

        void Update(TEntity entity);

        void BajaLogica(TEntity entity);

        void Delete(TEntity entity);

        Task Save();
    }
}
