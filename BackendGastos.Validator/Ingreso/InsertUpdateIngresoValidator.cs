using BackendGastos.Service.DTOs.Ingreso;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BackendGastos.Validator.Ingreso
{
    public class InsertUpdateIngresoValidator : AbstractValidator<InsertUpdateIngresoDto>
    {
        public InsertUpdateIngresoValidator()
        {
            //RuleFor(i => i.Descripcion).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.Descripcion).Length(2, 30).WithMessage("La {PropertyName} es obligatoria y debe tener de 2 a 20 caracteres");

            //RuleFor(i => i.Importe).NotEmpty().WithMessage("La {PropertyName} es obligatorio");
            RuleFor(i => i.Importe).ScalePrecision(2, 18).WithMessage("El {PropertyName} es obligatorio y debe tener un formato de (18,2)");

            //RuleFor(i => i.CategoriaIngresoId).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.CategoriaIngresoId).GreaterThan(0).WithMessage("El {PropertyName}es obligatorio y debe ser mayor a 0");

            //RuleFor(i => i.SubcategoriaIngresoId).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.SubcategoriaIngresoId).GreaterThan(0).WithMessage("El {PropertyName} es obligatorio y debe ser mayor a 0");

            //RuleFor(i => i.MonedaId).NotEmpty().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(i => i.MonedaId).GreaterThan(0).WithMessage("El {PropertyName} es obligatorio y debe ser mayor a 0");

            RuleFor(i => i.UsuarioId).GreaterThan(0).WithMessage("El {PropertyName} es obligatorio y debe ser mayor a 0");
        }
    }
}
