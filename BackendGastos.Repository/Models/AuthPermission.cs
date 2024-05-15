using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class AuthPermission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ContentTypeId { get; set; }

    public string Codename { get; set; } = null!;

    public virtual ICollection<AuthGroupPermission> AuthGroupPermissions { get; set; } = new List<AuthGroupPermission>();

    public virtual ICollection<AuthenticationUsuarioUserPermission> AuthenticationUsuarioUserPermissions { get; set; } = new List<AuthenticationUsuarioUserPermission>();

    public virtual DjangoContentType ContentType { get; set; } = null!;
}
