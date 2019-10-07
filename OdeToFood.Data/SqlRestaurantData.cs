using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            var query= from r in _db.Restaurants
                where r.Name.ToLower().StartsWith(name.ToLower()) || string.IsNullOrEmpty(name)
                orderby r.Name
                select r;

            return query;
        }

        public Restaurant GetById(int id)
        {
            var restaurant=_db.Restaurants.Find(id);
            return restaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _db.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                _db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}