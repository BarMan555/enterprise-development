using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Hospital.Api.Controllers;

/// <summary>
/// Provides API methods working with patients
/// </summary>
/// <param name="service">Service for patients repository</param>
/// <param name="logger">Logger for logging</param>
[ApiController]
[Route("api/[controller]")]
public class PatientController(IPatientService service, ILogger<PatientController> logger)
    : CrudBaseController<PatientGetDto, PatientCreateUpdateDto, ObjectId>(service, logger)
{
    /// <summary>
    /// Get appointments where is the patient
    /// </summary>
    /// <param name="id">id of patient</param>
    /// <returns>list of appintments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentGetDto>>> GetAppointmentsById(string id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetAppointmentsById), GetType().Name, id);
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                logger.LogError("An exception happened during {method} method of {controller}", nameof(GetAppointmentsById), GetType().Name);
                return BadRequest("Invalid Id format");
            }
            
            var res = await service.GetAppointmentsByPatient(objectId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAppointmentsById), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAppointmentsById), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}