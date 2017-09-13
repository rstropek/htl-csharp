using System;
using System.Collections.Generic;

namespace Tetris
{
    // QUIZ: What is a `static class`?
    // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members
    public static class Pieces
    {
        // List of pieces and colors see https://en.wikipedia.org/wiki/Tetris#Colors_of_Tetromino

        // QUIZ: Note the initialization. How is this kind of initialization called?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#collection-initializers

        // QUIZ: What is the difference between multidimensional array and 'jagged' arrays?
        // LEARN MORE at
        //   1. https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
        //   2. https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/jagged-arrays

        // QUIZ: Why doesn't C# need array sizes in the following code (e.g. `bool[,]`)?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays#array-initialization

        // QUIZ: 
        //   1. What does `readonly` mean?
        //   2. Which data types can be `const`?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly

        private static readonly bool[,] I = { { true, true, true, true } };
        private static readonly bool[,] J = { { true, true, true }, { false, false, true } };
        private static readonly bool[,] L = { { true, true, true }, { true, false, false } };
        private static readonly bool[,] O = { { true, true }, { true, true } };
        private static readonly bool[,] S = { { false, true, true }, { true, true, false } };
        private static readonly bool[,] T = { { true, true, true }, { false, true, false } };
        private static readonly bool[,] Z = { { true, true, false }, { false, true, true } };

        public static readonly IReadOnlyList<Piece> AllPieces = new Piece[] {
            new Piece(ConsoleColor.DarkRed, I),
            new Piece(ConsoleColor.Gray, J),
            new Piece(ConsoleColor.DarkYellow, L),
            new Piece(ConsoleColor.DarkBlue, O),
            new Piece(ConsoleColor.DarkGreen, S),
            new Piece(ConsoleColor.DarkMagenta, T),
            new Piece(ConsoleColor.Green, Z)
        };

        public static Piece GetRandomPiece()
        {
            var template = Pieces.AllPieces[new Random().Next(Pieces.AllPieces.Count)];

            // QUIZ: According to the docs, `Clone` creates a shallow copy.
            //   1. What does 'shallow' mean?
            //   2. What is the opposite of 'shallow' in this context?
            return new Piece(template.Color, (bool[,])template.Pattern.Clone());
        }
    }
}
