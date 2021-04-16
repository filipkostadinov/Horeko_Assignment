# Horeko_Assignment
Horeko Assignment

Tools, Technologies, Libraries, Packages

- .NET Core 3.1
- Newtonsoft.Json 13.0.1
- Nunit test 
- Serilog 2.10.0
- Swashbuckle.AspNetCore Version="6.1.2" (Swagger)
- Dependency injection
- N-tier architecture

Instructions: 
1. Open Visual Studio 
2. Open  Horeko_Assignment.sln
Swagger -  /swagger/index.html

Configurations and setting the project – 3 hours

DataLoader.cs
- It uses Newtonsoft.Json to Serialize and Deserialize the objects.
- Reads and Writes all the text from “dishes-sample-data.json” and “ingredients-sample-data.json” using System.IO.File

GET /dishes
Get all dishes with all the related information. Enabled filters: Dish name and Last Modified On. Both
filters can be applied simultaneously.

-	Time: 2 hours
-	It presents all the dishes with all of the ingredients, but without the ingredients from their parent dishes.
-	Filters: dish name and Last modified date
-	To be more clearly it filters only by day not by hours,minutes,seconds

GET / dishes /{id}
Get a single dish by dish id.
If a dish does not exist return Http error 409 with custom error message “Dish with id: {dishId} is not
found”.
If a dish exists beside the dish properties return List of ingredients that are included into that dish.
-	Time: 2 hours
-	It presents the dish with its all ingredients including the ingredients from its parent dishes . 

POST /dish
Create a new dish.
If a dish exists return Http error 409 with custom error message “Dish with name: {dishName} already
exists”.
-	Time: 2 hours
-	Newly created dish will be append to the dishes-sample-data.json file
-	Method for generate an uniqe ID - this is just for testing purposes, in practice it should use GUID(aka UUID) which are stored in database.
 
GET dishes/prices
For each dish calculate how much does it cost based on the ingredients it contains and their price.
Return list of dishes with prices.
-	Time: 4 hours
-	It calculates the price for every dish including its parent dishes

GET ingredients/usage
Returns list of ingredients and information how much of these ingredients are used in dishes:.
-	Time: 2 hours
-	It calculates the amount of every ingredient that is used in dishes including the option that some of the dishes have a parent dish.
-	It calculates how many dishes use the ingredient.

NUnit tests 
-	 2 hours
