
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using FluentValidation;

namespace BackendGastos.Validator.CategoriaIngreso
{
    public class CategoriaIngresoValidator : AbstractValidator<CategoriaIngresoDto>
    {
        public CategoriaIngresoValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("El Id es obligatorio");
            RuleFor(c => c.Descripcion).NotEmpty().WithMessage("La descripcion de la categoria es obligatoria");
            RuleFor(c => c.Descripcion).Length(2, 30).WithMessage("La {PropertyName} debe tener de 2 a 20 caracteres");
        }
    }
}
