using System;

namespace Tetris
{
    // LEARN MORE about user-defined exceptions at https://docs.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
    public class BoardException : Exception
    {
        public BoardException() { }

        // QUIZ: What does `base` mean?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/base
        public BoardException(string message) : base(message) { }

        public BoardException(string message, Exception innerException) : base(message, innerException) { }
    }
}
