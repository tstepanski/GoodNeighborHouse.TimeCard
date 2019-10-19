# Volunteer Time Card System
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
    * Has access to using "Administration mode"
* Modes
    * Administration Mode
        * Volunteer CRUD operations
        * Punch reconciliation
        * Department CRUD operations
        * Organization CRUD operations
        * Data export
        * Reporting
    * Kiosk Mode
        * Punch  in/out system for volunteers
 * Volunteer
    * Represents an individual providing services to Good Neighbor House needing tracked
    * Has access to punch in/out in "Kiosk mode"
* Department
    * Represents a division within Good Neighbor House requiring reporting of hours worked by volunteers
* Organization
    * Represents an external entity providing 1 or more volunteers requiring reporting of hours worked by said volunteers
* Punch reconciliation
    * Provides mechanism for verifying correctness of punches made by volunteers
    * Offers functionality to add/delete punches to fix history
    * Will automatically detect issues such as an uneven number of in/out punches and exceptionally long/short clocked-in durations