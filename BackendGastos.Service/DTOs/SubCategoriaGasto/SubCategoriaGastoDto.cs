namespace BackendGastos.Service.DTOs.SubCategoriaGasto
{
    public class SubCategoriaGastoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long CategoriaGastoId { get; set; }

        public long UsuarioId { get; set; }
    }
}
