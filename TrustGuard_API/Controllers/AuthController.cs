using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrustGuard_API.Data;
using TrustGuard_API.DTOs;
using TrustGuard_API.Models;
using TrustGuard_API.Utility;

namespace TrustGuard_API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private ApiResponse _response;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private string secretKey;

    public AuthController(ApplicationDbContext db, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        _response = new ApiResponse();
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        ApplicationUser userFromDb = _db.ApplicationUsers
                .FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);

        if (isValid == false)
        {
            _response.Result = new LoginResponseDTO();
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username or password is incorrect");
            return BadRequest(_response);
        }

        //generating JWT Token
        var roles = await _userManager.GetRolesAsync(userFromDb);
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(secretKey);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim("fullName", userFromDb.FullName),
                    new Claim("id", userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Email, userFromDb.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        LoginResponseDTO loginResponse = new()
        {
            Email = userFromDb.Email,
            Token = tokenHandler.WriteToken(token)
        };

        if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username or password is incorrect");
            return BadRequest(_response);
        }

        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = loginResponse;
        return Ok(_response);

    }
}