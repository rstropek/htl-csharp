# Entity Framework (EF)

O/RM in .NET Core


<!-- .slide: class="left" -->
## What is EF?

* Object-relational mapper (O/RM)
* Supports many different DB providers ([list](https://docs.microsoft.com/en-us/ef/core/providers/index))<br/>
  examples:
  * MS SQL Server
  * SQLite
  * PostgreSQL
  * MySQL
  * In-Memory (for testing)
* Latest version: *EF Core 2.0*
* NuGet (example): [*Microsoft.EntityFrameworkCore.SqlServer*](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)


<!-- .slide: class="left" -->
## Getting Started

* Follow [*installing* docs](https://docs.microsoft.com/en-us/ef/core/get-started/install/index) to add EF to project
* Work through [tutorials](https://docs.microsoft.com/en-us/ef/core/get-started/) in EF docs
* Tips:
  * Install and use [SQL Server 2017 Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQL Server 2017 Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-2016-express-localdb)
  * Use [In-Memory DB](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/) for simple test scenarios


<!-- .slide: class="left" -->
## Building a Model

```
<!--#include file="entity-framework/0010-intro/Person.cs" -->
```

* Read more about [creating a Model](https://docs.microsoft.com/en-us/ef/core/modeling/)


<!-- .slide: class="left" -->
## Setting up the Context

```
<!--#include file="entity-framework/0010-intro/Context.cs" -->
```


<!-- .slide: class="left" -->
## Writing Data

```
<!--#include file="entity-framework/0010-intro/WriteToDB.cs" -->
```

* Read more about [writing data](https://docs.microsoft.com/en-us/ef/core/saving/)


<!-- .slide: class="left" -->
## Writing Data to Related Sets

```
<!--#include file="entity-framework/0030-joins/AddingWithJoin.cs" -->
```

* Note *single* call to `Add` and `SaveChangesAsync`


<!-- .slide: class="left" -->
## Querying Data

```
<!--#include file="entity-framework/0010-intro/ReadFromDB.cs" -->
```

* Read more about [querying data](https://docs.microsoft.com/en-us/ef/core/querying/)


<!-- .slide: class="left" -->
## Querying Data

```
<!--#include file="entity-framework/0030-joins/QueryWithJoin.cs" -->
```

* `Include`: Getting `Order` with related `Customer`


<!-- .slide: class="left" -->
## Learn by Example

* [*Address Book* example on GitHub](https://github.com/rstropek/htl-csharp/tree/master/entity-framework/0020-web-api)
* `AddJsonOptions` in [Startup.cs](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/0020-web-api/Startup.cs) for handling loops in JSON serialization
* `services.AddDbContext` in [Startup.cs](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/0020-web-api/Startup.cs) for adding EF context to ASP.NET Core *Dependency Injection*
* [AutoMapper](http://automapper.org/) in [Startup.cs](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/0020-web-api/Startup.cs)
* [Migrations](https://github.com/rstropek/htl-csharp/tree/master/entity-framework/0020-web-api/Migrations)
  * [Read more about migrations...](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
* Visual Studio [*Scaffolding*](https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db#create-a-controller) for controllers
* EF's `Include` in [GroupsController.cs](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/0020-web-api/Controllers/GroupsController.cs)


<!-- .slide: class="left" -->
## EF Core + ASP.NET Core Cheat Sheet

[Entity Framework Cheat Sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md)


<!-- .slide: class="left" -->
## Further Readings and Exercises

* Readings
  * [Entity Framework Documentation](https://docs.microsoft.com/en-us/ef/#pivot=efcore)
  * [EF Tutorial with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro)
* Videos
  * Want to know more details? Watch [Entity Framework Core](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Entity-Framework-Core) on *Channel 9*
* Exercises
  * [*Cash Desk* Quiz](https://github.com/rstropek/htl-csharp/tree/master/entity-framework/9010-cash-desk)
