using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface for analytic service
/// </summary>
public interface ILibraryAnalyticsService
{
    /// <summary>
    /// Get doctors with at least some years of experience
    /// </summary>
    /// <param name="year">years</param>
    /// <returns>List of doctors</returns>
    public Task<List<DoctorGetDto>> GetDoctorsWithExperienceAtLeastYears(int year);
    
    /// <summary>
    /// Get patients assigned to a specific doctor
    /// </summary>
    /// <param name="doctorId">ID</param>
    /// <returns>List of patient</returns>
    public Task<List<PatientGetDto>> GetPatientsByDoctor(int doctorId);
    
    /// <summary>
    /// Get counting of repeat patient appointments in specific period.
    /// </summary>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>Counting</returns>
    public Task<int> GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(DateTime start, DateTime end);
    
    /// <summary>
    /// Get patients over some year old who have
    /// appointments with multiple doctors
    /// </summary>
    /// <param name="age">Age of patient</param>
    /// <returns>List of patients</returns>
    public Task<List<PatientGetDto>> GetPatientsOlderThanWithMultipleDoctors(int age);
    
    /// <summary>
    /// Get appointments in specific period
    /// happening in a specific room. 
    /// </summary>
    /// <param name="roomId">ID of room</param>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>List of appointments</returns>
    public Task<List<AppointmentGetDto>> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(int roomId, DateTime start, DateTime end);
}