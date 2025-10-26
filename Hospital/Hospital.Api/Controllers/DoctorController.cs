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
    : CrudBaseController<DoctorDto, int>(service, logger)
{
    /// <summary>
    /// Get appointment where is the doctor
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError),]
    public ActionResult<List<AppointmentDto>> GetAppointmentsById(int id)
    {
        return Logging(
            nameof(GetAppointmentsById), 
            () => Ok(service.GetAppointmentsByDoctor(id))
        );
    }
}