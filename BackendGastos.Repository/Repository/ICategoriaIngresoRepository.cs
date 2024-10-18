﻿using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public interface ICategoriaIngresoRepository : IRepository<GastosCategoriaigreso>
    {
        IEnumerable<GastosCategoriaigreso> Search(Func<GastosCategoriaigreso, bool> filter);

        Task<IEnumerable<CategoriaingresoAmount>> GetActiveWithAmount(long idUser);

    }
}
