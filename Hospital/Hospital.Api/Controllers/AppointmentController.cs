using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Info about appointment
/// </summary>
/// <param name="service"></param>
[ApiController]
[Route("api/[controller]")]
public class AppointmentController(IApplicationService<AppointmentDto, int> service, ILogger<AppointmentController> logger) 
    : CrudBaseController<AppointmentDto, int>(service, logger);