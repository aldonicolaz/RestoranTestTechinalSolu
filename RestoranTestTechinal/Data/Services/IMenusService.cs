using RestoranTestTechinal.Data.Base;
using RestoranTestTechinal.Models;
using System.Threading.Tasks;

namespace RestoranTestTechinal.Data.Services
{
    public interface IMenusService : IEntityBaseRepository<Menus>
    {
        Task<Menus> GetMenusByIdAsync(int id);
   
        Task UpdateMenusAsync(NewMenusVM data);
        Task AddNewMenusAsync(NewMenusVM data);

    }
}
