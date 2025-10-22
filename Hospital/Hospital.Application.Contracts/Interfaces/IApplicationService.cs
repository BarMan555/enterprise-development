namespace Hospital.Application.Contracts.Interfaces;

public interface IApplicationService<TDto, TKey>
{
    public TKey Create(TDto dto);
    public TDto Get(TKey dtoId);
    public List<TDto> GetAll();
    public TDto Update(TKey dtoId, TDto dto);
    public bool Delete(TKey dtoId);
}