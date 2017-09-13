namespace Tetris
{
    // QUIZ: What is an `interface`?
    // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/
    public interface IBoardContent
    {
        bool this[int row, int col] { get; set; }

        int Width { get; }

        int Height { get; }
    }
}
