namespace BackendGastos.Repository.Models;

public partial class GastosIngreso
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Importe { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Baja { get; set; }

    public long CategoriaIngresoId { get; set; }

    public long MonedaId { get; set; }

    public long SubcategoriaIngresoId { get; set; }

    public long UsuarioId { get; set; }

    public virtual GastosCategoriaigreso CategoriaIngreso { get; set; } = null!;

    public virtual GastosMonedum Moneda { get; set; } = null!;

    public virtual GastosSubcategoriaingreso SubcategoriaIngreso { get; set; } = null!;

    public virtual AuthenticationUsuario Usuario { get; set; } = null!;
}
