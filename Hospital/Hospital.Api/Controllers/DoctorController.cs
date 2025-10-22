using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Info about doctor
/// </summary>
/// <param name="service"></param>
[ApiController]
[Route("api/[controller]")]
public class DoctorController(IApplicationService<DoctorDto, int> service, ILogger<DoctorController> logger) 
    : CrudBaseController<DoctorDto, int>(service, logger);