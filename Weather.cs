using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    public class Day
    {
        public string date;
        public List<Weather> weathers;
        public int todayTempHigh;
        public int todayTempLow;
        public string todayWeather;
        public string todayWeatherIcon;

        public Day(string date) {
            this.date = ConvertDate(date);
            this.weathers = new List<Weather>();
            this.todayTempHigh = -100;
            this.todayTempLow = 100;
            this.todayWeather = "";
            this.todayWeatherIcon = "";
        }

        public void GetTodayTemp() {
            foreach (Weather weather in this.weathers) {
                this.todayTempHigh = Math.Max(this.todayTempHigh, weather.temperature);
                this.todayTempLow = Math.Min(this.todayTempLow, weather.temperature);
            }
            
            /* if (this.weathers.Count < 4) {
                this.todayTemp = this.weathers[0].temperature;
            } else {
                foreach (Weather weather in this.weathers) {
                    if (weather.hour == 12) {
                        this.todayTemp = weather.temperature;
                    }
                }
            } */
        }

        public void GetTodayWeather() {
            if (this.weathers.Count < 4) {
                this.todayWeather = this.weathers[0].weatherType;
                this.todayWeatherIcon = this.weathers[0].weatherIcon;
            } else {
                int index = 0;
                while (this.weathers[index].hour <= 12) {
                    index++;
                }
                if (this.weathers[index-1].weatherIcon == this.weathers[index+1].weatherIcon) {
                    this.todayWeather = this.weathers[index-1].weatherType;
                    this.todayWeatherIcon = this.weathers[index-1].weatherIcon;
                } else {
                    this.todayWeather = this.weathers[index].weatherType;
                    this.todayWeatherIcon = this.weathers[index].weatherIcon;
                }
            }
        }

        public string ConvertDate(string date) {
            DateTime dateObject = DateTime.Parse(date.Substring(0, date.Length - 1));
            return dateObject.ToString("ddd d MMM");
        }
    }

    public class Weather
    {
        public int feelsLikeTemp;
        public int windGust;
        public int screenRelativeHumidity;
        public int temperature;
        public string visibility;
        public string windDirection;
        public int windSpeed;
        public int maxUVIndex;
        public string weatherType;
        public string weatherIcon;
        public int precipitationProbability;
        public int hour;

        public Weather(JToken information) {
            this.feelsLikeTemp = (int) information["F"];
            this.windGust = (int) information["G"];
            this.screenRelativeHumidity = (int) information["H"];
            this.temperature = (int) information["T"];
            this.visibility = ConvertVisibility((string) information["V"]);
            this.windDirection = (string) information["D"];
            this.windSpeed = (int) information["S"];
            this.maxUVIndex = (int) information["U"];
            this.weatherType = ConvertWeatherType((string) information["W"]);
            this.weatherIcon = ConvertWeatherIcon((string) information["W"]);
            this.precipitationProbability = (int) information["Pp"];
            this.hour = ConvertToHour((int) information["$"]);
        }

        private string ConvertVisibility(string visibility) {
            Dictionary<string, string> visibilityData = new Dictionary<string, string>() {
                {"UN", "Unknown"},
                {"VP", "Very poor"},
                {"PO", "Poor"},
                {"MO", "Moderate"},
                {"GO", "Good"},
                {"VG", "Very good"},
                {"EX", "Excellent"}
            };

            if (visibilityData.ContainsKey(visibility)) {
                return visibilityData[visibility];
            } else {
                Console.WriteLine($"Visiblity code {visibility} not found.");
                return "";
            }
        }

        private string ConvertWeatherType(string weatherType) {
            Dictionary<string, string> weatherData = new Dictionary<string, string>() {
                {"NA", "Not available"},
                {"-1", "Trace rain"},
                {"0", "Clear night"},
                {"1", "Sunny day"},
                {"2", "Partly cloudy (night)"},
                {"3", "Partly cloudy (day)"},
                {"4", "Not used"},
                {"5", "Mist"},
                {"6", "Fog"},
                {"7", "Cloudy"},
                {"8", "Overcast"},
                {"9", "Light rain shower (night)"},
                {"10", "Light rain shower (day)"},
                {"11", "Drizzle"},
                {"12", "Light rain"},
                {"13", "Heavy rain shower (night)"},
                {"14", "Heavy rain shower (day)"},
                {"15", "Heavy rain"},
                {"16", "Sleet shower (night)"},
                {"17", "Sleet shower (day)"},
                {"18", "Sleet"},
                {"19", "Hail shower (night)"},
                {"20", "Hail shower (day)"},
                {"21", "Hail"},
                {"22", "Light snow shower (night)"},
                {"23", "Light snow shower (day)"},
                {"24", "Light snow"},
                {"25", "Heavy snow shower (night)"},
                {"26", "Heavy snow shower (day)"},
                {"27", "Heavy snow"},
                {"28", "Thunder shower (night)"},
                {"29", "Thunder shower (day)"},
                {"30", "Thunder"}
            };

            if (weatherData.ContainsKey(weatherType)) {
                return weatherData[weatherType];
            } else {
                Console.WriteLine($"Weather type {weatherType} not found.");
                return "";
            }
        }

        private int ConvertToHour(int minutes) {
            return minutes / 60;
        }

        private string ConvertWeatherIcon(string weatherType) {
            Dictionary<string, string> iconData = new Dictionary<string, string>() {
                {"NA", ""},
                {"-1", "fa-solid fa-cloud-rain"},
                {"0", "fa-solid fa-moon"},
                {"1", "fa-solid fa-sun"}, //
                {"2", "fa-solid fa-cloud-moon"},
                {"3", "fa-solid fa-cloud-sun"},
                {"4", ""},
                {"5", "fa-solid fa-smog"},
                {"6", "fa-solid fa-smog"},
                {"7", "fa-solid fa-cloud"},
                {"8", "fa-solid fa-cloud"},
                {"9", "fa-solid fa-cloud-moon-rain"},
                {"10", "fa-solid fa-cloud-sun-rain"},
                {"11", "fa-solid fa-cloud-rain"},
                {"12", "fa-solid fa-cloud-rain"},
                {"13", "fa-solid fa-cloud-moon-rain"},
                {"14", "fa-solid fa-cloud-sun-rain"},
                {"15", "fa-solid fa-cloud-showers-heavy"},
                {"16", "fa-solid fa-icicles"},
                {"17", "fa-solid fa-icicles"},
                {"18", "fa-solid fa-icicles"},
                {"19", "fa-solid fa-icicles"},
                {"20", "fa-solid fa-icicles"},
                {"21", "fa-solid fa-icicles"},
                {"22", "fa-regular fa-snowflake"},
                {"23", "fa-regular fa-snowflake"},
                {"24", "fa-regular fa-snowflake"},
                {"25", "fa-solid fa-snowflake"},
                {"26", "fa-solid fa-snowflake"},
                {"27", "fa-solid fa-snowflake"},
                {"28", "fa-solid fa-cloud-bolt"},
                {"29", "fa-solid fa-cloud-bolt"},
                {"30", "fa-solid fa-bolt"}
            };

            if (iconData.ContainsKey(weatherType)) {
                return iconData[weatherType];
            } else {
                Console.WriteLine($"Weather type {weatherType} not found.");
                return "";
            }

        }

    }
}