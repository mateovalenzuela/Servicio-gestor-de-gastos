namespace BackendGastos.Service.DTOs.SubCategoriaGasto
{
    public class CategoriaGastoYSubCategoriasGastoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public decimal? Importe { get; set; }

        public virtual List<SubCategoriaGastoDto> SubCategorias { get; set; } = new List<SubCategoriaGastoDto>();
    }
}
