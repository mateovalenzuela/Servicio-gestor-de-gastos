﻿using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public interface ISubCategoriaIngresoRepository : IRepository<GastosSubcategoriaingreso>
    {

        Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByUser(long idUser);

        Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByUserAndCategoriaIngreso(long idUser, long idSubCategoriaIngreso);

        Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByCategoriaIngreso(long idSubCategoriaIngreso);

        Task<GastosCategoriaigreso> GetCategoriaIngresoById(long idCategoriaIngreso);
        Task<AuthenticationUsuario> GetUsuarioById(long idUser);

        IEnumerable<GastosSubcategoriaingreso> Search(Func<GastosSubcategoriaingreso, bool> filter);

    }
}
