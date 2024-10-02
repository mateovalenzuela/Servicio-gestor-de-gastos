using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.DTOs.Reporte;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Validator.Reporte
{
    public class ObtenerReporteDtoValidator : AbstractValidator<ObtenerReporteDto>
    {
        // Formato y cultura a utilizar para la validación de fechas
        private readonly string FormatoFecha = "dd/MM/yyyy";
        private readonly CultureInfo CultureInfo = new("es-AR");

        public ObtenerReporteDtoValidator()
        {
            // Validar que FechaLimite es obligatoria y tiene un formato válido
            RuleFor(x => x.FechaLimite)
                .NotEmpty().WithMessage("La fecha límite es obligatoria.")
                .Must(fecha => ValidarFormatoFecha(fecha)).WithMessage($"La fecha límite debe tener el formato {FormatoFecha}.");

            // Validar que FechaInicial es obligatoria y tiene un formato válido
            RuleFor(x => x.FechaInicial)
                .NotEmpty().WithMessage("La fecha inicial es obligatoria.")
                .Must(fecha => ValidarFormatoFecha(fecha)).WithMessage($"La fecha inicial debe tener el formato {FormatoFecha}.");
        }

        // Método auxiliar para validar que la fecha tiene el formato específico
        private bool ValidarFormatoFecha(DateTime fecha)
        {
            var fechaString = fecha.ToString(FormatoFecha, CultureInfo); // Convertir fecha al formato de validación
            return DateTime.TryParseExact(fechaString, FormatoFecha, CultureInfo, DateTimeStyles.None, out _);
        }
    }

}
