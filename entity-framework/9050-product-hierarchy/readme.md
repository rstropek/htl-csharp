# Product Hierarchy Quiz

## Introduction

You are working in a production company. You job is to write a *.NET Core* program with *Entity Framework Core* to calculate the total costs of a product that your company produces.

## Database

### Structure

The database structure is defined as follows:

```sql
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductNumber] [nvarchar](15) NOT NULL,
	[Manufacturer] [nvarchar](75) NOT NULL,
	[UnitPrice] [decimal](10, 2) NULL,
    PRIMARY KEY CLUSTERED ( [ID] ASC )
)

CREATE TABLE [dbo].[Rebate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[MinQuantity] [int] NOT NULL,
	[RebatePerc] [decimal](10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ( [ID] ASC )
)
ALTER TABLE [dbo].[Rebate] WITH CHECK ADD CONSTRAINT [FK_Rebate_Product]
    FOREIGN KEY ( [ProductID] ) REFERENCES [dbo].[Product] ( [ID] )
CREATE UNIQUE INDEX [IX_Rebate_ProductID] ON [dbo].[Rebate] ( [ProductID] )

CREATE TABLE [dbo].[ProductHierarchy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentProductID] [int] NOT NULL,
	[ChildProductID] [int] NOT NULL,
	[Amount] [int] NOT NULL,
    PRIMARY KEY CLUSTERED ( [ID] ASC )
)
ALTER TABLE [dbo].[ProductHierarchy] WITH CHECK ADD CONSTRAINT [FK_ChildProduct]
    FOREIGN KEY( [ChildProductID] ) REFERENCES [dbo].[Product] ( [ID] )
ALTER TABLE [dbo].[ProductHierarchy] WITH CHECK ADD CONSTRAINT [FK_ParentProduct]
    FOREIGN KEY( [ParentProductID] ) REFERENCES [dbo].[Product] ( [ID] )
```

* `Product` contains master data per product including the products *unit price*
  * Note that products with `ID >= 902` are *assembly groups* that consist of multiple other products. They do not have a unit price on their own.
  * Products with `ID <= 901` are end products that do not cosist of other products. They do have a unit price.

* `Rebate` contains rebates that we get from suppliers if we order at least `MinQuantity` units
  * Note that `RebatePerc` contains the rebate in percentage (e.g. `0.10` means 10%)

* `ProductHierarchy` contains the product hierarchy. `Amount` number of units of the child product are necessary to produce one unit of the parent product.

### Demo Data

You can access a SQL Database in Azure with demo data. Here is the connection information:

| Setting                     | Value
|-----------------------------|-------------------------------------
| Name of DB Server           | `producthierarchy.database.windows.net`
| Name of Database            | `ProductHierarchy`
| User                        | `reader`
| Password                    | Ask your teacher
| Encrypted connection        | `true`

## Challenge

### Calculation Logic

Form group of 2-5 people and write a *.NET Core* program with *Entity Framework Core* to calculate the total costs of producing the end product with `ID = 902`. Don't forget to take possible rebates into account!

### User Interface

Create a WPF application that asks the user for a product ID. After having entered it, it performs the calculation logic and displays a table with the following content:

* The table contains one and exactly one line per product that is part of the end product. Assembly groups (i.e. products that consist of other products) should not be part of the result table.

* Table columns:
  * Product number
  * Total amount of units necessary
  * Total price (amount * unit price) without rebate
  * Total price after possible rebates have been subtracted

### Web API

Access the database directly from WPF if you want to keep it simple. If you want additional practice, create the calculation logic in the form of a Web API and access this Web API from WPF.

## Solution

[*solution.sql*](solution.sql) contains a query that contains the calculation logic. You can use its result to verify whether your solution is correct.

[*ProductHierarchy*](https://github.com/rstropek/htl-csharp/tree/master/entity-framework/9050-product-hierarchy/ProductHierarchy) contains a sample solution. However, try to find your own solution before checking this reference implementation.