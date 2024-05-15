using BackendGastos.Controller.DTOs.Ingreso;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.Ingreso
{
    internal class IngresoValidator : AbstractValidator<IngresoDto>
    {
        public IngresoValidator()
        {
            RuleFor(i => i.Descripcion).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.Descripcion).Length(2, 30).WithMessage("La {PropertyName} debe tener de 2 a 20 caracteres");

            RuleFor(i => i.Importe).NotEmpty().WithMessage("La {PropertyName} es obligatorio");
            RuleFor(i => i.Importe).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");

            RuleFor(i => i.CategoriaIngresoId).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.CategoriaIngresoId).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");

            RuleFor(i => i.SubcategoriaIngresoId).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.SubcategoriaIngresoId).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");

            RuleFor(i => i.UsuarioId).NotEmpty().WithMessage("El {PropertyName} es obligatorio");
            RuleFor(i => i.UsuarioId).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");

            RuleFor(i => i.MonedaId).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.MonedaId).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");
        }
    }
}
