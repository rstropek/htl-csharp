using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;

            DrawBorders2();

            var board = new Board();
            board.NewPiece();
            while (true)
            {
                if (board.CurrentPiece == null)
                {
                    board.currentPiece = Pieces.AllPieces[new Random().Next(Pieces.AllPieces.Count)];

                    // QUIZ: What isn't optimal in the previous line of code 
                    //   in terms of performance?

                    board.currentRow = 0;
                    board.currentCol = (Board.BOARD_WIDTH - board.CurrentPiece.Pattern.GetLength(0)) / 2;
                }
                else
                {
                    if (HasReachedBottom(board.currentRow, board.CurrentCol, board.CurrentPiece.Pattern))
                    {
                        board.MergePieceIntoBoardContent(board.currentRow, board.CurrentCol, board.CurrentPiece.Pattern);
                        board.currentPiece = Pieces.AllPieces[new Random().Next(Pieces.AllPieces.Count)];
                        board.currentRow = 0;
                        board.currentCol = (Board.BOARD_WIDTH - board.CurrentPiece.Pattern.GetLength(0)) / 2;
                    }
                    else
                    {
                        DrawPiece(board.CurrentCol, board.currentRow, Board.BACKGROUND_COLOR, board.CurrentPiece.Pattern);
                        board.currentRow++;
                    }
                }

                DrawPiece(board.CurrentCol, board.currentRow, board.CurrentPiece.Color, board.CurrentPiece.Pattern);

                for (var watch = Stopwatch.StartNew(); watch.ElapsedMilliseconds < 1000;)
                {
                    if (Console.KeyAvailable)
                    {
                        var pressedKey = Console.ReadKey();
                        var previousCol = board.CurrentCol;
                        switch (pressedKey.Key)
                        {
                            case ConsoleKey.LeftArrow when board.CurrentCol > 0:
                                board.currentCol--;
                                break;
                            case ConsoleKey.RightArrow when (board.CurrentCol + board.CurrentPiece.Pattern.GetLength(1)) < Board.BOARD_WIDTH:
                                board.currentCol++;
                                break;
                        }

                        if (previousCol != board.CurrentCol)
                        {
                            DrawPiece(previousCol, board.currentRow, Board.BACKGROUND_COLOR, board.CurrentPiece.Pattern);
                            DrawPiece(board.CurrentCol, board.currentRow, board.CurrentPiece.Color, board.CurrentPiece.Pattern);
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        private static bool HasReachedBottom(int currentRow, int currentCol, bool[,] pattern)
        {
            if ((currentRow + pattern.GetLength(0)) == Board.BOARD_HEIGHT)
            {
                return true;
            }

            return !IsSpaceAvailable(currentRow + 1, currentCol, pattern);
        }

        private static bool IsSpaceAvailable(int currentRow, int currentCol, bool[,] pattern)
        {
            //for (var row = 0; row < pattern.GetLength(0); row++)
            //{
            //    for (var col = 0; col < pattern.GetLength(1); col++)
            //    {
            //        if (pattern[row, col] && BoardContent[currentRow + row, currentCol + col])
            //        {
            //            return false;
            //        }
            //    }
            //}

            return true;
        }

        private static void DrawBorders()
        {
            var oldBackgroundColor = Console.BackgroundColor;
            try
            {
                // Move cursor to upper left corner
                Console.SetCursorPosition(Board.BORDER_LEFT, Board.BORDER_TOP);
                while (Console.CursorTop < Board.BORDER_TOP + Board.BOARD_HEIGHT + 1)
                {
                    // Draw left border line
                    Console.BackgroundColor = Board.BORDER_COLOR;
                    Console.Write(' ');

                    if (Console.CursorTop != Board.BORDER_TOP + Board.BOARD_HEIGHT)
                    {
                        // We have not reached the last line with the bottom border line yet.
                        // Therefore we need to use the background color or the board.
                        Console.BackgroundColor = Board.BACKGROUND_COLOR;
                    }

                    // Draw board background or (in the last line) bottom border
                    Console.Write(" ".PadLeft(Board.BOARD_WIDTH)); // QUIZ: What does PadLef do?

                    // Draw right border line
                    Console.BackgroundColor = Board.BORDER_COLOR;
                    Console.Write(' ');

                    // Move to next line
                    Console.SetCursorPosition(Board.BORDER_LEFT, Console.CursorTop + 1);
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

        private static void DrawBorders2()
        {
            RestoreOriginalState(() =>
            {
                Console.SetCursorPosition(Board.BORDER_LEFT, Board.BORDER_TOP);
                while (Console.CursorTop < Board.BORDER_TOP + Board.BOARD_HEIGHT + 1)
                {
                    Console.BackgroundColor = Board.BORDER_COLOR;
                    Console.Write(' ');

                    if (Console.CursorTop != Board.BORDER_TOP + Board.BOARD_HEIGHT)
                    {
                        Console.BackgroundColor = Board.BACKGROUND_COLOR;
                    }

                    Console.Write(" ".PadLeft(Board.BOARD_WIDTH));

                    Console.BackgroundColor = Board.BORDER_COLOR;
                    Console.Write(' ');

                    Console.SetCursorPosition(Board.BORDER_LEFT, Console.CursorTop + 1);
                }
            });
        }

        private static void RestoreOriginalState(Action drawingFunc)
        {
            // QUIZ: What does 'Action' mean? Discuss 'Func', too.

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

        private static void DrawPiece(int left, int top, ConsoleColor color, bool[,] pattern)
        {
            RestoreOriginalState(() =>
            {
                Console.BackgroundColor = color;
                for (var row = 0; row < pattern.GetLength(0); row++)
                {
                    Console.SetCursorPosition(Board.BORDER_LEFT + 1 + left, Board.BORDER_TOP + top + row);

                    // QUIZ: What isn't optimal in the previous line of code 
                    //   in terms of performance?

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
