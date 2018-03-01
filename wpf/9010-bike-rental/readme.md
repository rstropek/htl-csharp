# Bike Rental WPF UI

## Introduction

Your job is to create a Windows UI application using WPF technology. The application will be used by administrators of a bike shop to maintain master data and bike rentals.

Note that this UI example uses the RESTful Web API created in the [Bike Rental example created in the Entity Framework section](https://github.com/rstropek/htl-csharp/tree/master/entity-framework/9040-bike-rental).

Note that the UI details (e.g. form layout, colors, sizes, resizing behavior, etc.) are not fixed. It is your job to design the UI properly. Take some time to think about the UI layout to make sure your application is easy to use.

Everybody has to submit her or his best try via GitHub.

## Requirements

1. As an administrator, I want to have a list of customers that I can optionally filter by customers' last names.

1. As an administrator, I want to maintain the customer list by adding, updating and deleting customers. The customer form has to reflect the customer data structure described in the [RESTful Web API specification](https://github.com/rstropek/htl-csharp/tree/master/entity-framework/9040-bike-rental).

1. As an administrator, I want to have a list of available bikes that I can optionally sort by price of first hour (ascending) and/or price of additional hour (ascending), and/or purchase date (descending).

1. As an administrator, I want to maintain the bike list by adding, updating and deleting bikes. The bike form has to reflect the bike data structure described in the [RESTful Web API specification](https://github.com/rstropek/htl-csharp/tree/master/entity-framework/9040-bike-rental).

1. As an administrator, I want to see a list of unpaid, ended rentals with total price > 0 so that I can call the customers asking for payments. For each rental, the following data must be returned:
    * Customer's first and last name
    * Rental's start end, end date, and total price

## Advanced Requirements for Extra Points

Build the application using the [MVVM pattern](https://www.codeproject.com/Articles/165368/WPF-MVVM-Quick-Start-Tutorial). Create at least five meaningful unit tests that test your *ViewModel* code.

Send me a GitHub issue if you have MVVM with unit tests to get your extra points.
