using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public class UtilidadesService : IUtilidadesService
    {

        // Definimos formato de fecha y cultura
        private readonly string FormatoFecha = "dd/MM/yyyy";
        private readonly CultureInfo CultureInfo = new("es-AR");

        // Método para obtener la fecha y hora actual en UTC
        public Task<DateTime?> GetDateTimeNow()
        {
            // Aseguramos que la fecha sea UTC
            DateTime utcNow = DateTime.UtcNow;

            // Devolvemos DateTime en formato UTC
            return Task.FromResult<DateTime?>(utcNow);
        }

        // Método para validar una fecha en string según el formato definido
        public Task<DateTime?> ValidateDateTime(string fecha)
        {
            DateTime parsedDate;

            // Intentamos parsear la fecha usando el formato y cultura especificados
            bool esValido = DateTime.TryParseExact(fecha, this.FormatoFecha, this.CultureInfo, DateTimeStyles.None, out parsedDate);

            if (!esValido)
            {
                // Si no es válida, devolvemos null
                return Task.FromResult<DateTime?>(null);
            }

            // Devolvemos la fecha 
            return Task.FromResult<DateTime?>(parsedDate);
        }
    }


}
