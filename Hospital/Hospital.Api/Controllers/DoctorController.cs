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
public class DoctorController(IApplicationService<DoctorDto, int> service, ILogger<DoctorController> logger) 
    : CrudBaseController<DoctorDto, int>(service, logger);