namespace BackendGastos.Service.DTOs.Gasto
{
    public class InsertUpdateGastoDto
    {
        public string Descripcion { get; set; } = null!;

        public decimal Importe { get; set; }

        public long CategoriaGastoId { get; set; }

        public long MonedaId { get; set; }

        public long SubcategoriaGastoId { get; set; }

        public long UsuarioId { get; set; }
    }
}
