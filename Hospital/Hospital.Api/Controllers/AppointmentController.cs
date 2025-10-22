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
public class AppointmentController(IApplicationService<AppointmentDto, int> service, ILogger<AppointmentController> logger) 
    : CrudBaseController<AppointmentDto, int>(service, logger);