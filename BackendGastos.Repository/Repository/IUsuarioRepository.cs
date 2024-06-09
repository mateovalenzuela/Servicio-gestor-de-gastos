using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public interface IUsuarioRepository
    {
        public Task<AuthenticationUsuario?> GetActiveById(long id);
    }
}
