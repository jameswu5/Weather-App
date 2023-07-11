# Weather App
A simple weather web application made with C# (ASP.NET) using the Met Office API.

### Features
* Real-time weather data from Met Office in 5000 UK locations.
* Search bar with suggestions if your search cannot be found in the database.
* Intuitive interface with a minimalist design.

### How to use
Use the app by pasting just your Met Office API key to APIKey.txt. You can obtain a key by [this link](https://www.metoffice.gov.uk/services/data/datapoint/api).
Populate the database by uncommenting the three commented lines (lines 11-13) on Program.cs and typing **dotnet run** in the terminal with the current directory to this folder. If you don't wish the app to run, then comment out the new app line (line 15) on Program.cs.


To run the app, type **dotnet watch**. On MacOS devices, you may encounter an error. This can be fixed by clicking *Show in Finder* every time you encounter the error. Right click on the file and open it. You may have to do this twice. You should then be able to run the app.

### Issues
If there are issues with running this application, feel free to contact me through [email](mailto:jameswu144@gmail.com) or by [LinkedIn](https://www.linkedin.com/in/jameswu3/).

Enjoy!
