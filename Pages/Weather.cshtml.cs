using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeatherApp.Pages
{
    public class WeatherModel : PageModel
    {
        public int locationID;
        public Database database = new Database();
        public Location place;
        public List<Day> weatherInfo;

        public async Task OnGetAsync(int id)
        {
            Scraper scraper = new Scraper(Program.ReadAPIKey("APIKey.txt"));
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
