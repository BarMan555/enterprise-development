using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Provides API methods working with patients
/// </summary>
/// <param name="service">Service for patients repository</param>
/// <param name="logger">Logger for logging</param>
[ApiController]
[Route("api/[controller]")]
public class PatientController(IPatientService service, ILogger<PatientController> logger)
    : CrudBaseController<PatientGetDto, PatientCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Get appointments where is the patient
    /// </summary>
    /// <param name="id">id of patient</param>
    /// <returns>list of appintments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentGetDto>>> GetAppointmentsById(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetAppointmentsById), GetType().Name, id);
        try
        {
            var res = await service.GetAppointmentsByPatient(id);
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