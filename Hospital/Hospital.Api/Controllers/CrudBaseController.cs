using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Generic base controller providing CRUD endpoints for all entities.
/// </summary>
/// <typeparam name="TKey">Type of the entity identifier.</typeparam>
/// <typeparam name="TGetDto">Dto dor receiving</typeparam>
/// <typeparam name="TCreateUpdateDto">Dto for creating or updating</typeparam>
/// <param name="appService">Service for work with DTO.</param>
/// <param name="logger">Logger for information.</param>
[ApiController]
[Route("api/[controller]")]
public class CrudBaseController<TGetDto, TCreateUpdateDto, TKey>(
    IApplicationService<TGetDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudBaseController<TGetDto, TCreateUpdateDto, TKey>> logger) 
    : ControllerBase
    where TGetDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Helper method for consistent logging and error handling.
    /// </summary>
    protected ActionResult Logging(string method, Func<ActionResult> action)
    {
        logger.LogInformation("START: {Method}", method);
        try
        {
            var result = action();
            var count = 0;
            if (result is OkObjectResult okResult && okResult.Value != null)
            {
                if (okResult.Value is System.Collections.IEnumerable collection)
                {
                    count = collection.Cast<object>().Count();
                }
                else count = 1;
            }
            logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", method, count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR: {Method} failed.", method);
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="newDto">Data for the new entity.</param>
    /// <returns>The created entity.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public ActionResult<TKey> Create(TCreateUpdateDto newDto) =>
        Logging(nameof(Create), () => Created(nameof(Create), appService.Create(newDto)));

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="id">Identifier of the entity to update.</param>
    /// <param name="newDto">Updated entity data.</param>
    /// <returns>The updated entity.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<TGetDto> Edit(TKey id, [FromBody] TCreateUpdateDto newDto) =>
        Logging(nameof(Edit), () => Ok(appService.Update(id, newDto)));

    /// <summary>
    /// Retrieves an entity by its ID.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>The entity if found, otherwise 204 No Content.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<TGetDto> Get(TKey id) => Logging(nameof(Get), () => Ok(appService.Get(id)));

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>List of all entities.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<TGetDto>> GetAll() => Logging(nameof(GetAll), () => Ok(appService.GetAll()));

    /// <summary>
    /// Deletes an entity by ID.
    /// </summary>
    /// <param name="id">Identifier of the entity to delete.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> Delete(TKey id) => Logging(nameof(Delete), () => Ok(appService.Delete(id)));
}