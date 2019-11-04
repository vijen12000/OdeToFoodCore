using OdeToFood.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        Task<IEnumerable<Restaurant>> GetRestaurantByName(string name="");        
        Task<Restaurant> GetByIdAsync(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Task<Restaurant> Add(Restaurant newRestaurant);
        Task<Restaurant> Delete(int id);
        Task<int> GetCountOfRestaurants();
        Task<int> Commit();
    }
}
