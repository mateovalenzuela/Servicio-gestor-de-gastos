using BackendGastos.Service.DTOs.Gasto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.Gasto
{
    public class SearchGastoValidator : AbstractValidator<GastoDto>
    {
        public SearchGastoValidator() 
        {
            RuleFor(g => g.Descripcion).Length(2, 30).WithMessage("La {PropertyName} debe tener de 2 a 20 caracteres");
        }
    }
}
