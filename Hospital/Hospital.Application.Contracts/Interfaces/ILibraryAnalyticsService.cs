using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

public interface ILibraryAnalyticsService
{
    public List<DoctorDto> GetDoctorsWithExperienceAtLeastYears(int year);
    public List<PatientDto> GetPatientsByDoctor(int doctorId);
    public int GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(DateTime start, DateTime end);
    public List<PatientDto> GetPatientsOlderThaneWithMultipleDoctors(int age);
    public List<AppointmentDto> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(int roomId, DateTime start, DateTime end);
}