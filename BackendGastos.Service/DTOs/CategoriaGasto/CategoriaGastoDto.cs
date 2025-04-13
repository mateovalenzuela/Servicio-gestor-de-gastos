namespace BackendGastos.Service.DTOs.CategoriaGasto
{
    public class CategoriaGastoDto : IEquatable<CategoriaGastoDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }


        public bool Equals(CategoriaGastoDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion;
        }

        public override bool Equals(object obj) => Equals(obj as CategoriaGastoDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion);



    }



}
