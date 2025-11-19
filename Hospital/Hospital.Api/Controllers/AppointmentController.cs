using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Provides API methods working with appointments
/// </summary>
/// <param name="service">Service for appointments repository</param>
/// <param name="logger">Logger for logging</param>
[ApiController]
[Route("api/[controller]")]
public class AppointmentController(
    IAppointmentService service,
    ILogger<AppointmentController> logger)
    : CrudBaseController<AppointmentGetDto, AppointmentCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Get Patient of the appointment by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>doctor</returns>
    [HttpGet("{id}/patient")]
    [ProducesResponseType(typeof(PatientGetDto), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PatientGetDto>> GetPatientById(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetPatientById), GetType().Name, id);
        try
        {
            var res = await service.GetParientByAppointment(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPatientById), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPatientById), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
    
    /// <summary>
    /// Get doctor of the appointment by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>patient</returns>
    [HttpGet("{id}/doctor")]
    [ProducesResponseType(typeof(DoctorGetDto), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<DoctorGetDto>> GetDoctorById(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetDoctorById), GetType().Name, id);
        try
        {
            var res = await service.GetDoctorByAppointment(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetDoctorById), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetDoctorById), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}