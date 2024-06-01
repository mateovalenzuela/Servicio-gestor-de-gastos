using BackendGastos.Service.DTOs.SubCategoriaGasto;

namespace BackendGastos.Service.DTOs.CategoriaIngreso
{
    public class CategoriaIngresoDto : IEquatable<CategoriaIngresoDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }


        public bool Equals(CategoriaIngresoDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion;
        }

        public override bool Equals(object obj) => Equals(obj as CategoriaIngresoDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion);
    }
}
