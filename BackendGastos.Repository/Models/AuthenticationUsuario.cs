using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class AuthenticationUsuario
{
    public long Id { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public bool IsSuperuser { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailConfirmado { get; set; }

    public bool IsActive { get; set; }

    public bool IsStaff { get; set; }

    public virtual AuthenticationPerfil? AuthenticationPerfil { get; set; }

    public virtual ICollection<AuthenticationUsuarioGroup> AuthenticationUsuarioGroups { get; set; } = new List<AuthenticationUsuarioGroup>();

    public virtual ICollection<AuthenticationUsuarioUserPermission> AuthenticationUsuarioUserPermissions { get; set; } = new List<AuthenticationUsuarioUserPermission>();

    public virtual AuthtokenToken? AuthtokenToken { get; set; }

    public virtual ICollection<DjangoAdminLog> DjangoAdminLogs { get; set; } = new List<DjangoAdminLog>();

    public virtual ICollection<GastosGasto> GastosGastos { get; set; } = new List<GastosGasto>();

    public virtual ICollection<GastosIngreso> GastosIngresos { get; set; } = new List<GastosIngreso>();

    public virtual ICollection<GastosSubcategoriagasto> GastosSubcategoriagastos { get; set; } = new List<GastosSubcategoriagasto>();

    public virtual ICollection<GastosSubcategoriaingreso> GastosSubcategoriaingresos { get; set; } = new List<GastosSubcategoriaingreso>();

    public virtual ICollection<TokenBlacklistOutstandingtoken> TokenBlacklistOutstandingtokens { get; set; } = new List<TokenBlacklistOutstandingtoken>();
}
