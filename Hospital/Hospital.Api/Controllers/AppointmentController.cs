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
    public ActionResult<PatientGetDto> GetPatientById(int id)
    {
        return Logging(
            nameof(GetPatientById),
            () => Ok(service.GetParientByAppointment(id))
        );
    }
    
    /// <summary>
    /// Get doctor of the appointment by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>patient</returns>
    [HttpGet("{id}/doctor")]
    [ProducesResponseType(typeof(DoctorGetDto), 200)]
    [ProducesResponseType(500)]
    public ActionResult<DoctorGetDto> GetDoctorById(int id)
    {
        return Logging(
            nameof(GetDoctorById),
            () => Ok(service.GetDoctorByAppointment(id))
        );
    }
}