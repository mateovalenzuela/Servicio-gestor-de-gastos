using BackendGastos.Service.DTOs.SubCategoriaIngreso;

namespace BackendGastos.Service.DTOs.Ingreso
{
    public class IngresoDto : IEquatable<IngresoDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public decimal Importe { get; set; }

        public long CategoriaIngresoId { get; set; }

        public long MonedaId { get; set; }

        public long SubcategoriaIngresoId { get; set; }

        public long UsuarioId { get; set; }

        public bool Equals(IngresoDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion &&
                   Importe == other.Importe &&
                   UsuarioId == other.UsuarioId &&
                   CategoriaIngresoId == other.CategoriaIngresoId &&
                   SubcategoriaIngresoId == other.SubcategoriaIngresoId &&
                   MonedaId == other.MonedaId;
        }

        public override bool Equals(object obj) => Equals(obj as IngresoDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion, Importe, UsuarioId, CategoriaIngresoId, SubcategoriaIngresoId, MonedaId);
    }
}
