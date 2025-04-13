namespace BackendGastos.Service.Services
{
    public interface IUtilidadesService
    {
        Task<DateTime?> GetDateTimeNow();

        Task<DateTime?> ValidateDateTime(string fecha);
    }
}
