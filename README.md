# WebApi-Task
WebApi(.NET CORE) connection with SQL

Project setup:
- Create Web API (.NET Core 3.1).

Basic functionality
- Create Database (code first approach)

  Create database and name it “WeatherInfo” using “code first” approach and “entity framework core”. Database should contain two tables named “Country” and “City” (for this assignment, assume that each city belongs only to one country).
    Table “Country” should contain following attributes:
      - Id (int)
      - Name (string length 50)
    Table “City” should contain following attributes:
      - Id (int)
      - CountryId
      - Name (string length 100)
      
-Web API
  Web API should contain following methods:
  1) “CreateCountries”, which will accept list of countries and will add them to the database. Be cautious not to insert any duplicates in the database. If country is once added       it should not be added again. Method should return only countries which were newly added in the database and not the ones that were already in the database.
  2) “CreateCity”, which will accept city name and country name. Be cautious not to insert any duplicates in the database. If the city is once added in the database it should not      be added again. Method should return only cities which were newly added in the database and not the ones that were already in the database.
  3) “ListCities”, which will return all cities in the database.

-Use Swagger to Test the API!
