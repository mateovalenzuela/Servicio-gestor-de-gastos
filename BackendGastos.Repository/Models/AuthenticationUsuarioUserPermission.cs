using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class AuthenticationUsuarioUserPermission
{
    public long Id { get; set; }

    public long UsuarioId { get; set; }

    public int PermissionId { get; set; }

    public virtual AuthPermission Permission { get; set; } = null!;

    public virtual AuthenticationUsuario Usuario { get; set; } = null!;
}
