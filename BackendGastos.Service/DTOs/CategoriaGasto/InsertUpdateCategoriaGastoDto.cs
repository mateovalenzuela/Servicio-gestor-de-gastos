using System.ComponentModel.DataAnnotations;

namespace BackendGastos.Service.DTOs.CategoriaGasto
{
    public class InsertUpdateCategoriaGastoDto
    {
        [Required(ErrorMessage = "La Descripción es requerida")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "La Descripción debe tener entre 2 y 30 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La Fecha de Creación es requerida")]
        public DateTime FechaCreacion { get; set; }
    }
}
