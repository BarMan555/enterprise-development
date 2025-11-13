using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;

namespace Hospital.Api.Controllers;

/// <summary>
/// Provides API methods for analytical queries
/// </summary>
/// <param name="analyticsService">Analytical service</param>
/// <param name="logger">Logger for recording information</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticController(
    ILibraryAnalyticsService analyticsService, 
    ILogger<AnalyticController> logger) 
    : ControllerBase
{
    
    /// <summary>
    /// Helper method for consistent logging and error handling.
    /// </summary>
    private async Task<ActionResult> Logging(string method, Func<ActionResult> action)
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

    /// <summary>
    /// Get doctors with at least some years of experience
    /// </summary>
    /// <param name="year">years</param>
    /// <returns>List of doctors</returns>
    [HttpGet("doctors-with-experience")]
    [ProducesResponseType(typeof(List<DoctorGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<DoctorGetDto>>> GetDoctorsWithExperienceAtLeastYears([FromQuery] int year)
    {
        return await Logging(nameof(GetDoctorsWithExperienceAtLeastYears), () =>
        {
            var result = analyticsService.GetDoctorsWithExperienceAtLeastYears(year);
            return Ok(result);
        });
    }
    
    /// <summary>
    /// Get patients assigned to a specific doctor
    /// </summary>
    /// <param name="doctorId">ID</param>
    /// <returns>List of patient</returns>
    [HttpGet("patients-by-doctor")]
    [ProducesResponseType(typeof(List<PatientGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PatientGetDto>>> GetPatientsByDoctor([FromQuery] int doctorId)
    {
        return await Logging(nameof(GetPatientsByDoctor), () =>
        {
            var result = analyticsService.GetPatientsByDoctor(doctorId);
            return Ok(result);
        });
    }
    
    /// <summary>
    /// Get counting of repeat patient appointments in specific period.
    /// </summary>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>Counting</returns>
    [HttpGet("count-appointments-with-repeat-visits")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<int>> GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(
        [FromQuery] DateTime start, 
        [FromQuery] DateTime end)
    {
        return await Logging(nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), () =>
        {
            var result = analyticsService.GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(start, end);
            return Ok(result);
        });
    }
    
    /// <summary>
    /// Get patients over some year old who have
    /// appointments with multiple doctors
    /// </summary>
    /// <param name="age">Age of patient</param>
    /// <returns>List of patients</returns>
    [HttpGet("patients-older-than")]
    [ProducesResponseType(typeof(List<PatientGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PatientGetDto>>> GetPatientsOlderThanWithMultipleDoctors([FromQuery] int age)
    {
        return await Logging(nameof(GetPatientsOlderThanWithMultipleDoctors), () =>
        {
            var result = analyticsService.GetPatientsOlderThanWithMultipleDoctors(age);
            return Ok(result);
        });
    }
    
    /// <summary>
    /// Get appointments in specific period
    /// happening in a specific room. 
    /// </summary>
    /// <param name="roomId">ID of room</param>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>List of appointments</returns>
    [HttpGet("appointments-in-specific-room")]
    [ProducesResponseType(typeof(List<AppointmentGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentGetDto>>> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(
        [FromQuery] int roomId,  
        [FromQuery] DateTime start, 
        [FromQuery] DateTime end)
    {
        return await Logging(nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), () =>
        {
            var result = analyticsService.GetAppointmentsWhenInSpecificRoomInSpecificPeriod(roomId, start, end);
            return Ok(result);
        }); 
    }
}