namespace BackendGastos.Repository.Repository
{
    public interface IRepository<TEntity>
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
