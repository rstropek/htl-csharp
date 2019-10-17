# CSharp Exam - Currency Converter

## Introduction

Your job is to write a program with which prices of products can be converted into different currencies.

Given are two CSV (comma-separated values) files:

* One contains exchange rates.
* The other one contains products and their prices in a given currency.

Users can run your program to get the product price of a given product. If necessary, your program has to convert the product's price based on the exchange rate table.

## CSV Files

### General Information

* Line ending: CRLF
* Ignore the first line (column headers)
* Ignore empty lines
* You can use a NuGet package for CSV parsing, but I would *strongly recommend* to not do that.

### Exchange Rate Table

You can find a sample exchange rate table in the file [*ExchangeRates.csv*](ExchangeRates.csv). It contains exchange rates against EUR. Here is its content:

```txt
Currency,Rate
USD,1.10
CHF,1.10
GBP,0.86
TRL,6.50
```

The exchange rate e.g. *USD,1.10* means that you get 1.10 USD for one EUR, *GBP,0.86* means that you get 0.86 GBP for one EUR, etc.

### Product Prices

You can find a sample exchange rate table in the file [*Prices.csv*](Prices.csv). Here is its content:

```txt
Description,Currency,Price
Car,USD,30000
Bike,CHF,800
Book,TRL,20
Chocolate,EUR,3.5
```

The column *Price* contains the price in the currency contained in column *Currency*.

## Requirements

* Use *.NET Core 3* and C#.

* You do *not* have to implement error handling. You can assume that the CSV files exist and contain meaningful values. You can assume that meaningful command line arguments are given. If not, it is ok if your app crashes with an unhandled exception.

* (4 points) Put the business logic (parsing of the CSV files, currency conversion) in a reusable *.NET Standard 2.1* class library. The class library *must not* contain any input or output (e.g. reading files, writing to console).

* (6 points) Read the CSV files into appropriate in-memory data structures. It is up to you to decide which data structures make most sense.
  * **Tip:** For exchange rates and monetary values, use the C# data type `decimal`
  * **Tip:** The CSV files contain English number format. You can parse it using `decimal.Parse(numberAsString, CultureInfo.InvariantCulture)`

* (2 points) Implement those part of the CSV parsing logic that is common for both CSV files only once. Avoid duplicating this logic.

* (1 point) All numbers written to the console should be *rounded* to 2 decimal places (e.g. 27272,73). It is part of the exam to find out how to round decimal values when printing them.

* (4 points) Your app receives the name of a product and a target currency as command line arguments (e.g. *Car EUR*). The target currency is always *EUR* (earn extra points if you support other target currencies, too; see next requirement for details). Examples:
  * *CurrencyConverter.exe Car EUR* leads to *27272,73*
  * *CurrencyConverter.exe Book EUR* leads to *3,08*
  * *CurrencyConverter.exe Chocolate EUR* leads to *3,50*

* (3 points) Your app can convert prices into any target currency (e.g. *Car GBP*). If the target currency is not EUR, you have to internally calculate from the original currency into EUR, and then from EUR to the target currency. Examples:
  * *CurrencyConverter.exe Car GBP* leads to *23454,55*
  * *CurrencyConverter.exe Bike USD* leads to *800,00*
