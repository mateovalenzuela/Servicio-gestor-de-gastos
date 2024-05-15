namespace BackendGastos.Controller.DTOs.CategoriaIngreso
{
    public class ReaderCategoriaIngresoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Baja { get; set; }
    }
}
