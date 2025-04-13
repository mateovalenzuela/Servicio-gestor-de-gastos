namespace BackendGastos.Service.DTOs.CategoriaIngreso
{
    public class CategoriaIngresoWithAmountDto : IEquatable<CategoriaIngresoWithAmountDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public decimal ImporteTotal { get; set; }

        public bool Equals(CategoriaIngresoWithAmountDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion &&
                   ImporteTotal == other.ImporteTotal;
        }

        public override bool Equals(object obj) => Equals(obj as CategoriaIngresoWithAmountDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion, ImporteTotal);
    }
}
