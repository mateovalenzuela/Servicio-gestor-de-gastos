namespace BackendGastos.Repository.Models;

public partial class GastosMonedum
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Baja { get; set; }

    public virtual ICollection<GastosGasto> GastosGastos { get; set; } = new List<GastosGasto>();

    public virtual ICollection<GastosIngreso> GastosIngresos { get; set; } = new List<GastosIngreso>();
}
