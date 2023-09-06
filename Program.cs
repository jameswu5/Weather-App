namespace WeatherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await PopulateDatabase();
            new App(args);
        }

        public static string ReadAPIKey(string textFileName) {
            string text = File.ReadAllText(textFileName);
            return text;
        }

        static async Task PopulateDatabase() {
            string APIKey = ReadAPIKey("APIKey.txt");
            Scraper scraper = new Scraper(APIKey);
            Database database = new Database();

            string responseContent = await scraper.Query("val/wxfcs/all/json/sitelist?");
            List<Location> locations = scraper.ParseLocationResponse(responseContent);
            database.InsertLocations(locations);
        }
    }
}