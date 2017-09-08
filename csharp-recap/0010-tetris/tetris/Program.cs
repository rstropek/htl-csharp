using System;
using System.Collections.Generic;

namespace Tetris
{
    class Program
    {
        private static int BOARD_WIDTH = 30;
        private static int BOARD_HEIGHT = 25;
        private static int BORDER_LEFT = 5;
        private static int BORDER_TOP = 2;
        private static ConsoleColor BORDER_COLOR = ConsoleColor.Yellow;

        // List of pieces and colors see https://en.wikipedia.org/wiki/Tetris#Colors_of_Tetromino
        private static IReadOnlyList<Piece> PIECES = new Piece[] {
            new Piece(ConsoleColor.DarkRed, new [,] { { true, true, true, true } }),
            new Piece(ConsoleColor.Gray, new [,] { { true, true, true }, { false, false, true } }),
            new Piece(ConsoleColor.DarkYellow, new [,] { { true, true, true }, { true, false, false } }),
            new Piece(ConsoleColor.DarkBlue,    new [,] { { true, true }, { true, true } }),
            new Piece(ConsoleColor.DarkGreen, new [,] { { false, true, true }, { true, true, false } }),
            new Piece(ConsoleColor.DarkMagenta, new [,] { { true, true, true }, { false, true, false } }),
            new Piece(ConsoleColor.Green, new [,] { { true, true, false }, { false, true, true } })
        };

        // QUIZ: 
        //   1. Why doesn't C# need a type in the code above (new [,]...)?
        //   1. Why doesn't C# need array sizes in the code above (new Piece[]...)?

        static void Main(string[] args)
        {
            Console.Clear();
            DrawBorders();

            Piece currentPiece = null, previousPiece = null;
            int currentRow = 0, currentCol = 0, previousRow = 0, previousCol = 0;
            while (true)
            {
                if (currentPiece == null)
                {
                    previousPiece = null;
                    currentPiece = PIECES[new Random().Next(PIECES.Count)];

                    // QUIZ: What isn't optimal in the previous line of code 
                    //   in terms of performance?

                    // Note multiple assignments in a single line of code
                    previousRow = previousCol = -1;
                    currentRow = 0;
                    currentCol = (BOARD_WIDTH - currentPiece.Pattern.GetLength(0)) / 2;
                }

                if (previousPiece != null)
                {
                }
            }

            Console.ReadKey();
        }



        private static void DrawBorders()
        {
            var oldBackgroundColor = Console.BackgroundColor;
            try
            {
                Console.BackgroundColor = BORDER_COLOR;
                Console.SetCursorPosition(BORDER_LEFT, BORDER_TOP);
                Console.CursorVisible = false;

                // Draw left border line
                for (; Console.CursorTop < BORDER_TOP + BOARD_HEIGHT; Console.CursorTop++)
                {
                    Console.Write(' ');
                    Console.CursorLeft--;
                }

                // QUIZ: What is faster:
                //   1. Having your own counter in the for loop
                //   2. Continuously checking Console.CursorTop
                // Find an answer using https://source.dot.net

                // QUIZ: What is faster:
                //   1. Console.CursorTop++
                //   2. Console.SetCursorPosition
                // Find an answer using dnSpy (https://github.com/0xd4d/dnSpy)

                // Now we are in the row where we have to draw the bottom line
                Console.Write(" ".PadLeft(BOARD_WIDTH + 1));

                // QUIZ: What does PadLef do?

                // Note that this time we are using our own counter variable
                for (var row = 0; row <= BOARD_HEIGHT; row++)
                {
                    Console.Write(' ');
                    Console.CursorTop--;
                    Console.CursorLeft--;
                }
            }
            finally
            {
                Console.BackgroundColor = oldBackgroundColor;

                // QUIZ: Why is it important to do cleanup like this in a finally-block?
            }
        }
    }
}
