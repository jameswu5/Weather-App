namespace WeatherApp
{
    [Serializable]
    public class Location
    {
        public int id;
        public string name;

        public Location(int id, string name) {
            this.id = id;
            this.name = name;
        }
    }
}