using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using Remotion.Linq.Clauses;

namespace OdeToFood.Data
{
    public class SqlRestaurantData:IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Restaurant>> GetRestaurantByName(string name="")
        {
            var query= await Task.Run(()=>from r in _db.Restaurants
                where string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower())
                       orderby r.Name
                select r);

            return query;
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _db.Restaurants.FindAsync(id);            
        }

        public Restaurant GetById(int id)
        {
            return _db.Restaurants.Find(id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }

        public async Task<Restaurant> Add(Restaurant newRestaurant)
        {
            await _db.AddAsync(newRestaurant);
            return newRestaurant;
        }

        public async Task<Restaurant> Delete(int id)
        {
            var restaurant = await GetByIdAsync(id);
            if (restaurant != null)
            {
                _db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public async Task<int> GetCountOfRestaurants()
        {
            return  await _db.Restaurants.CountAsync();
        }

        public async Task<int> Commit()
        {
            return await _db.SaveChangesAsync();
        }
    }
}