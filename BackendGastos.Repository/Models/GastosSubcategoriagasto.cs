namespace BackendGastos.Repository.Models;

public partial class GastosSubcategoriagasto
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Baja { get; set; }

    public long CategoriaGastoId { get; set; }

    public long UsuarioId { get; set; }

    public virtual GastosCategoriagasto CategoriaGasto { get; set; } = null!;

    public virtual ICollection<GastosGasto> GastosGastos { get; set; } = new List<GastosGasto>();

    public virtual AuthenticationUsuario Usuario { get; set; } = null!;
}
