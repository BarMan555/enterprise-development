
namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of any CRUD service 
/// </summary>
/// <typeparam name="TGetDto">For receiving entities</typeparam>
/// <typeparam name="TCreateUpdateDto">For creating and updating entities</typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationService<TGetDto, TCreateUpdateDto, TKey>
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="dto">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public TKey Create(TCreateUpdateDto dto);
    
    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public TGetDto Get(TKey dtoId);
    
    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<TGetDto> GetAll();
    
    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public TGetDto Update(TKey dtoId, TCreateUpdateDto dto);
    
    /// <summary>
    /// Delete entity from repository
    /// </summary>
    /// <param name="dtoId">Entity ID</param>
    /// <returns>Result of deleting</returns>
    public bool Delete(TKey dtoId);
}