using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.SubCategoriaIngreso
{
    internal class InsertUpdateSubCategoriaIngresoValidator : AbstractValidator<InsertUpdateSubCategoriaIngresoDto>
    {
        public InsertUpdateSubCategoriaIngresoValidator()
        {
            RuleFor(s => s.Descripcion).NotEmpty().WithMessage("La {PropertyName} de la categoria es obligatoria");
            RuleFor(s => s.Descripcion).Length(2, 30).WithMessage("La {PropertyName} debe tener de 2 a 20 caracteres");
        }
    }
}
