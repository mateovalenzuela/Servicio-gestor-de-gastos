namespace BackendGastos.Repository.Models;

public partial class GastosCategoriaigreso
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Baja { get; set; }

    public virtual ICollection<GastosIngreso> GastosIngresos { get; set; } = new List<GastosIngreso>();

    public virtual ICollection<GastosSubcategoriaingreso> GastosSubcategoriaingresos { get; set; } = new List<GastosSubcategoriaingreso>();
}
