using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
    /// Get doctors with at least some years of experience
    /// </summary>
    /// <param name="year">years</param>
    /// <returns>List of doctors</returns>
    [HttpGet("doctors-with-experience")]
    [ProducesResponseType(typeof(List<DoctorGetDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<DoctorGetDto>>> GetDoctorsWithExperienceAtLeastYears([FromQuery] int year)
    {
        logger.LogInformation("{method} method of {controller} is called with {year} parameter", nameof(GetDoctorsWithExperienceAtLeastYears), GetType().Name, year);
        try
        {
            var res = await analyticsService.GetDoctorsWithExperienceAtLeastYears(year);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetDoctorsWithExperienceAtLeastYears), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetDoctorsWithExperienceAtLeastYears), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
    
    /// <summary>
    /// Get patients assigned to a specific doctor
    /// </summary>
    /// <param name="doctorId">ID</param>
    /// <returns>List of patient</returns>
    [HttpGet("patients-by-doctor")]
    [ProducesResponseType(typeof(List<PatientGetDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PatientGetDto>>> GetPatientsByDoctor([FromQuery] string doctorId)
    {
        logger.LogInformation("{method} method of {controller} is called with {doctorId} parameter", nameof(GetPatientsByDoctor), GetType().Name, doctorId);
        try
        {
            if (!ObjectId.TryParse(doctorId, out var objectId))
            {
                logger.LogError("An exception happened during {method} method of {controller}", nameof(GetPatientsByDoctor), GetType().Name);
                return BadRequest("Invalid Id format");
            }
            
            var res = await analyticsService.GetPatientsByDoctor(objectId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPatientsByDoctor), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPatientsByDoctor), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
    
    /// <summary>
    /// Get counting of repeat patient appointments in specific period.
    /// </summary>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>Counting</returns>
    [HttpGet("count-appointments-with-repeat-visits")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<int>> GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(
        [FromQuery] DateTime start, 
        [FromQuery] DateTime end)
    {
        logger.LogInformation("{method} method of {controller} is called with {start} and {end} parameters", nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), GetType().Name, start, end);
        try
        {
            var res = await analyticsService.GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(start, end);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
    
    /// <summary>
    /// Get patients over some year old who have
    /// appointments with multiple doctors
    /// </summary>
    /// <param name="age">Age of patient</param>
    /// <returns>List of patients</returns>
    [HttpGet("patients-older-than")]
    [ProducesResponseType(typeof(List<PatientGetDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PatientGetDto>>> GetPatientsOlderThanWithMultipleDoctors([FromQuery] int age)
    {
        logger.LogInformation("{method} method of {controller} is called with {age} parameter", nameof(GetPatientsOlderThanWithMultipleDoctors), GetType().Name, age);
        try
        {
            var res = await analyticsService.GetPatientsOlderThanWithMultipleDoctors(age);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPatientsOlderThanWithMultipleDoctors), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPatientsOlderThanWithMultipleDoctors), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
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
    [ProducesResponseType(typeof(List<AppointmentGetDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentGetDto>>> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(
        [FromQuery] int roomId,  
        [FromQuery] DateTime start, 
        [FromQuery] DateTime end)
    {
        logger.LogInformation("{method} method of {controller} is called with {roomId}, {start} and {end} parameters", nameof(GetAppointmentsWhenInSpecificRoomInSpecificPeriod), GetType().Name, roomId, start, end);
        try
        {
            var res = await analyticsService.GetAppointmentsWhenInSpecificRoomInSpecificPeriod(roomId, start, end);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAppointmentsWhenInSpecificRoomInSpecificPeriod), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAppointmentsWhenInSpecificRoomInSpecificPeriod), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}