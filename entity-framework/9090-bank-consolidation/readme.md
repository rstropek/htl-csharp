# Exam

## Introduction

You are working in a bank. This bank is planning a reorganization. The goal is to consolidate existing subsidiaries. In this process, all employees and customers of one subsidiaries are *moved* to another subsidiary.

## General Requirements

* Use .NET Core 3.x

* Use Entity Framework Core

## Part 1 - Create Database (8 points)

* Write a console app that creates a database with the following tables:

|     Table Name      |          Description           |
| ------------------- | ------------------------------ |
| `Subsidiaries`      | Bank subsidiaries              |
| `Employees`         | Employees of subsidiaries      |
| `Customers`         | Bank customers                 |
| `ConsolidationLogs` | Log of consolidation processes |

* The tables must have the following columns:

|     Table Name      |       Column Name        |                               Restrictions                                |
| ------------------- | ------------------------ | ------------------------------------------------------------------------- |
| `Subsidiaries`      | `ID`                     | Numeric, auto-generated primary key                                       |
|                     | `Description`            | *Mandatory* alphanumeric description, maximum length 250 characters       |
|                     | `ManagerID`              | Employee ID of the manager of the subsidiary                              |
| `Employees`         | `ID`                     | Numeric, auto-generated primary key                                       |
|                     | `SubsidiaryID`           | Foreign key to subsidiary where employee works                            |
|                     | `FirstName`              | *Mandatory* first name, maximum length 100 characters                     |
|                     | `LastName`               | *Mandatory* first name, maximum length 150 characters                     |
| `Customers`         | `ID`                     | Numeric, auto-generated primary key                                       |
|                     | `CustomerName`           | *Mandatory* customer name (person's name of company name), max. 150 chars |
|                     | `SubsidiaryID`           | Foreign key to subsidiary where employee works                            |
| `ConsolidationLogs` | `ID`                     | Numeric, auto-generated primary key                                       |
|                     | `ConsolidationDate`      | *Mandatory* date and time of the consolidation run                        |
|                     | `NumberOfCustomersMoved` | *Mandatory* number of customer records moved from source to target        |
|                     | `NumberOfEmployeesMoved` | *Mandatory* number of employee records moved from source to target        |

* You have to create tables according to the schema described above in a local *SQL Server Database* on your computer.

* You have to create the tables in a schema with the name of your GitHub user (e.g. `rstropek` for Mr. Rainer Stropek). See [documentation](https://docs.microsoft.com/en-us/ef/core/modeling/relational/default-schema) for how to specify the schema name in Entity Framework Core.

## Part 2 - Consolidation (12 points)

* Write a console app that consolidates two subsidiaries.

* The console app gets two parameters:
  * ID of source subsidiary
  * ID of target subsidiary

* The console app has to exit with a non-zero result code (return value of `main`) if a specified subsidiary ID does not exist.

* The consolidation algorithm must be:
  * All customers of the source subsidiary must be *moved* to the target subsidiary.
  * All employees of the source subsidiary must be *moved* to the target subsidiary.
  * The source subsidiary must be deleted.
  * One record with appropriate log data must be written to `ConsolidationLog`.

* The entire consolidation has to either complete successfully or undo all changes (i.e. use a transaction).
