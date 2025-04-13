using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface IUsuarioRepository
    {
        public Task<AuthenticationUsuario?> GetActiveById(long id);

        public Task<bool> IsActive(long id);
    }
}
