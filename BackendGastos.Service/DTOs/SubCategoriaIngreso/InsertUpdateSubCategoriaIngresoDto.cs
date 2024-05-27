namespace BackendGastos.Service.DTOs.SubCategoriaIngreso
{
    public class InsertUpdateSubCategoriaIngresoDto
    {
        public string Descripcion { get; set; }

        public long CategoriaIngresoId { get; set; }

        public long UsuarioId { get; set; }

    }
}
