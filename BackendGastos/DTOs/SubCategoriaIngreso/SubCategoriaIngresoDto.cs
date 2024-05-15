namespace BackendGastos.Controller.DTOs.SubCategoriaIngreso
{
    public class SubCategoriaIngresoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long CategoriaIngresoId { get; set; }

        public long UsuarioId { get; set; }
    }
}
