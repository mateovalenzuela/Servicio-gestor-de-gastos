using BackendGastos.Service.DTOs.CategoriaIngreso;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.CategoriaIngreso
{
    public class InsertUpdateCategoriaIngresoValidator : AbstractValidator<InsertUpdateCategoriaIngresoDto>
    {
        public InsertUpdateCategoriaIngresoValidator()
        {
            RuleFor(c => c.Descripcion).NotEmpty().WithMessage("La {PropertyName} no debe ser null");
            RuleFor(c => c.Descripcion).Length(2, 30).WithMessage("La {PropertyName} debe tener de 2 a 20 caracteres");
        }
    }
}
