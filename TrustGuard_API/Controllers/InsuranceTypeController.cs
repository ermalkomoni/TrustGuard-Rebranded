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
        _db= db;
        _response= new ApiResponse();
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> GetInsuranceTypes() 
    { 
        _response.Result = _db.InsuranceTypes;
        _response.StatusCode=HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpGet("{id:int}",Name = "GetInsuranceType")]
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


    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse>> UpdateInsuranceType(int id, [FromForm] InsuranceTypeUpdateDTO insuranceTypeUpdateDTO, [FromServices] IWebHostEnvironment hostingEnvironment)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (insuranceTypeUpdateDTO == null) // || id != insuranceTypeUpdateDTO.Id
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }
    
                InsuranceType insuranceTypeFromDb = await _db.InsuranceTypes.FindAsync(id);
                if (insuranceTypeFromDb == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }
    
                insuranceTypeFromDb.Name = insuranceTypeUpdateDTO.Name;
                insuranceTypeFromDb.Price = insuranceTypeUpdateDTO.Price;
                insuranceTypeFromDb.Category = insuranceTypeUpdateDTO.Category;
                insuranceTypeFromDb.SpecialTag = insuranceTypeUpdateDTO.SpecialTag;
                insuranceTypeFromDb.Description = insuranceTypeUpdateDTO.Description;
                
                using (var stream = new MemoryStream())
                {
                     await insuranceTypeUpdateDTO.File.CopyToAsync(stream);
                     var imageBytes = stream.ToArray();
                     var base64Image = Convert.ToBase64String(imageBytes);
                        
                     //storing image
                     insuranceTypeFromDb.Image = base64Image;
                }

                _db.InsuranceTypes.Update(insuranceTypeFromDb);
                _db.SaveChanges();
    
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
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
    
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse>> DeleteInsuranceType(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }
        
            InsuranceType insuranceTypeFromDb = await _db.InsuranceTypes.FindAsync(id);
            if (insuranceTypeFromDb == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }
    
            _db.InsuranceTypes.Remove(insuranceTypeFromDb);
            _db.SaveChanges();
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    // [NonAction]
    // public async Task <string> SaveImage(IFormFile imageFile)
    // {
    //     string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray())
    //         .Replace(' ', '-');
    //     imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
    //     var imagePath = Path.Combine(_environment.ContentRootPath, "Images", imageName);
    //     using (var fileStream = new FileStream(imagePath, FileMode.Create))
    //     {
    //         await imageFile.CopyToAsync(fileStream);
    //     }
    //
    //     return imageName;
    //
    // } 
    
    
}