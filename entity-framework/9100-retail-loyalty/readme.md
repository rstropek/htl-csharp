# Exam

## Introduction

You are working in a retail company. From time to time it happens that customers demand to be deleted from the company's loyalty program because of privacy reasons.

## General Requirements

* Use .NET Core 3.x

* Use Entity Framework Core

## Part 1 - Create Database (8 points)

* Write a console app that creates a database with the following tables:

| Table Name  |                        Description                        |
| ----------- | --------------------------------------------------------- |
| `Stores`    | Retail stores                                             |
| `Customers` | Customers of the retail store chain                       |
| `Invoices`  | Invoices for which customers have received loyalty points |
| `Points`    | Point balances of customers                               |

* The tables must have the following columns:

| Table Name  |  Column Name   |                               Restrictions                                |
| ----------- | -------------- | ------------------------------------------------------------------------- |
| `Stores`    | `ID`           | Numeric, auto-generated primary key                                       |
|             | `Location`     | *Mandatory* alphanumeric location, maximum length 250 characters          |
|             | `IsHomeStore`  | *Mandatory* flag indicating whether store is home store for any customers |
| `Customers` | `ID`           | Numeric, auto-generated primary key                                       |
|             | `HomeStoreID`  | Foreign key to store where the customer first registered                  |
|             | `FirstName`    | *Mandatory* first name, maximum length 100 characters                     |
|             | `LastName`     | *Mandatory* first name, maximum length 150 characters                     |
|             | `EmailAddress` | *Mandatory, unique* email address, maximum length 150 characters          |
| `Invoices`  | `ID`           | Numeric, auto-generated primary key                                       |
|             | `InvoiceDate`  | *Mandatory* date and time of invoice                                      |
|             | `StoreID`      | Foreign key to store where the invoice was issued                         |
|             | `CustomerID`   | Foreign key to the customer                                               |
|             | `Amount`       | *Mandatory* invoice amount in Euro                                        |
|             | `Points`       | *Optional* number of points received for this invoice                     |
| `Points`    | `ID`           | Numeric, auto-generated primary key                                       |
|             | `BalanceDate`  | *Mandatory* date and time of the balance record                           |
|             | `CustomerID`   | Foreign key to the customer                                               |
|             | `Points`       | *Mandatory* number of points the customer has                             |

* You have to create tables according to the schema described above in a local *SQL Server Database* on your computer.

* You have to create the tables in a schema with the name of your GitHub user (e.g. `rstropek` for Mr. Rainer Stropek). See [documentation](https://docs.microsoft.com/en-us/ef/core/modeling/relational/default-schema) for how to specify the schema name in Entity Framework Core.

## Part 2 - Delete Customer (12 points)

* Write a console app that deletes a customer.

* The console app gets the customer's ID as a parameter

* The console app has to exit with a non-zero result code (return value of `main`) if the specified customer ID does not exist.

* The delete algorithm has to work as follows:
  * All invoices for the customer have to be deleted.
  * All point balances of the customer have to be deleted.
  * The customer record with the given customer ID has to be deleted.
  * The flag `Stores.IsHomeStore` has to be unset if there are no more customers having the corresponding store as their home store.

* The entire delete algorithm has to either complete successfully or undo all changes (i.e. use a transaction).
