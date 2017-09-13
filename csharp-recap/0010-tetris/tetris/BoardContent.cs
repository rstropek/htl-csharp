namespace Tetris
{
    public class BoardContent : IBoardContent
    {
        // QUIZ: 
        //   1. Do we need to initialize the content of bool array in `BoardContent`?
        //   2. What is the content if we do not initialize the bool array?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/#array-overview

        // QUIZ: Why is `content` private? Wouldn't it be easier to make it public?
        private readonly bool[,] content;

        public BoardContent(int height, int width)
        {
            content = new bool[height, width];
        }

        // QUIZ: Note expression-bodied getter/setter here. What else can you do with expression bodies?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public bool this[int row, int col]
        {
            set => content[row, col] = value;
            get => content[row, col];
        }

        public int Width { get => content.GetLength(1); }

        public int Height { get => content.GetLength(0); }
    }
}
