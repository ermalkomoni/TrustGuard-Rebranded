using System.Net;
using Microsoft.AspNetCore.Mvc;
using TrustGuard_API.Data;
using TrustGuard_API.DTOs;
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

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateInsuranceType([FromForm] InsuranceTypeCreateDTO insuranceTypeCreateDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (insuranceTypeCreateDto.File == null || insuranceTypeCreateDto.File.Length == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                // Convert the image file to base64
                using (var stream = new MemoryStream())
                {
                    await insuranceTypeCreateDto.File.CopyToAsync(stream);
                    var imageBytes = stream.ToArray();
                    var base64Image = Convert.ToBase64String(imageBytes);

                    InsuranceType menuItemToCreate = new()
                    {
                        Name = insuranceTypeCreateDto.Name,
                        Price = insuranceTypeCreateDto.Price,
                        Category = insuranceTypeCreateDto.Category,
                        SpecialTag = insuranceTypeCreateDto.SpecialTag,
                        Description = insuranceTypeCreateDto.Description,
                        Image = base64Image // Store the image as base64 string
                    };

                    _db.InsuranceTypes.Add(menuItemToCreate);
                    _db.SaveChanges();
                    _response.Result = menuItemToCreate;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItemToCreate.Id }, _response);
                }
            }
            else
            {
                _response.IsSuccess = false;
            }

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }
}