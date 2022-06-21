Technology: 
-ASP.NET Core 3.1 WebApi

Features:  
-Clean Architecture
-CQRS with MediatR Library
-Entity Framework Core - Code First
-Swagger UI
-Microsoft Identity with JWT Authentication
-API Versioning


Nuget package used : 
-FluentValidation
-Swashbuckle.AspNetCore.Swagger
-AutoMapper
-MediatR


 
Step 1: 
In appsettings.json, please change database credentials : 

"DefaultConnection": "Data Source=DESKTOP-VBHP93R\\SQLEXPRESS;Initial Catalog=CoreDb;User Id=sa;Password=12345;Integrated Security=True;MultipleActiveResultSets=True",
    "IdentityConnection": "Data Source=DESKTOP-VBHP93R\\SQLEXPRESS;Initial Catalog=CoreIdentityDb;Integrated Security=True;MultipleActiveResultSets=True"


Path :  …\CleanArchitecture.WebApi-master\WebApi\appsettings.json




 
Step 2: 
Default Project in Package manager console : Infrastructure.Persistence

 add-migration product -Context ApplicationDbContext
 Update-Database -Context  ApplicationDbContext
 
 
Default Project in Package manager console : Infrastructure.Identity
 add-migration product -Context IdentityContext
 Update-Database -Context IdentityContext



Step 3 : 
Application can be tested in debug mode through MS Visual Studio 2019
https://localhost:5001/swagger/index.html

Or
Can be run through exe file.
Path: ….\CleanArchitecture.WebApi-master\WebApi\bin\Debug\netcoreapp3.1\WebApi.exe


To test Product APIs will need to Authenticate using 
https://localhost:5001/api/Account/authenticate

superadmin@gmail.com
123Pa$$word!
Generated JWT token can be used to Authorize to call Product APIs.



~~


Unit test cases: 
3 test cases (CreateProduct, GetProductbyId, GetProductbyId_Notfound) are done which can be verified by checking through Test explorer.

