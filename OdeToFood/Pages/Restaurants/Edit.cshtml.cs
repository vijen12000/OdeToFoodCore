using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHeper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        public EditModel(IRestaurantData restaurantData,IHtmlHelper htmlHeper)
        {
            this.restaurantData = restaurantData;
            this.htmlHeper = htmlHeper;
        }
        
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHeper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = this.restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Cuisines = htmlHeper.GetEnumSelectList<CuisineType>();
            if (!ModelState.IsValid)
            {
                return Page();

            }
            if (Restaurant.Id > 0)
            {
                Restaurant = restaurantData.Update(Restaurant);
            }else
            {
                restaurantData.Add(Restaurant);
            }
            restaurantData.Commit();
            TempData["Message"] = "Restaurant Saved!";
            return RedirectToPage("./Detail", new { restaurantId=Restaurant.Id});
                                    
        }
    }
}