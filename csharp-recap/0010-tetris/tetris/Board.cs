using System;
using System.Linq;

namespace Tetris
{
    public class Board
    {
        // QUIZ:
        //   1. What does `const` mean?
        //   2. Which data types can be `const`?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const
        public const int BOARD_WIDTH = 20;
        public const int BOARD_HEIGHT = 25;
        public const int BORDER_LEFT = 5;
        public const int BORDER_TOP = 2;
        public const ConsoleColor BORDER_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Black;

        // QUIZ: 
        //   1. Do we need to initialize the content of bool array in `BoardContent`?
        //   2. What is the content if we do not initialize the bool array?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/#array-overview
        private readonly bool[,] BoardContent = new bool[BOARD_HEIGHT, BOARD_WIDTH];

        // QUIZ: Note expression-bodied getter here. What else can you do with expression bodies?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public bool this[int row, int col] { get => this.BoardContent[row, col]; }

        // QUIZ: What does `auto-implemented property` mean?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
        public int CurrentRow { get; private set; } = 0;
        public int CurrentCol { get; private set; } = 0;
        public Piece CurrentPiece { get; private set; } = null;
        public Piece NextPiece { get; private set; } = null;

        public void NewPiece()
        {
            // QUIZ: What does `??` mean in the next line of code?
            // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-conditional-operator
            CurrentPiece = NextPiece ?? Pieces.GetRandomPiece();
            NextPiece = Pieces.GetRandomPiece();
            MergeCurrentPieceIntoBoardContent(0, (Board.BOARD_WIDTH - CurrentPiece.Pattern.GetLength(0)) / 2);
        }

        // QUIZ: How is the C# concept called that enables queries like `Where` and `Any`?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/quantifier-operations
        public bool CanMergePatternIntoBoardContent(int targetRow, int targetCol, bool[,] pattern) =>
            // Check whether any target pixel is already occupied
            !pattern.ToEnumerable()
                .Where(item => item.val)
                .Any(item => item.val && BoardContent[targetRow + item.row, targetCol + item.col]);

        // QUIZ: Why is the following method called `Try...`?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/exceptions-and-performance#try-parse-pattern
        public bool TryMergingPatternIntoBoardContent(int targetRow, int targetCol, bool[,] pattern)
        {
            if (CanMergePatternIntoBoardContent(targetRow, targetCol, pattern))
            {
                return false; // Indicate error
            }

            // Set target pixcels
            foreach (var item in pattern.ToEnumerable().Where(item => item.val))
            {
                BoardContent[targetRow + item.row, targetCol + item.col] = item.val;
            }

            return true; // Indicate success
        }

        public bool TryMergingCurrentPieceIntoBoardContent(int targetRow, int targetCol)
            => TryMergingPatternIntoBoardContent(targetRow, targetCol, CurrentPiece.Pattern);

        public void MergePatternIntoBoardContent(int targetRow, int targetCol, bool[,] pattern)
        {
            if (!TryMergingPatternIntoBoardContent(targetRow, targetCol, pattern))
            {
                throw new BoardException();
            }
        }

        public void MergeCurrentPieceIntoBoardContent(int targetRow, int targetCol)
            => MergePatternIntoBoardContent(targetRow, targetCol, CurrentPiece.Pattern);
    }
}
