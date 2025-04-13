namespace BackendGastos.Service.DTOs.SubCategoriaIngreso
{
    public class CategoriaIngresoYSubCategoriasIngresoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public decimal? Importe { get; set; }

        public virtual List<SubCategoriaIngresoDto> SubCategorias { get; set; } = new List<SubCategoriaIngresoDto>();
    }
}
