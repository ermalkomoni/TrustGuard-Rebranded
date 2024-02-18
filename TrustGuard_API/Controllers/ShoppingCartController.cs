using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustGuard_API.Data;
using TrustGuard_API.Models;

namespace TrustGuard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : Controller
{
        protected ApiResponse _response;
        private readonly ApplicationDbContext _db;
        public ShoppingCartController(ApplicationDbContext db)
        {
            _response = new();
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetShoppingCart(string userId)
        {
            try
            {
                ShoppingCart shoppingCart;
                if (string.IsNullOrEmpty(userId))
                {
                    shoppingCart = new();
                }
                else
                { 
                    shoppingCart= _db.ShoppingCarts
                    .Include(u => u.CartItems).ThenInclude(u => u.InsuranceType)
                    .FirstOrDefault(u => u.UserId == userId);

                }
                if (shoppingCart.CartItems != null && shoppingCart.CartItems.Count > 0) {
                    shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.InsuranceType.Price);
                }
                _response.Result = shoppingCart;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }
}