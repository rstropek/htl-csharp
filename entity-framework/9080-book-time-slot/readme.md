# Book a Time Slot

## Introduction

A fitness studio hired you to create a website that allows their customers to book a training time slot during the current Covid-19 crises. Only a limited number of concurrent visitors are allowed. Your website should manage that limitation.

## Functional Requirements

### Administrator

1. As an administrator, I must be able to maintain (create, update, delete, list) customer data (6-letter alphanumeric customer code, first name, last name, 4-digit PIN code).
1. As an administrator, I must be able to check whether a given customer if currently allowed to be in the studio (i.e. she currently is in a booked time slot).

### General Rules

1. Time slots are only possible during opening hours, which are 6am to 10pm (every day of the week).
2. Time slots are 2 hours long. Possible time slots are:
   * 6am-8am
   * 8am-10am
   * ...
   * 6pm-8pm
   * 8pm-10pm
3. Customers can only book time slots in the future.
4. Customers can only book time slots for the current calendar day and the following 2 calendar days. The can *not* book time slots further in the future.
5. Customers are not allowed to have more than 3 time slots booked for the upcoming days.
6. Time slots on the current calendar day cannot be canceled anymore.

### Customer

1. As a customer, I want to login using my customer code and PIN code.
2. As a customer, I want to book time slots during which I am allowed to come to the fitness studio.
3. As a customer, I want to be able to see all my booked time slots in the future.
4. As a customer, I want to be able to cancel a time slot.

## Non-Functional Requirements

1. Store data in SQL Server.
2. For data access, use Entity Framework Core 3.1.
3. Implement the business logic in C# and .NET Core 3.1.
4. Expose the business logic using a RESTful Web API implemented with ASP.NET Core.
5. Implement a web UI using Java

## Special Challenges

Are you looking for additional challenges to practice for your matura?

1. As an administrator, I want a report of all customers who were in the studio at the same time as another given customer during the past 48 hours. This report is important if a customer gets a positive Covid-19 test and public authorities require a list of all contacts for this person.
2. Allow storing of closed days (e.g. public holidays) for the fitness studio.
3. Allow storing exception of opening hours (e.g. on a specific day open from 6am to 12pm).
4. Allow customers to choose between 1h and 2h time slots.
