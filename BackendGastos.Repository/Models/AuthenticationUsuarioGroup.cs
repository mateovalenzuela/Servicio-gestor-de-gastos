using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class AuthenticationUsuarioGroup
{
    public long Id { get; set; }

    public long UsuarioId { get; set; }

    public int GroupId { get; set; }

    public virtual AuthGroup Group { get; set; } = null!;

    public virtual AuthenticationUsuario Usuario { get; set; } = null!;
}
