using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using CartStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        CartRepository _cartRepository = new CartRepository();

        [HttpGet("getCart/{userid}")]
        public IActionResult GetCart(int userid, int pageIndex=1,int pageSize=10, string? keyword="")
        {
            if(userid == 0)
            {
                return BadRequest("Please Login!! Invalid  UserId");
            }
            var cart = _cartRepository.GetCart(userid, pageIndex, pageSize, keyword);
            //ListResponse<CartModel> listResponse = new ListResponse<CartModel>()
            //{
            //    Results = cart.Results.Select(c => new CartModel(c)).ToList(),
            //    Totalrecords = cart.Totalrecords
            //};

            ListResponse<CartResModel> listResponse = new ListResponse<CartResModel>()
            {
                Results = cart.Results.Select(c => new CartResModel(c)).ToList(),
                Totalrecords = cart.Totalrecords
            };

            return Ok(listResponse);
        }

        [HttpPost("addToCart")]
        public IActionResult AddToCart(CartModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            if (_cartRepository.CartItemExist(model.Bookid))
            {
                return Conflict();
            }

            Cart cart = new Cart()
            {
                Id = model.Id,
                Userid=model.Userid,
                Bookid = model.Bookid,
                Quantity = 1
            };
            var entry = _cartRepository.AddToCart(cart);

            CartModel bookModel = new CartModel(entry);

            return Ok(bookModel);
        }

        [HttpGet("getCartItem/{id}")]
        public IActionResult GetCartItem(int id)
        {
            var cart = _cartRepository.GetCartItem(id);

            if (cart == null)
            {
                return NotFound();
            }

            CartModel cartModel = new CartModel(cart);

            return Ok(cartModel);
        }

        [HttpPut("updateCartItem")]
        public IActionResult UpdateCartItem(CartModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            Cart cart = new Cart()
            {
                Id = model.Id,
                Userid = model.Userid,
                Bookid = model.Bookid,
                Quantity = model.Quantity
            };
            var entry = _cartRepository.UpdateCartItem(cart);

            CartModel cartModel = new CartModel(entry);

            return Ok(cartModel);
        }

        [HttpDelete("deleteBook/{id}")]
        //[ProducesResponseType(typeof(bool), 200)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult DeleteCartItem(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please enter valid Id");
            }
            var entry = _cartRepository.DeleteCartItem(id);
            return Ok(entry);
        }
    }
}
