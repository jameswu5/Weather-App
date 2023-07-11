using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace WeatherApp
{
    public class Database
    {
        private SqliteConnection connection;

        public Database() {
            this.connection = new SqliteConnection("Data Source = location.db");
            this.connection.Open();
            Console.WriteLine("Connection opened.");
            CreateDatabase();
        }

        private void CreateDatabase() {
            var command = this.connection.CreateCommand();
            command.CommandText = 
            @"
                CREATE TABLE IF NOT EXISTS location (
                    id INTEGER PRIMARY KEY,
                    name TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }

        public void InsertData(int id, string name) {
            var command = this.connection.CreateCommand();
            command.CommandText = $"INSERT INTO location VALUES ({id}, '{name}')";
            try {
                command.ExecuteNonQuery();
                Console.WriteLine($"{id}: {name} | successfully inserted into database");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** | {id}: {name} | {ex.Message}");
            }
        }

        public void InsertLocations(List<Location> locations) {
            foreach (Location location in locations) {
                InsertData(location.id, location.name);
            }
        }

        public List<Location> GetIDFromSearch(string name, int limit = 10) {
            // returns all the ids beginning with the name
            List<Location> results = new List<Location>();

            var command = this.connection.CreateCommand();
            command.CommandText = $"SELECT * FROM location WHERE name LIKE '{name}%' LIMIT {limit}";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int id = reader.GetInt32(0);
                string fullname = reader.GetString(1);
                results.Add(new Location(id, fullname));
            }
            return results;
        }

        public Location GetIDFromName(string name) {
            var command = this.connection.CreateCommand();
            command.CommandText = $"SELECT * FROM location WHERE name == '{name}' COLLATE NOCASE";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int id = reader.GetInt32(0);
                string fullname = reader.GetString(1);
                return new Location(id, fullname);
            }
            return null;
        }

        public Location GetLocationFromID(int id) {
            var command = this.connection.CreateCommand();
            command.CommandText = $"SELECT * FROM location WHERE id == '{id}'";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int locationID = reader.GetInt32(0);
                string fullname = reader.GetString(1);
                return new Location(locationID, fullname);
            }
            return null;
        }
    }
}