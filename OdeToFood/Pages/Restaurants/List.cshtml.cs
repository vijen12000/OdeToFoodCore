using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        public IConfiguration Configuration { get; }
        
        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration configuration, IRestaurantData restaurantData)
        {
            Configuration = configuration;
            this.restaurantData = restaurantData;
        }

        public async void OnGet()
        {            
            Message = Configuration["Message"];
            Restaurants = await this.restaurantData.GetRestaurantByName(SearchTerm);
        }
    }
}