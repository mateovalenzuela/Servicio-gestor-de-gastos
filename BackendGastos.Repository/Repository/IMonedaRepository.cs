using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    internal interface IMonedaRepository : IRepository<GastosMonedum>
    {
        IEnumerable<GastosMonedum> Search(Func<GastosMonedum, bool> filter);

    }
}
