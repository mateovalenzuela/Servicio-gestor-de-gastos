namespace BackendGastos.Service.DTOs.SubCategoriaIngreso
{
    public class SubCategoriaIngresoDto : IEquatable<SubCategoriaIngresoDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long CategoriaIngresoId { get; set; }

        public long UsuarioId { get; set; }

        public bool Equals(SubCategoriaIngresoDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion &&
                   UsuarioId == other.UsuarioId &&
                   CategoriaIngresoId == other.CategoriaIngresoId;
        }

        public override bool Equals(object obj) => Equals(obj as SubCategoriaIngresoDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion, UsuarioId, CategoriaIngresoId);


    }
}
