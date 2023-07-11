using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeatherApp.Pages
{
    public class WeatherModel : PageModel
    {
        public int locationID;
        public Database database = new Database();
        private string APIKey = "89363ab3-9219-485e-aa5c-e316753fdc6c";
        public Location place;
        public List<Day> weatherInfo;

        public async Task OnGetAsync(int id)
        {
            Scraper scraper = new Scraper(APIKey);
            locationID = id;
            place = database.GetLocationFromID(locationID);
            if (place == null) {
                Console.WriteLine("Place not found");
            } else {
                weatherInfo = await scraper.GetWeatherInLocation(place.id);
            }
        }
    }
}
