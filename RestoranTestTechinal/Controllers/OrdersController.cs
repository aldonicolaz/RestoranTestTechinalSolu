using RestoranTestTechinal.Data.Cart;
using RestoranTestTechinal.Data.Services;
using RestoranTestTechinal.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using RestoranTestTechinal.Data.Static;
using Microsoft.AspNetCore.Identity;

namespace RestoranTestTechinal.Controllers
{

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMenusService _menusService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMenusService menusService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _menusService = menusService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }
        

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

  
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            
            var item = await _menusService.GetMenusByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddNomorMeja(string ShoppingCartId, string nomormeja)
        {

            _shoppingCart.AddNomorMeja(ShoppingCartId, nomormeja);
            
            return RedirectToAction(nameof(ShoppingCart));
        }
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _menusService.GetMenusByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
