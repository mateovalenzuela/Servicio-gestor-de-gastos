namespace BackendGastos.Service.DTOs.Ingreso
{
    public class ReaderIngresoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public decimal Importe { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Baja { get; set; }

        public long CategoriaIngresoId { get; set; }

        public long MonedaId { get; set; }

        public long SubcategoriaIngresoId { get; set; }

        public long UsuarioId { get; set; }
    }
}
