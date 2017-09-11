using System.Collections.Generic;

namespace Tetris
{
    public static class BoardContentIteratorExtension
    {
        // QUIZ: What is `IEnumerable`?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=netcore-2.0#Remarks

        // QUIZ: What are 'generic types' in C#?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/

        // QUIZ: What does `this` mean used for the parameter `pattern`?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods

        // QUIZ: Note the 'strange' return type. How is this `(int row, int col, bool val)` construct called?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/tuples

        public static IEnumerable<(int row, int col, bool val)> ToEnumerable(this bool[,] pattern)
        {
            for (var row = 0; row < pattern.GetLength(0); row++)
            {
                for (var col = 0; col < pattern.GetLength(1); col++)
                {
                    // QUIZ: Note the 'iterator' here. What does `yield` do?
                    // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators
                    yield return (row, col, pattern[row, col]);
                }
            }
        }
    }
}
