# Bike Rental Exercise

## Note

In this exercise, you will create a Web API that we will use during upcoming exercises. Therefore, it is **important** that everybody completes this exercise correctly. Additionally, this exercise is **very relevant** as an exercise for your upcoming *Matura*.

You can earn **up to two extra points** for your grade:

* You get one point if you send me an internet link to the RESTful Web API
* You get one point if you manage to fulfill all requirements in this specification

## Introduction

You are a developer in a company that wants to provide **bike rental services**. You are responsible for the system's backend consisting of...

* ...the database,
* ...the data access layer and
* ...a RESTful Web API that will be used by different front-ends (e.g. website, mobile app, desktop app).

Below you find the business requirements for the system. Note that the requirements do not include technical implementation details (e.g. technical data model, URLs, HTTP response codes, etc.). It is your job to **design these details** according the the business requirements mentioned below.

## Functional Business Requirements

### Data Structure

#### Identifiers

Each of the entities below has to have a numeric *ID* column containing a unique, mandatory identification number that is set automatically. This ID cannot be changed for existing data records.

#### Customers

The system has to store **customers**. For each *Customer*, you have to store the following properties:

1. Gender of the customer - possible values are:
    * Male
    * Female
    * Unknown
1. First name - mandatory, max. 50 characters
1. Last name - mandatory, max. 75 characters
1. Birthday - mandatory date, no time
1. Street - mandatory, max. 75 characters
1. House number - optional, max 10 characters
1. Zip code - mandatory, max. 10 characters
1. Town - mandatory, max. 75 characters

Because of data protection regulations, it must be possible to delete a customer. If a customer is deleted, all data referencing this customer - in particular her bike rentals - have to be deleted, too.

#### Bikes

The system has to store **bikes**. For each *Bike*, you have to store the following properties:

1. Brand - mandatory, max. 25 characters
1. Purchase date - mandatory date, no time
1. Notes - optional, max. 1000 characters
1. Date of last service - optional date, no time
1. Rental price in Euro for first hour - mandatory number, two decimal places, value >= 0.00
1. Rental price in Euro for each additional hour - mandatory number, two decimal places, value >= 1.00
1. **Bike category** - possible bike categories are:
    * Standard bike
    * Mountainbike
    * Trecking bike
    * Racing bike

Details regarding rental price and cost calculation follow later in this document.

It must not be possible to delete a bike if one or more rentals exist for it.

#### Rentals

The system has to store **rentals** of bikes by customers. The *Rentals* entity contains the following properties:

1. Mandatory reference to the customer who rented the bike
1. Mandatory reference to the rented bike
1. Rental begin - mandatory date and time
1. Rental end - optional date and time, must be > rental begin
1. Total costs in Euro - total rental costs, two decimal places, value >= 0.00
1. Paid flag - boolean flag indicating whether the rental has already been paid by the customer, can only be *true* if the rental has already ended (i.e. rental end and total costs are set)

The system has to **calculate the total costs** automatically as soon as the end time of the rental is known. The costs are calculated based on rental time and the rental price for the rented bike. Details regarding cost calculation follow later in this document.

A rental that has been started (i.e. rental begin is set) but has not been ended (i.e. rental end and total costs are not set) is called an *active rental*. Rental that have been ended are called *ended rentals*. At any given point in time, each customer must only have **a single active rental**.

### Cost Calculation

When a rental is ended, the total costs have to be calculated automatically. The following rules apply:

* If the rental was <= 15 minutes, it is free
* A full hour is charged, even if the rental did not take the entire hour

Here are examples for cost calculations:

| Property                        | Value
|---------------------------------|-------------------------
| Rental begin                    | 14th Feb. 2018, 08:15am
| Rental end                      | 14th Feb. 2018, 10:30am
| Bike price first hour           | 3 Euro
| Bike price each additional hour | 5 Euro
| Total costs                     | 3 Euro for first hour (08:15am - 09:15am)<br/>5 Euro for additional hour (09:15am - 10:15am)<br/>5 Euro for additional hour (10:15am - 10:30am)<br/>= 13 Euro in total

| Property                        | Value
|---------------------------------|-------------------------
| Rental begin                    | 14th Feb. 2018, 08:15am
| Rental end                      | 14th Feb. 2018, 08:45am
| Bike price first hour           | 3 Euro
| Total costs                     | 3 Euro for first hour

| Property                        | Value
|---------------------------------|-------------------------
| Rental begin                    | 14th Feb. 2018, 08:15am
| Rental end                      | 14th Feb. 2018, 08:25am
| Total costs                     | Free because <= 15 minutes


### RESTful Web API

The system has to offer a RESTful Web API with the operations defined in this chapter.

Note that the business rules stated above have to be checked in the RESTful Web API implementation.

#### Customers

1. Get all customers
    * A caller can optionally specify a filter value for the last name
1. Create a new customer
1. Update a customer
1. Delete a customer
1. Get all rentals for a specific customer

#### Bikes

1. Get all bikes that are currently available (i.e. not in an active rent)
    * A caller can optionally specify whether the list should be sorted by price of first hour (ascending), price of additional hours (ascending), or purchase date (descending)
1. Create a bike
2. Update a bike
3. Delete a bike

#### Rentals

1. Start a rental
    * Rental begin has to be set automatically based on the system time
    * Rental end is empty
    * Total costs are empty
1. End an existing, active rental
    * Rental end has to be set automatically based on the system time
    * Total costs are calculated automatically
1. Mark an ended rental as paid
    * Can only be executed on rentals that have a total price > 0
1. Get a list of unpaid, ended rentals with total price > 0. For each rental, the following data must be returned:
    * Customer's ID, first and last name
    * Rental's ID, start end, end date, and total price

## Non-Functional Requirements

1. There has to be a *Swagger* and/or *Open API* document describing the RESTful Web API*
1. The code has to be well documented and formatted
1. The RESTful Web API has to follow established best practices for designing RESTful Web APIs
1. Tests should be automated. There have to be unit tests for at least the price finding logic.

*) You do not need to create the document by hand. You can use the *Swashbuckle for ASP.NET Core* NuGet package instead.

## Technical Specification

1. Use *SQL Server* to store data persistently
1. Use *C#* as your programming language
1. Use *ASP.NET Core* as the framework for implementing the RESTful Web API
1. Use *Entity Framework Core* to access the database
1. Write unit tests with *.NET Core* (*xUnit* or any other unit testing framework of your choice)
1. Use *GitHub* as your source control system
