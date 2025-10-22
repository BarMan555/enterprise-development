using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CrudBaseController<TDto, TKey>(
    IApplicationService<TDto, TKey> appService,
    ILogger<CrudBaseController<TDto, TKey>> logger) 
    : ControllerBase
    where TDto : class
    where TKey : struct
{
    
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

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Create(TDto dto) =>
        Logging(nameof(Create), () => Created(nameof(Create), appService.Create(dto)));

    [HttpPut("{dtoId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Edit(TKey dtoId, [FromBody] TDto newDto) =>
        Logging(nameof(Edit), () => Ok(appService.Update(dtoId, newDto)));

    [HttpGet("{dtoId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Get(TKey dtoId) => Logging(nameof(Get), () => Ok(appService.Get(dtoId)));

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<TDto>> GetAll() => Logging(nameof(GetAll), () => Ok(appService.GetAll()));

    [HttpDelete("{dtoId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> Delete(TKey dtoId) => Logging(nameof(Delete), () => Ok(appService.Delete(dtoId)));
}