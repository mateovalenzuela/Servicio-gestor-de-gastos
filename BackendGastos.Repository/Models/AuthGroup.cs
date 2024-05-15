using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class AuthGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AuthGroupPermission> AuthGroupPermissions { get; set; } = new List<AuthGroupPermission>();

    public virtual ICollection<AuthenticationUsuarioGroup> AuthenticationUsuarioGroups { get; set; } = new List<AuthenticationUsuarioGroup>();
}
