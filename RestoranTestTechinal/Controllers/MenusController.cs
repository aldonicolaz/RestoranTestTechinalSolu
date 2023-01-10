using RestoranTestTechinal.Data.Services;
using RestoranTestTechinal.Data.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using RestoranTestTechinal.Models;

namespace RestoranTestTechinal.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MenusController : Controller
    {
        private readonly IMenusService _service;

        public MenusController(IMenusService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMenus = await _service.GetAllAsync();
            var top5 = allMenus.Take(5);
            return View(top5);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMenus = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMenus.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                //var filteredResultNew = allMenus.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allMenus);
        }
       // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMenusVM menu)
     {
            await _service.AddNewMenusAsync(menu);
            return RedirectToAction(nameof(Index));
        }


        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var menu = await _service.GetMenusByIdAsync(id);
            if (menu == null) return View("NotFound");

            var response = new NewMenusVM()
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Price = menu.Price,
                
                ImageURL = menu.ImageURL
        
            };

   

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMenusVM menu)
        {
            if (id != menu.Id) return View("NotFound");

           

            await _service.UpdateMenusAsync(menu);
            return RedirectToAction(nameof(Index));
        }
        //GET: Menus/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var menusDetail = await _service.GetMenusByIdAsync(id);
            return View(menusDetail);
        }



    }
}