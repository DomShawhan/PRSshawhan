# PRS System  

This is my PRS System capstone for the MAX Technical Training Maximum Coding Bootcamp.  

## Description  
This is a Purchase Requisition System that enables the users to place requests for necessary work items. Reviewers can view the requests once they are submitted by the user. If a request is under $50, it will be automatically approved, otherwise it will be added to the list of requests to review.

## Getting Started  
### Dependencies  
- .Net 6.0  
- Microsoft.CodeAnalysis.Razor (6.0.27)
- Microsoft.EntityFrameworkCore.SqlServer (6.0.28)
- Microsoft.EntityFrameworkCore.Tools (6.0.28)
- Microsoft.VisualStudio.Web.CodeGeneration.Design (6.0.16)
- NuGet.ProjectModel (6.9.1)
- Microsoft SQL Server

### Installing
- In SQL Server Management Studio run the file SQL/PRSCreateDatabaseAndTables.sql
- In the appsettings.json, add the SQL Server connection string to the "PRSConnection" property

### Executing Program
- Open the PRSshawhan.sln in Visual Studio
- Run the program using the green run button

## Authors
- [Dominic Shawhan](https://github.com/DomShawhan)
    - Built to the specifications of [Sean Blessing](https://github.com/sean-blessing)  

## Version History
* 0.4
    * Removed the get vendor by vendor code route and the get vendor by city state route and DTO routes as they are no longer part of specs
    * Added the vendor summary and user summary routes and DTOs
    * Refactored the code to add try/catch blocks to the context calls
    * Added the Utilites folder to the Models folder to organize the code
    * Updated the endpoints documentation(documentation/endpoints.md)
* 0.3
    * Added the DTOs and the EF folders to the Models folder to organize the code
    * Added the login route and login DTO
    * Added the get vendor by vendor code route
    * Added the get vendor by city state route and DTO
    * Updated the endpoints documentation(documentation/endpoints.md)
* 0.2
    * Added Approve Request, Reject Request, Review Request, and Get all requests to review routes
    * Added the PRSutlities class and set the status variables
    * Added the wwwroot/index.html page and enabled static files
* 0.1
    * Initial Release