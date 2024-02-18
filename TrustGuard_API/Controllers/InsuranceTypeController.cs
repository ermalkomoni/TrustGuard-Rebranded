using System.Net;
using Microsoft.AspNetCore.Mvc;
using TrustGuard_API.Data;
using TrustGuard_API.Models;

namespace TrustGuard_API.Controllers;

[Route("api/InsuranceType")]
[ApiController]
public class InsuranceTypeController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private ApiResponse _response;
    private readonly IWebHostEnvironment _environment;

    public InsuranceTypeController(ApplicationDbContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _response = new ApiResponse();
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> GetInsuranceTypes()
    {
        _response.Result = _db.InsuranceTypes;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpGet("{id:int}", Name = "GetInsuranceType")]
    public async Task<IActionResult> GetInsuranceType(int id)
    {
        if (id == 0)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }

        InsuranceType insuranceType = _db.InsuranceTypes.FirstOrDefault(u => u.Id == id);
        if (insuranceType == null)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            return NotFound(_response);
        }

        _response.Result = insuranceType;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }
}