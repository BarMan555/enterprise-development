using System.Runtime.CompilerServices;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OpenTelemetry.Trace;

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
public abstract class CrudBaseController<TGetDto, TCreateUpdateDto, TKey>(
    IApplicationService<TGetDto, TCreateUpdateDto, ObjectId> appService,
    ILogger<CrudBaseController<TGetDto, TCreateUpdateDto, TKey>> logger) 
    : ControllerBase
    where TGetDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="newDto">Data for the new entity.</param>
    /// <returns>The created entity.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TKey>> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Create), GetType().Name, newDto);
        try
        {
            var result = await appService.Create(newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), result);
        }
        catch (Exception e)
        {
            logger.LogError(e,"An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            return StatusCode(500, $"{e.Message}\n\r{e.InnerException?.Message}");
        }
    }


    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="id">Identifier of the entity to update.</param>
    /// <param name="newDto">Updated entity data.</param>
    /// <returns>The updated entity.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TGetDto>> Edit(string id, [FromBody] TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Edit), GetType().Name, newDto);
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                logger.LogError("An exception happened during {method} method of {controller}", nameof(Edit), GetType().Name);
                return BadRequest("Invalid Id format");
            }
            
            var result = await appService.Update(objectId, newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Edit), GetType().Name);
            return CreatedAtAction(nameof(this.Edit), result);
        }
        catch (Exception e)
        {
            logger.LogError(e,"An exception happened during {method} method of {controller}", nameof(Edit), GetType().Name);
            return StatusCode(500, $"{e.Message}\n\r{e.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves an entity by its ID.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>The entity if found, otherwise 204 No Content.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TGetDto>> Get(string id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                logger.LogError("An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
                return BadRequest("Invalid Id format");
            }

            var res = await appService.Get(objectId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>List of all entities.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TGetDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var res = await appService.GetAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Deletes an entity by ID.
    /// </summary>
    /// <param name="id">Identifier of the entity to delete.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<bool>> Delete(string id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                logger.LogError("An exception happened during {method} method of {controller}", nameof(Delete), GetType().Name);
                return BadRequest("Invalid Id format");
            }
            
            var res = await appService.Delete(objectId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            return res ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Delete), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}