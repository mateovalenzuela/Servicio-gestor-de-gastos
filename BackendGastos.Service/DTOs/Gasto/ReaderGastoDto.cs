namespace BackendGastos.Service.DTOs.Gasto
{
    public class ReaderGastoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public decimal Importe { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Baja { get; set; }

        public long CategoriaGastoId { get; set; }

        public long MonedaId { get; set; }

        public long SubcategoriaGastoId { get; set; }

        public long UsuarioId { get; set; }
    }
}
