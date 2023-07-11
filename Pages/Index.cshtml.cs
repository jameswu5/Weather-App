using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeatherApp.Pages;

[IgnoreAntiforgeryToken(Order = 1001)]

public class IndexModel : PageModel
{
    private readonly Database database = new Database();
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string search = Request.Form["searchField"];
        Location place = database.GetIDFromName(search);

        if (place == null) {
            return Redirect($"Error?searchName={search}");
        } else {
            return Redirect($"Weather?id={place.id}");
        }
    }
}
