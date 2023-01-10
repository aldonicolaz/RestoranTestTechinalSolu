using RestoranTestTechinal.Data.Base;
using RestoranTestTechinal.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RestoranTestTechinal.Data.Services
{
    public class MenusService : EntityBaseRepository<Menus>, IMenusService
    {
        private readonly AppDbContext _context;
        public MenusService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMenusAsync(NewMenusVM data)
        {
            var newMenu = new Menus()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                IsInStock = data.IsInStock,
                Category = data.Category

            };
            await _context.Menus.AddAsync(newMenu);
            await _context.SaveChangesAsync();


        }

        public async Task<Menus> GetMenusByIdAsync(int id)
        {
            var menus = await _context.Menus.FirstOrDefaultAsync(n => n.Id == id);

            return menus;
        }

        public async Task UpdateMenusAsync(NewMenusVM data)
        {
           var dataselect = await _context.Menus.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dataselect != null)
            {
                dataselect.Name = data.Name;
                dataselect.Description = data.Description;
                dataselect.Price = data.Price;
                dataselect.ImageURL = data.ImageURL;
                dataselect.Category =  data.Category;
                dataselect.IsInStock = data.IsInStock;

                 await _context.SaveChangesAsync();
            }    
        }
    }
}
