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
    : CrudBaseController<PatientDto, int>(service, logger)
{
    /// <summary>
    /// Get appointments where is the patient
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appintments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError),]
    public ActionResult<List<AppointmentDto>> GetAppointmentsById(int id)
    {
        return Logging(
            nameof(GetAppointmentsById), 
            () => Ok(service.GetAppointmentsByPatient(id))
        );
    }
}