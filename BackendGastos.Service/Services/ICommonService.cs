namespace BackendGastos.Service.Services
{
    public interface ICommonService<TDto, InsertUpdateTDto>
    {
        public Dictionary<string, string> Errors { get; }
        Task<IEnumerable<TDto>> Get();
        Task<TDto?> GetById(long id);
        Task<TDto> Add(InsertUpdateTDto insertUpdateDto);
        Task<TDto?> Update(long id, InsertUpdateTDto insertUpdateDto);
        Task<TDto?> Delete(long id);

    }
}
