using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class AuthenticationPerfil
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Imagen { get; set; }

    public long UsuarioId { get; set; }

    public virtual AuthenticationUsuario Usuario { get; set; } = null!;
}
