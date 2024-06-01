using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.SubCategoriaIngreso
{
    public class InsertUpdateSubCategoriaIngresoValidator : AbstractValidator<InsertUpdateSubCategoriaIngresoDto>
    {
        public InsertUpdateSubCategoriaIngresoValidator()
        {
            //RuleFor(s => s.Descripcion).NotEmpty().WithMessage("La {PropertyName} de la categoria es obligatoria");
            RuleFor(s => s.Descripcion).Length(2, 30).WithMessage("La {PropertyName} es obligatoria y debe tener de 2 a 20 caracteres");

            //RuleFor(s => s.UsuarioId).NotEmpty().WithMessage("El {PropertyNaame} no debe ser nulo");
            RuleFor(s => s.UsuarioId).GreaterThan(0).WithMessage("El {PropertyNaame} no es valido");

            //RuleFor(s => s.CategoriaIngresoId).NotEmpty().WithMessage("El {PropertyNaame} no debe ser nulo");
            RuleFor(s => s.CategoriaIngresoId).GreaterThan(0).WithMessage("El {PropertyNaame} no es valido");
        }
    }
}
