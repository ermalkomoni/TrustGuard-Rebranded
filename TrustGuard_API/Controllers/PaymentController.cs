using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TrustGuard_API.Data;
using TrustGuard_API.Models;

namespace TrustGuard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    protected ApiResponse _response;
    private readonly IConfiguration _congifuration;
    private readonly ApplicationDbContext _db;
    public PaymentController(IConfiguration configuration, ApplicationDbContext db)
    {
        _congifuration = configuration;
        _db = db;
        _response = new();
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> MakePayment(string userId)
    {
        ShoppingCart shoppingCart = _db.ShoppingCarts
            .Include(u => u.CartItems)
            .ThenInclude(u => u.InsuranceType).FirstOrDefault(u => u.UserId == userId);

        if (shoppingCart == null || shoppingCart.CartItems == null || shoppingCart.CartItems.Count() == 0)
        {
            _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }

        #region Create Payment Intent

        StripeConfiguration.ApiKey = _congifuration["StripeSettings:SecretKey"];
        shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.InsuranceType.Price);

        PaymentIntentCreateOptions options = new()
        {
            Amount = (int)(shoppingCart.CartTotal * 100),
            Currency = "eur",
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
        };
        PaymentIntentService service = new();
        PaymentIntent response = service.Create(options);
        shoppingCart.StripePaymentIntentId = response.Id;
        shoppingCart.ClientSecret = response.ClientSecret;


        #endregion

        _response.Result = shoppingCart;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }    
}