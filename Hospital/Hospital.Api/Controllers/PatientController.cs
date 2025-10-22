using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

/// <summary>
/// Provides API methods working with patients
/// </summary>
/// <param name="service">Service for patients repository</param>
/// <param name="logger">Logger for logging</param>
[ApiController]
[Route("api/[controller]")]
public class PatientController(IApplicationService<PatientDto, int> service, ILogger<PatientController> logger) 
    : CrudBaseController<PatientDto, int>(service, logger);