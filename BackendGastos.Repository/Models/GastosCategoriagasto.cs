using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class GastosCategoriagasto
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Baja { get; set; }

    public virtual ICollection<GastosGasto> GastosGastos { get; set; } = new List<GastosGasto>();

    public virtual ICollection<GastosSubcategoriagasto> GastosSubcategoriagastos { get; set; } = new List<GastosSubcategoriagasto>();
}
