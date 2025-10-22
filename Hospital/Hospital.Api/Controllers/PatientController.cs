using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Info about patient
/// </summary>
/// <param name="service"></param>
[ApiController]
[Route("api/[controller]")]
public class PatientController(IApplicationService<PatientDto, int> service, ILogger<PatientController> logger) 
    : CrudBaseController<PatientDto, int>(service, logger);