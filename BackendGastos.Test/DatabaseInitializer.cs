using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Test
{
    internal class DatabaseInitializer
    {
        public static void Initialize(ProyectoGastosTestContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are already users in the database
            if (context.AuthenticationUsuarios.Any())
            {
                return;   // Database has been seeded
            }

            // Add two users
            var usuarios = new List<AuthenticationUsuario>
            {
            new() { Id = 1, Username = "Test 1", Email = "usuario_test1@example.com", Password = "fwewfwefwe", 
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
            new() { Id = 2, Username = "Test 2", Email = "usuario_test2@example.com", Password = "fwewfwefwe",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
            };

            context.AuthenticationUsuarios.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}
