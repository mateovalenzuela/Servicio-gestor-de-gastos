namespace BackendGastos.Service.DTOs.SubCategoriaGasto
{
    public class SubCategoriaGastoDto : IEquatable<SubCategoriaGastoDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long CategoriaGastoId { get; set; }

        public long UsuarioId { get; set; }

        public bool Equals(SubCategoriaGastoDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion &&
                   UsuarioId == other.UsuarioId &&
                   CategoriaGastoId == other.CategoriaGastoId;
        }

        public override bool Equals(object obj) => Equals(obj as SubCategoriaGastoDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion, UsuarioId, CategoriaGastoId);
    }
}
