using BackendGastos.Service.DTOs.SubCategoriaGasto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.SubCategoriaGasto
{
    public class InsertUpdateSubCategoriaGastoValidator : AbstractValidator<InsertUpdateSubCategoriaGastoDto>
    {
        public InsertUpdateSubCategoriaGastoValidator()
        {
            RuleFor(s => s.Descripcion).Length(2, 30).WithMessage("La {PropertyName} es obligadoria, debe tener de 2 a 20 caracteres");

            //RuleFor(s => s.CategoriaGastoId).NotEmpty().WithMessage("El {PropertyName} no debe ser nulo");
            RuleFor(s => s.CategoriaGastoId).GreaterThan(0).WithMessage("El {PropertyName} no es valido");

           // RuleFor(s => s.UsuarioId).NotEmpty().WithMessage("El {PropertyName} no debe ser nulo");
            RuleFor(s => s.UsuarioId).GreaterThan(0).WithMessage("El {PropertyName} no es valido");
        }
    }
}
