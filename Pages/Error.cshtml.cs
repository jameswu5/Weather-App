using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeatherApp.Pages
{
    public class ErrorModel : PageModel
    {
        public string search;
        public List<Location> suggestions;

        public async Task OnGetAsync(string searchName) {
            Database database = new Database();
            search = searchName;
            suggestions = database.GetIDFromSearch(search);
        }
    }
}