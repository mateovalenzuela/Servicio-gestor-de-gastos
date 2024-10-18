using BackendGastos.Service.DTOs.Ingreso;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.Ingreso
{
    public class SearchIngresoValidator : AbstractValidator<IngresoDto>
    {
        public SearchIngresoValidator() 
        {
            RuleFor(g => g.Descripcion).Length(2, 30).WithMessage("La {PropertyName} debe tener de 2 a 20 caracteres");
        }
    }
}
