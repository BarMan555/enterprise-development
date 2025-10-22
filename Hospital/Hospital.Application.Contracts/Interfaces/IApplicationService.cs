namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of any CRUD service 
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationService<TDto, TKey>
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="dto">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public TKey Create(TDto dto);
    
    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public TDto Get(TKey dtoId);
    
    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<TDto> GetAll();
    
    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public TDto Update(TKey dtoId, TDto dto);
    
    /// <summary>
    /// Delete entity from repository
    /// </summary>
    /// <param name="dtoId">Entity ID</param>
    /// <returns>Result of deleting</returns>
    public bool Delete(TKey dtoId);
}