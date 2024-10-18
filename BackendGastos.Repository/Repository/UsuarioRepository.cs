﻿using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ProyectoGastosTestContext _context;

        public UsuarioRepository(ProyectoGastosTestContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationUsuario?> GetActiveById(long id)
        {
            var usuario = await _context.AuthenticationUsuarios.FindAsync(id);
            if (usuario != null)
            {
                return usuario;
            }
            return null;
        }

        public async Task<bool> IsActive(long id)
            => await _context.AuthenticationUsuarios.AnyAsync(u => u.Id == id);
    }
}
