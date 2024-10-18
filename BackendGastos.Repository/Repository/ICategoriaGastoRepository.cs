﻿using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public interface ICategoriaGastoRepository : IRepository<GastosCategoriagasto>
    {
        IEnumerable<GastosCategoriagasto> Search(Func<GastosCategoriagasto, bool> filter);

        Task<IEnumerable<CategoriagastoAmount>> GetActiveWithAmount(long idUser);
    }
}
