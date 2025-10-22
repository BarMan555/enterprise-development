using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;

namespace Hospital.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticController(
    ILibraryAnalyticsService analService, 
    ILogger<AnalyticController> logger) 
    : ControllerBase
{
    
    private ActionResult Logging(string method, Func<ActionResult> action)
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

    [HttpGet("doctors-with-experience")]
    [ProducesResponseType(typeof(List<DoctorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public ActionResult<List<DoctorDto>> GetDoctorsWithExperienceAtLeastYears([FromQuery] int year)
    {
        return Logging(nameof(GetDoctorsWithExperienceAtLeastYears), () =>
        {
            var result = analService.GetDoctorsWithExperienceAtLeastYears(year);
            return Ok(result);
        });
    }
    
    [HttpGet("patients-by-doctor")]
    [ProducesResponseType(typeof(List<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetPatientsByDoctor([FromQuery] int doctorId)
    {
        return Logging(nameof(GetPatientsByDoctor), () =>
        {
            var result = analService.GetPatientsByDoctor(doctorId);
            return Ok(result);
        });
    }
    
    [HttpGet("count-appointments-with-repeat-visits")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public ActionResult<int> GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(
        [FromQuery] DateTime start, 
        [FromQuery] DateTime end)
    {
        return Logging(nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), () =>
        {
            var result = analService.GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(start, end);
            return Ok(result);
        });
    }
    
    [HttpGet("patients-older-than")]
    [ProducesResponseType(typeof(List<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetPatientsOlderThaneWithMultipleDoctors([FromQuery] int age)
    {
        return Logging(nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), () =>
        {
            var result = analService.GetPatientsOlderThaneWithMultipleDoctors(age);
            return Ok(result);
        });
    }
    
    [HttpGet("appointments-in-specific-room")]
    [ProducesResponseType(typeof(List<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public ActionResult<List<AppointmentDto>> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(
        [FromQuery] int roomId,  
        [FromQuery] DateTime start, 
        [FromQuery] DateTime end)
    {
        return Logging(nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), () =>
        {
            var result = analService.GetAppointmentsWhenInSpecificRoomInSpecificPeriod(roomId, start, end);
            return Ok(result);
        });
    }
}