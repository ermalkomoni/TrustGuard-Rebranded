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


    [HttpPost]
    public async Task<ActionResult<ApiResponse>> AddOrUpdateItemInCart(string userId, int insuranceTypeId, int updateQuantityBy)
    {
        // Shopping cart will have one entry per user id, even if a user has many items in cart.
        // Cart items will have all the items in shopping cart for a user
        // updatequantityby will have count by with an items quantity needs to be updated
        // if it is -1 that means we have lower a count if it is 5 it means we have to add 5 count to existing count.
        // if updatequantityby by is 0, item will be removed


        // when a user adds a new item to a new shopping cart for the first time
        // when a user adds a new item to an existing shopping cart (basically user has other items in cart)
        // when a user updates an existing item count
        // when a user removes an existing item

        ShoppingCart shoppingCart = _db.ShoppingCarts.Include(u => u.CartItems).FirstOrDefault(u => u.UserId == userId);
        InsuranceType insuranceType = _db.InsuranceTypes.FirstOrDefault(u => u.Id == insuranceTypeId);
        if (insuranceType == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        if (shoppingCart == null && updateQuantityBy > 0)
        {
            //create a shopping cart & add cart item

            ShoppingCart newCart = new() { UserId = userId };
            _db.ShoppingCarts.Add(newCart);
            _db.SaveChanges();

            CartItem newCartItem = new()
            {
                InsuranceTypeId = insuranceTypeId,
                Quantity = updateQuantityBy,
                ShoppingCartId = newCart.Id,
                InsuranceType = null
            };
            _db.CartItems.Add(newCartItem);
            _db.SaveChanges();
        }
        else
        {
            //shopping cart exists

            CartItem cartItemInCart = shoppingCart.CartItems.FirstOrDefault(u => u.InsuranceTypeId == insuranceTypeId);
            if (cartItemInCart == null)
            {
                //item does not exist in current cart
                CartItem newCartItem = new()
                {
                    InsuranceTypeId = insuranceTypeId,
                    Quantity = updateQuantityBy,
                    ShoppingCartId = shoppingCart.Id,
                    InsuranceType = null
                };
                _db.CartItems.Add(newCartItem);
                _db.SaveChanges();
            }
            else
            {
                //item already exist in the cart and we have to update quantity
                int newQuantity = cartItemInCart.Quantity + updateQuantityBy;
                if (updateQuantityBy == 0 || newQuantity <= 0)
                {
                    //remove cart item from cart and if it is the only item then remove cart
                    _db.CartItems.Remove(cartItemInCart);
                    if (shoppingCart.CartItems.Count() == 1)
                    {
                        _db.ShoppingCarts.Remove(shoppingCart);
                    }
                    _db.SaveChanges();
                }
                else
                {
                    cartItemInCart.Quantity = newQuantity;
                    _db.SaveChanges();
                }
            }
        }
        return _response;

    }
}