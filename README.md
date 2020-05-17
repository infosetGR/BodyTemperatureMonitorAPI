# BodyTemperatureMonitorAPI
Sample Rest API in .NET Core 3.1 with Swagger, JWT authentication and Entity Framework  to Monitor Patient Body Temperatures

Run on VS2019
- Open solution file
- Edit Mysql connection string in Startup.cs (A Mysql database is required)
- In Package Manager Console run update-database
- Run the project


Description
REST API following best practices using .NET Core 3.1 with a functional and documented Swagger. The system allow its users to log their body temperature, monitor their health and save their personal data.
Each user is an individual patient that needs to login with their email and password in order to use the services. The authentication can be as simple as checking the provided username and password against the relevant fields in a database table. Bonus: use authentication with Identity.
Once logged-in, patients can access endpoints by providing a JWT (JSON Web Token) in the headers of the request. The token is returned by the login process, should it be successful. 
Database MySQL and ORM EF Core. 

Features
Patients are able to add personal data (e.g. First Name, Last Name, Email, Age etc.). and also view and update them at any time.
Health is the most important feature of the project. Users (patients) of the API can provide their body temperature independently form the personal information, and check their current health. The current health is provided by the system and is either healthy or with an ongoing fever.
Once the reported body temperature increases above 37°C an ongoing fever log is created and all the next body temperature logs are included in the ongoing fever log. Ongoing fever concludes when the patient enters temperature below 37°C.
Note: Temperature is validated. Any temperature below 35°C or above 42°C is not accepted.
Patients can also view all the fever sessions they had between any two dates

 
