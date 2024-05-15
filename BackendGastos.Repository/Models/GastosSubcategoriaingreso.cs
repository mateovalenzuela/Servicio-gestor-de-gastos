using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class GastosSubcategoriaingreso
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Baja { get; set; }

    public long CategoriaIngresoId { get; set; }

    public long UsuarioId { get; set; }

    public virtual GastosCategoriaigreso CategoriaIngreso { get; set; } = null!;

    public virtual ICollection<GastosIngreso> GastosIngresos { get; set; } = new List<GastosIngreso>();

    public virtual AuthenticationUsuario Usuario { get; set; } = null!;
}
