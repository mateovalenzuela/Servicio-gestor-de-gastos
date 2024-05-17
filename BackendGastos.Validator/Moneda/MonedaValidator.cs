using BackendGastos.Service.DTOs.Moneda;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.Moneda
{
    internal class MonedaValidator : AbstractValidator<MonedaDto>
    {
        public MonedaValidator()
        {
            RuleFor(m => m.Id).NotEmpty().WithMessage("El {PropertyName} es obligatorio");
            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("La {PropertyName} de la categoria es obligatoria");
            RuleFor(m => m.Descripcion).Length(2, 30).WithMessage("La {PropertyName} debe tener de 2 a 20 caracteres");
        }
    }
}
