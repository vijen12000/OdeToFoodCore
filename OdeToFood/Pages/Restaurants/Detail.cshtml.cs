using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    { 
        [TempData]
        public string Message { get; set; }

        public Restaurant Restaurant { get; set; }
        public IRestaurantData RestaurantData { get; }

        public DetailModel(IRestaurantData restaurantData)
        {
            RestaurantData = restaurantData;
        }

        public async Task<IActionResult> OnGet(int restaurantId)
        {            
            Restaurant = await RestaurantData.GetByIdAsync(restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}