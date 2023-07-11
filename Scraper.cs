using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.Json;


namespace WeatherApp
{
    public class Scraper
    {
        static readonly HttpClient client = new HttpClient();
        readonly string APIKey;

        public Scraper(string APIKey) {
            this.APIKey = APIKey;
        }

        public async Task<string> Query(string resource) {
            string apiUrl = $"http://datapoint.metoffice.gov.uk/public/data/{resource}key={this.APIKey}";
            try {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode == true) {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                } else {
                    Console.WriteLine("API call unsuccessful.");
                }
            } catch (Exception ex) {
                Console.WriteLine($"Exception {ex} thrown.");
            }
            return "";
        }


        public async Task<List<Day>> GetWeatherInLocation(int locationID) {
            string responseContent = await Query($"val/wxfcs/all/json/{locationID}?res=3hourly&");
            return ParseWeatherResponse(responseContent);
        }

        public List<Day> ParseWeatherResponse(string responseContent) {

            List<Day> result = new List<Day>();

            try {
                JObject responseObject = JObject.Parse(responseContent);
                List<JToken> information = responseObject["SiteRep"]["DV"]["Location"]["Period"].ToList();

                foreach (JToken dayInfo in information) {
                    Day dayWeather = new Day((string)dayInfo["value"]);

                    foreach (JToken hourInfo in dayInfo["Rep"]) {
                        Weather weatherObject = new Weather(hourInfo);
                        dayWeather.weathers.Add(weatherObject);
                    }

                    dayWeather.GetTodayTemp();
                    dayWeather.GetTodayWeather();
                    result.Add(dayWeather);
                }
            }

            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return result;            
        }
    
        public List<Location> ParseLocationResponse(string responseContent) {
            List<Location> locations = new List<Location>();

            try {
                JObject responseObject = JObject.Parse(responseContent);
                List<JToken> information = responseObject["Locations"]["Location"].ToList();
                foreach (JToken info in information) {
                    int id = (int)info["id"];
                    string name = (string)info["name"];
                    locations.Add(new Location(id, name));
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return locations;
        }
    }
}