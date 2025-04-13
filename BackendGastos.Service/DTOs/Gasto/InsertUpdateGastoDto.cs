using System.ComponentModel.DataAnnotations;

namespace BackendGastos.Service.DTOs.Gasto
{
    public class InsertUpdateGastoDto
    {
        [Required(ErrorMessage = "La Descripción es requerida")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "La Descripción debe tener entre 2 y 30 caracteres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El Importe es requerido")]
        public decimal Importe { get; set; }


        [Required(ErrorMessage = "La Categoría es requerida")]
        [Range(0, long.MaxValue, ErrorMessage = "Debe ser mayor a 0")]
        public long CategoriaGastoId { get; set; }


        [Required(ErrorMessage = "La Moneda es requerida")]
        [Range(0, long.MaxValue, ErrorMessage = "Debe ser mayor a 0")]
        public long MonedaId { get; set; }

        [Required(ErrorMessage = "La Subcategoría es requerida")]
        [Range(0, long.MaxValue, ErrorMessage = "Debe ser mayor a 0")]
        public long SubcategoriaGastoId { get; set; }


        [Required(ErrorMessage = "La Fecha de Creación es requerida")]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "El Usuario es requerido")]
        [Range(0, long.MaxValue, ErrorMessage = "Debe ser mayor a 0")]
        public long UsuarioId { get; set; }
    }
}
