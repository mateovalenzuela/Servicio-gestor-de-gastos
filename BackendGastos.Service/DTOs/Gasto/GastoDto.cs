using BackendGastos.Service.DTOs.Ingreso;

namespace BackendGastos.Service.DTOs.Gasto
{
    public class GastoDto : IEquatable<GastoDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public decimal Importe { get; set; }

        public long CategoriaGastoId { get; set; }

        public long MonedaId { get; set; }

        public long SubcategoriaGastoId { get; set; }

        public long UsuarioId { get; set; }


        public bool Equals(GastoDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion &&
                   Importe == other.Importe &&
                   UsuarioId == other.UsuarioId &&
                   CategoriaGastoId == other.CategoriaGastoId &&
                   SubcategoriaGastoId == other.SubcategoriaGastoId &&
                   MonedaId == other.MonedaId;
        }

        public override bool Equals(object obj) => Equals(obj as GastoDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion, Importe, UsuarioId, CategoriaGastoId, SubcategoriaGastoId, MonedaId);


    }
}
