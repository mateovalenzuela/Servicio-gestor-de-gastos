namespace BackendGastos.Controller.DTOs.Ingreso
{
    public class InsertUpdateIngresoDto
    {
        public string Descripcion { get; set; } = null!;

        public decimal Importe { get; set; }

        public long CategoriaIngresoId { get; set; }

        public long MonedaId { get; set; }

        public long SubcategoriaIngresoId { get; set; }

        public long UsuarioId { get; set; }
    }
}
