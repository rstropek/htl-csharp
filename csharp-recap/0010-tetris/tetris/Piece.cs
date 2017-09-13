using System;

namespace Tetris
{
    /// <summary>
    /// Represents a single piece in a tetris game
    /// </summary>
    public class Piece
    {
        // QUIZ: Note the structure of the following comments section.
        //   1. How is this kind of documentation in C# called?
        //   2. Why is documenting code in this way useful?

        /// <summary>
        /// Initializes a new instance of the <see cref="Piece"/> class
        /// </summary>
        /// <param name="color">Color of the piece</param>
        /// <param name="pattern">Pattern of the piece (see remarks for details)</param>
        /// <remarks>
        /// An array element set to <c>true</c> in parameter <paramref name="pattern"/> represents
        /// a colored pixel. <c>false</c> represents an empty pixel.
        /// </remarks>
        public Piece(ConsoleColor color, bool[,] pattern)
        {
            Color = color;
            Pattern = pattern;
        }

        // QUIZ: Note that there is no setter. 
        //   Why can we still set the value of `Color` in the constructor?
        public ConsoleColor Color { get; }
        public bool[,] Pattern { get; }

        public Piece GetRotatedPiece(RotationDirection direction)
        {
            // During rotation, width becomes height and height becomes width
            var rotatedPattern = new bool[Width, Height];
            foreach(var pixel in Pattern.ToEnumerable())
            {
                // QUIZ: What isn't optimal in terms of performance with this `if`?
                if (direction == RotationDirection.Clockwise)
                {
                    rotatedPattern[pixel.col, rotatedPattern.GetLength(1) - 1 - pixel.row] = pixel.val;
                }
                else
                {
                    rotatedPattern[rotatedPattern.GetLength(0) - 1 - pixel.col, pixel.row] = pixel.val;
                }
            }

            return new Piece(Color, rotatedPattern);
        }

        public int Width { get => Pattern.GetLength(1); }

        public int Height { get => Pattern.GetLength(0); }
    }

    // QUIZ:
    //  1. Is this class immutable? Why/why not?
    //  2. Discuss what 'immutable' means.
    //  3. If the class is no immutable, how could we make it one?
}

// QUIZ:
//   1. Would it make sense to make Piece a struct instead of a class?
//      Discuss the differences between value types and reference types.
//   2. What would be the difference?
//   3. What would be more efficient in terms of memory?
//   4. Is `Piece.Pattern` a value or reference type?
