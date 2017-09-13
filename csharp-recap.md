# C# Recap

Let's remember C# fundamentals


<!-- .slide: class="left" -->
## Learn by Example

<img src="images/tetris.png" alt="Tetris Sample" width="75%" />

[Sample Code](https://github.com/rstropek/htl-csharp/tree/master/csharp-recap/0010-tetris)


<!-- .slide: class="left" -->
## Learn by Example (cont.)

* Learn about C# without too much theory
  * C# language features
  * Design patterns
  * Writing tests
* Learn about Visual Studio in a practical example
  * Code navigation
  * Building
  * Debugging
  * Unit testing


<!-- .slide: class="left" -->
## Create .NET Console App

* Create Console App in Visual Studio
* Create Console App in command line
  * Windows
  * Linux


<!-- .slide: class="left" -->
## Draw Tetris Border and Usage

[*Program.cs*](https://github.com/rstropek/htl-csharp/blob/master/csharp-recap/0010-tetris/tetris/Program.cs)

* [Constants](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const)
* Working with [`Console`](https://docs.microsoft.com/en-us/dotnet/api/system.console?view=netcore-2.0)
* Functional programmming with delegates


<!-- .slide: class="left" -->
# Tetris Piece

[*Piece.cs*](https://github.com/rstropek/htl-csharp/blob/master/csharp-recap/0010-tetris/tetris/Piece.cs)

* C# XML documentation
* Immutable classes
* Classes vs. structures (aka reference vs. value types)
* Disassembling


<!-- .slide: class="left" -->
## [Tetris Pieces](https://en.wikipedia.org/wiki/Tetris#Colors_of_Tetromino)

[*Pieces.cs*](https://github.com/rstropek/htl-csharp/blob/master/csharp-recap/0010-tetris/tetris/Pieces.cs)

* [Static classes](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members)
* [Multidimensional arrays](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays) and [collection initialization](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#collection-initializers)
* [`readonly`](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly)


<!-- .slide: class="left" -->
## Board Content

[*BoardContent.cs*](https://github.com/rstropek/htl-csharp/blob/master/csharp-recap/0010-tetris/tetris/BoardContent.cs)

* [Arrays](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/#array-overview)
* Indexer
* [Expression-bodied members](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members)


<!-- .slide: class="left" -->
## Board

[*Board.cs*](https://github.com/rstropek/htl-csharp/blob/master/csharp-recap/0010-tetris/tetris/Board.cs)
[*BoardException*](https://github.com/rstropek/htl-csharp/blob/master/csharp-recap/0010-tetris/tetris/BoardException.cs)

* [Auto-implemented properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties)
* [Optional arguments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#optional-arguments)
* Performance discussions
* [Custom exceptions](https://docs.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions) and [`Try...` pattern](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/exceptions-and-performance#try-parse-pattern)
* Enumerations
* [Delegates](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)


<!-- .slide: class="left" -->
## Board Content Iterator

[*BoardContentIteratorExtension.cs*](https://github.com/rstropek/htl-csharp/blob/master/csharp-recap/0010-tetris/tetris/BoardContentIteratorExtension.cs)

* [Generic types](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/)
* [Enumerables](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=netcore-2.0#Remarks)
* Linq basics
* [Extension methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
* [Tuples](https://docs.microsoft.com/en-us/dotnet/csharp/tuples)


<!-- .slide: class="left" -->
## Unit Testing

[*Tetris.Tests*](https://github.com/rstropek/htl-csharp/tree/development/csharp-recap/0010-tetris/Tetris.Tests)

* Visual Studio unit testing
* [Mock objects](https://en.wikipedia.org/wiki/Mock_object)
