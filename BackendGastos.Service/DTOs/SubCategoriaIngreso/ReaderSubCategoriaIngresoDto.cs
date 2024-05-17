namespace BackendGastos.Service.DTOs.SubCategoriaIngreso
{
    public class ReaderSubCategoriaIngresoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long CategoriaIngresoId { get; set; }

        public long UsuarioId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Baja { get; set; }
    }
}
