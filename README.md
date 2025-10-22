# Разработка корпоративных приложений
## Задание «Поликлиника»
В базе данных поликлиники содержится информация о записях пациентов на прием к врачам.

Пациент характеризуется номером паспорта, ФИО, полом, датой рождения, адресом, группой крови, резус фактором и контактным телефоном.
Пол пациента является перечислением.
Группа крови пациента является перечислением.
Резус фактор пациента является перечислением.

Информация о враче включает номер паспорта, ФИО, год рождения, специализацию, стаж работы.
Специализация врача является справочником.

При записи на прием пациента в базе данных фиксируется дата и время приема, номер кабинета, а также индикатор того, является ли прием повторным.

### Классы
* [Patient](./Hospital/Hospital.Domain/Models/Patient.cs) - класс описывает пациента
* [Doctor](./Hospital/Hospital/Hospital.Domain/Models/Doctor.cs) - класс описывает доктора
* [Appointment](./Hospital/Hospital/Hospital.Domain/Models/Appointment.cs) - класс описывает прием к врачу

### Перечисления
* [BloodType](./Hospital/Hospital.Domain/Enums/BloodType.cs) - группа крови пациента
* [Gender](./Hospital/Hospital.Domain/Enums/Gender.cs) - гендер пациента
* [RhFactor](./HospitalHospital.Domain/Enums/RhFactor.cs) - резус фактор пациента
* [Speciallization](./Hospital/Hospital.Domain/Enums/Speciallization.cs) - специализация врача

### Тесты
[HospitalRepoTests](./Hospital/Hospital.Tests/HospitalRepoTests.cs) - заданные по варианту юнит-тесты 
1. GetDoctorsWithExperience_WhenExperienceAtLeast10Years_ReturnsExperiencedDoctorsOrderedByName() - Вывести информацию о всех врачах, стаж работы которых не менее 10 лет.
2. GetPatientsByDoctor_WhenDoctorIsSpecified_ReturnsPatientsOrderedByName() - Вывести информацию о всех пациентах, записанных на прием к указанному врачу, упорядочить по ФИО.
3. CountAppointments_WhenRepeatVisitsInLastMonth_ReturnsCorrectCount() - Вывести информацию о количестве повторных приемов пациентов за последний месяц.
4. GetPatients_WhenOver30WithMultipleDoctors_ReturnsPatientsOrderedByBirthDate() - Вывести информацию о пациентах старше 30 лет, которые записаны на прием к нескольким врачам, упорядочить по дате рождения.
5. GetAppointments_WhenInSpecificRoomCurrentMonth_ReturnsAppointmentsOrderedByDateTime() - Вывести информацию о приемах за текущий месяц, проходящих в выбранном кабинете.

[HospitalRepoFixture](./Hospital/Hospital.Tests/Fixtures/HospitalRepoFixture.cs) - фикстура, использующая для заполнения репозитории.

### Hospital.Infrastructure.InMemory - Слой для доступа к данным
- **Repositories** - Реализации репозиториев:
    - [InMemoryPatientRepository.cs](./Hospital/Hospital.Infrastructure.InMemory/Repositories/InMemoryPatientRepository.cs)
    - [InMemoryDoctorRepository.cs](./Hospital/HospitalHospital.Infrastructure/Repositories/InMemoryDoctorRepository.cs)
    - [InMemoryAppointmentRepository.cs](./Hospital/Hospital.Infrastructure/Repositories/InMemoryAppointmentRepository.cs)
    - [InMemoryRepository.cs](./Hospital/Hospital.Infrastructure/Repositories/InMemoryRepository.cs)

### Hospital.Application.Contracts - Контракты для сервисного слоя
#### Dtos
- DTO:
    - [AppointmentDto.cs](./Hospital/Hospital.Application.Contracts/Dtos/AppointmentDto.cs)  - Для приема.
    - [DoctorDto.cs](./Hospital/Hospital.Application.Contracts/Dtos/DoctorDto.cs)  - Для доктора.
    - [PatientDto.cs](./Hospital/Hospital.Application.Contracts/Dtos/PatientDto.cs)  - Для пациента.
   
#### Interfaces - Контракты сервисов
- [IApplicationService.cs](./Hospital/Hospital.Application.Contracts/Interfaces/IApplicationService.cs) - Интерфейс для CRUD операций.
- [ILibraryAnalyticsService.cs](./Hospital/Hospital.Application.Contracts/Interfaces/ILibraryAnalyticsService.cs) - Интерфейс для аналитической службы.

### Hospital.Application - Сервисы
#### Services - Реализации сервисов с CRUD операциями
- [PatientService.cs](./Hospital/Hospital.Application/Services/PatientService.cs) - Для пациентов.
- [DoctorService.cs](./Hospital/Hospital.Application/Services/DoctorService.cs) - Для докторов.
- [AppointmentService.cs](./Hospital/Hospital.Application/Services/AppointmentService.cs) - Для приемов к врачу.
- [LibraryAnalyticsService.cs](./Hospital/Hospital.Application/Services/LibraryAnalyticsService.cs) - Сервис аналитических запросов.

- [MappingProfile.cs](./Hospital/Hospital.Application/MappingProfile.cs) - Настройки AutoMapper для преобразования между DTO и доменной областью.


### Library.Api - Веб-API
#### Controllers - API контроллеры
- [AnalyticsController.cs](./Hospital/Hospital.Api/Controllers/AnalyticController.cs) - Контроллер для аналитических запросов (содержит те же запросы, которые проверяются в юнит-тестах).
- [PatientController.cs](./Hospital/Hospital.Api/Controllers/PatientController.cs) - Управление пациентами.
- [DoctorController.cs](./Hospital/Hospital.Api/Controllers/DoctorController.cs) - Управление докторами.
- [AppointmentController.cs](./Hospital/Hospital.Api/Controllers/AppointmentController.cs) - Управление приемами к врачу.
- [CrudControllerBase.cs](./Hospital/Hospital.Api/Controllers/CrudControllerBase.cs) - Базовый класс для CRUD операций.