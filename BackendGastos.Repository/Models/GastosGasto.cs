using System;
using System.Collections.Generic;

namespace BackendGastos.Repository.Models;

public partial class GastosGasto
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Importe { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Baja { get; set; }

    public long CategoriaGastoId { get; set; }

    public long MonedaId { get; set; }

    public long SubcategoriaGastoId { get; set; }

    public long UsuarioId { get; set; }

    public virtual GastosCategoriagasto CategoriaGasto { get; set; } = null!;

    public virtual GastosMonedum Moneda { get; set; } = null!;

    public virtual GastosSubcategoriagasto SubcategoriaGasto { get; set; } = null!;

    public virtual AuthenticationUsuario Usuario { get; set; } = null!;
}
