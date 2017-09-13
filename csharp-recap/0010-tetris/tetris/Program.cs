using System;
using System.Diagnostics;

namespace Tetris
{
    class Program
    {
        // QUIZ:
        //   1. What does `const` mean?
        //   2. Which data types can be `const`?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const
        public const int BOARD_WIDTH = 25;
        public const int BOARD_HEIGHT = 20;
        public const int BORDER_LEFT = 5;
        public const int BORDER_TOP = 2;
        public const ConsoleColor BORDER_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Black;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;

            DrawBorders2();

            var board = new Board(new BoardContent(BOARD_HEIGHT, BOARD_WIDTH), Pieces.GetRandomPiece);
            board.NewPiece();
            DrawPiece(board.CurrentCol, board.CurrentRow, board.CurrentPiece.Color, board.CurrentPiece.Pattern);

            try
            {
                while (true)
                {
                    var dropped = false;
                    for (var watch = Stopwatch.StartNew(); watch.ElapsedMilliseconds < 1000;)
                    {
                        if (Console.KeyAvailable)
                        {
                            var pressedKey = Console.ReadKey();
                            var previousCol = board.CurrentCol;
                            var previousRow = board.CurrentRow;
                            var previousPattern = board.CurrentPiece.Pattern;
                            switch (pressedKey.Key)
                            {
                                // QUIZ: What does the `when` keyword do in the following line of code?
                                // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/when#when-in-a-switch-statement
                                case ConsoleKey.LeftArrow when (pressedKey.Modifiers & ConsoleModifiers.Control) != 0:
                                    board.TryRotatePiece(RotationDirection.CounterClockwise);
                                    break;
                                case ConsoleKey.RightArrow when (pressedKey.Modifiers & ConsoleModifiers.Control) != 0:
                                    board.TryRotatePiece(RotationDirection.Clockwise);
                                    break;
                                case ConsoleKey.LeftArrow:
                                    board.TryMove(Direction.Left);
                                    break;
                                case ConsoleKey.RightArrow:
                                    board.TryMove(Direction.Right);
                                    break;
                                case ConsoleKey.Spacebar when !dropped:
                                    board.DropPiece();
                                    dropped = true;
                                    break;
                            }

                            if (previousCol != board.CurrentCol || previousRow != board.CurrentRow || previousPattern != board.CurrentPiece.Pattern)
                            {
                                DrawPiece(previousCol, previousRow, BACKGROUND_COLOR, previousPattern);
                                DrawPiece(board.CurrentCol, board.CurrentRow, board.CurrentPiece.Color, board.CurrentPiece.Pattern);
                            }
                        }
                    }

                    if (!board.IsMovePossible(Direction.Down) || dropped)
                    {
                        board.MergeCurrentPieceIntoBoardContent();
                        board.NewPiece();
                    }
                    else
                    {
                        DrawPiece(board.CurrentCol, board.CurrentRow, BACKGROUND_COLOR, board.CurrentPiece.Pattern);
                        board.TryMove(Direction.Down);
                    }

                    DrawPiece(board.CurrentCol, board.CurrentRow, board.CurrentPiece.Color, board.CurrentPiece.Pattern);
                }
            }
            catch (BoardException)
            {
                Console.WriteLine("GAME OVER");
            }

            Console.ReadKey();
        }

        private static void DrawBorders()
        {
            var oldBackgroundColor = Console.BackgroundColor;
            try
            {
                // Move cursor to upper left corner
                Console.SetCursorPosition(BORDER_LEFT, BORDER_TOP);
                while (Console.CursorTop < BORDER_TOP + BOARD_HEIGHT + 1)
                {
                    // Draw left border line
                    Console.BackgroundColor = BORDER_COLOR;
                    Console.Write(' ');

                    if (Console.CursorTop != BORDER_TOP + BOARD_HEIGHT)
                    {
                        // We have not reached the last line with the bottom border line yet.
                        // Therefore we need to use the background color or the board.
                        Console.BackgroundColor = BACKGROUND_COLOR;
                    }

                    // Draw board background or (in the last line) bottom border
                    Console.Write(" ".PadLeft(BOARD_WIDTH)); // QUIZ: What does PadLef do?

                    // Draw right border line
                    Console.BackgroundColor = BORDER_COLOR;
                    Console.Write(' ');

                    // Move to next line
                    Console.SetCursorPosition(BORDER_LEFT, Console.CursorTop + 1);
                }

                // QUIZ: What is faster:
                //   1. Having your own counter in the for loop
                //   2. Continuously checking Console.CursorTop
                // Find an answer using https://source.dot.net

                // QUIZ: What is faster:
                //   1. Console.CursorTop++
                //   2. Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1)
                // Find an answer using dnSpy (https://github.com/0xd4d/dnSpy)
            }
            finally
            {
                Console.BackgroundColor = oldBackgroundColor;

                // QUIZ: Why is it important to do cleanup like this in a finally-block?
            }
        }

        // QUIZ: What does 'Action' mean? Discuss 'Func', too.
        private static void RestoreOriginalState(Action drawingFunc)
        {
            var oldBackgroundColor = Console.BackgroundColor;
            var oldTop = Console.CursorTop;
            var oldLeft = Console.CursorLeft;
            try
            {
                drawingFunc();
            }
            finally
            {
                Console.BackgroundColor = oldBackgroundColor;
                Console.CursorTop = oldTop;
                Console.CursorLeft = oldLeft;
            }
        }

        private static void DrawBorders2()
        {
            RestoreOriginalState(() =>
            {
                Console.SetCursorPosition(BORDER_LEFT, BORDER_TOP);
                while (Console.CursorTop < BORDER_TOP + BOARD_HEIGHT + 1)
                {
                    Console.BackgroundColor = BORDER_COLOR;
                    Console.Write(' ');

                    if (Console.CursorTop != BORDER_TOP + BOARD_HEIGHT)
                    {
                        Console.BackgroundColor = BACKGROUND_COLOR;
                    }

                    Console.Write(" ".PadLeft(BOARD_WIDTH));

                    Console.BackgroundColor = BORDER_COLOR;
                    Console.Write(' ');

                    Console.SetCursorPosition(BORDER_LEFT, Console.CursorTop + 1);
                }
            });
        }

        private static void DrawPiece(int left, int top, ConsoleColor color, bool[,] pattern)
        {
            RestoreOriginalState(() =>
            {
                Console.BackgroundColor = color;
                for (var row = 0; row < pattern.GetLength(0); row++)
                {
                    // QUIZ: What isn't optimal in the next line of code in terms of performance?
                    Console.SetCursorPosition(BORDER_LEFT + 1 + left, BORDER_TOP + top + row);

                    for (var col = 0; col < pattern.GetLength(1); col++)
                    {
                        if (pattern[row, col])
                        {
                            // Current pixel is set -> fill it
                            Console.Write(' ');
                        }
                        else
                        {
                            // Current pixel is unset -> jump over it
                            Console.CursorLeft++;
                        }
                    }
                }
            });
        }
    }
}
