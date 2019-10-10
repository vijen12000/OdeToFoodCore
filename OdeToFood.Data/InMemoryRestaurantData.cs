using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name="Vijender's Pizaa", Location="Maryland",Cuisine=CuisineType.Italian},
                new Restaurant { Id = 2, Name="Cinnamon Club", Location="Londan",Cuisine=CuisineType.Indian},
                new Restaurant { Id = 3, Name="La Costa", Location="California",Cuisine=CuisineType.Mexican},
            };
        }

        public async Task<Restaurant> Add(Restaurant newRestaurant)
        {
            await Task.Run(() => restaurants.Add(newRestaurant));            
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public async Task<Restaurant> Delete(int id)
        {
            var restaurant = await GetByIdAsync(id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public async Task<int> GetCountOfRestaurants()
        {
            return await Task.Run(() => restaurants.Count);
        }

        public async Task<int> Commit()
        {
            return await Task.Run(() => 0);
        }
        
        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await Task.Run(() => restaurants.SingleOrDefault(r => r.Id == id));
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantByName(string name=null)
        {
            return await Task.Run(() => from r in restaurants
                where string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower())
                orderby r.Name
                select r);

            //return from r in restaurants
            //    where string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower())
            //    orderby r.Name
            //    select r;
        }
        
        public Restaurant Update(Restaurant updatedRestaurant)
        {            
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);

            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }      
    }
}