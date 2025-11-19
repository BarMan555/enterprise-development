using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Provides API methods working with doctors
/// </summary>
/// <param name="service">Service for doctors repository</param>
/// <param name="logger">Logger for logging</param>
[ApiController]
[Route("api/[controller]")]
public class DoctorController(IDoctorService service, ILogger<DoctorController> logger)
    : CrudBaseController<DoctorGetDto, DoctorCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Get appointment where is the doctor
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentGetDto>>> GetAppointmentsById(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetAppointmentsById), GetType().Name, id);
        try
        {
            var res = await service.GetAppointmentsByDoctor(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAppointmentsById), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAppointmentsById), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}