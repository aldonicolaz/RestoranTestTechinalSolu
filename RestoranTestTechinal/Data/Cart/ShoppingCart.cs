using RestoranTestTechinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoranTestTechinal.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
       
            //here i added in the same way other variables and put them in a list
       
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId =  session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            Console.WriteLine(cartId);
            string formatId = "ABC" + DateTime.Now.ToString("dd-MM-yyyy");
            return new ShoppingCart(context) { ShoppingCartId = formatId + cartId };
        }

        public void AddItemToCart(Menus menus)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Menus.Id == menus.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,    
                    Menus = menus,
                    Amount = 1,

                
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void AddNomorMeja(string ShoppingCartId, string nomormeja)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n =>  n.ShoppingCartId == ShoppingCartId);

         
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    NomorMeja = nomormeja,
                    Amount = 1,


                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            
 
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Menus menus)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Menus.Id == menus.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Menus).ToList());
        }

        public double GetShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Menus.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
