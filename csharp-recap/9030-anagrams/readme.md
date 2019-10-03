# Anagram Checker

## Introduction

You have to write an [anagram](https://en.wikipedia.org/wiki/Anagram) checker that supports the operations listed below. Make the functionality available through two user interfaces:

1. Command-line tool
1. Web API

**Note** that both user interfaces mentioned below must use the same logic implemented in a [C# class library](https://docs.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio).

If you struggle with technical requirements, make sure to visit the links in the requirements below. They lead to helpful chapters in the C# documentation.

## Check Two Words

### Console App

Write a console app that can check whether two words are anagrams (i.e. one word is formed by rearranging the letters of the other word using all the original letters exactly once). The words to check are given in [command line arguments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/main-and-command-args/command-line-arguments).

The command `AnagramChecker check listen silent` must lead to the *STDOUT* result `"listen" and "silent" are anagrams`

The command `AnagramChecker check talk jump` must lead to `"talk" and "jump" are no anagrams`.

### Web API

The following request has to return the HTTP status code *OK* because *listen* and *silent* are anagrams.

```http
GET http://localhost:5000/api/checkAnagram
Content-Type: application/json

{
    "w1": "listen",
    "w2": "silent"
}
```

The following request has to return the HTTP status code *BadRequest* because *talk* and *jump* are no anagrams.

```http
GET http://localhost:5000/api/checkAnagram
Content-Type: application/json

{
    "w1": "talk",
    "w2": "jump"
}
```

## Find Anagrams

### Console App

Write a console app that can get all *known* anagrams from a dictionary (details regarding dictionary see below).

The command `AnagramChecker getKnown silent` must lead to the following *STDOUT* result:

```txt
listen
inlets
```

If no known anagrams exist, print *No known anagrams found* on *STDOUT*.

### Web API

A user can send a word and the app has to return all *known* anagrams.

```http
GET http://localhost:5000/api/getKnownAnagrams?w=silent
```

This HTTP request returns the HTTP status code *OK* and a result like the following in the HTTP body:

```json
["listen", "inlets"]
```

If no known anagrams exist, return the HTTP status code *NotFound*.

### Dictionary

Your app should [read known anagrams from a text file](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file) that you have to add to your app. You are free to chose the format of the text file (e.g. JSON, CSV).

Your app has to read the text file at runtime. **Make the name of the text file a configuration setting**. Do not hard-code the text file's name in your code.

Here are some sample anagrams that you can use for testing:

* tar = rat
* arc = car
* elbow = below
* state = taste
* cider = cried
* dusty = study
* night = thing
* inch = chin
* brag = grab
* cat = act
* bored = robed
* save = vase
* angel = glean
* stressed = desserts
* silent = listen
* silent = inlets

## Encapsulation

Make sure your code fulfills the following non-functional requirements.

### Logic in Separate Classes

The logic (anagram check, reading dictionary from file) must **not** be part of the ASP.NET Controller. It has to be encapsulated in its own class in a separate class library. Use a [C# interface](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface) in combination with [*dependency injection*](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection) to get an instance of the anagram checker in the ASP.NET controller.

### Logging for Web API

If the users asks for known anagrams for a word for which no anagrams could be found, write a proper log message using [ASP.NET Core logging](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/).

## Extra Points

If you fulfill all the requirements above, you can earn up to two extra points by solving the following requirements. If you think you earned extra points, let me know by adding a GitHub issue.

### Serilog with File Sink

Earn an extra point if you configure your app to use [*Serilog*](https://serilog.net/) with its [*File* sink](https://github.com/serilog/serilog-sinks-file).

### Get Permutations

A user can ask for *all* anagrams of a given word, independent of whether they make sense (=generate all possible *permutations*).

```http
GET http://localhost:5000/api/getPermutations?w=abc
```

This HTTP request returns the HTTP status code *OK* and the following result in the HTTP body:

```json
["abc", "acb", "bac", "bca", "cba", "cab"]
```

Tip: Consider using the [*Heap* algorithm](https://en.wikipedia.org/wiki/Heap's_algorithm).
