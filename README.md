#Volunteer Time Card System
>Developed for Good Neighbor House
>
>Developed as a gift from the Southwest Ohio GiveCamp
***
## Requirements
* Dot Net Core 3.0 SDK
* Microsoft SQL Server (to use local database)
## Technologies employed
* C#
* Dot Net Core
* ASP.NET
* Entity Framework Core (with Migrations)
* Microsoft SQL Server
* LDAP (forthcoming)
## Setting up the build
1. Navigate to the root of the solution
2. Run the following commands


    dotnet restore
    dotnet build  

## Concepts
* User
    * Represents an employee of Good Neighbor House
    * Required for initial "Kiosk mode" login
    * Has access to perform the following administrative tasks
        * Volunteer CRUD operations
        * Punch reconciliation
        * Department CRUD operations
        * Organization CRUD operations
        * Data export
        * Reporting