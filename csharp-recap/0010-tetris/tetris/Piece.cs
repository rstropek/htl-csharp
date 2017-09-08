using System;

namespace Tetris
{
    class Piece
    {
        // QUIZ: Is this call immutable? Discuss what 'immutable' means.

        public Piece(ConsoleColor color, bool[,] pattern)
        {
            this.Color = color;
            this.Pattern = pattern;
        }

        public ConsoleColor Color { get; }
        public bool[,] Pattern { get; }
    }

    // QUIZ: Would it make sense to make Piece a struct instead of a class? Discuss the
    //       differences between value types and reference types.
    //   1. What would be the difference?
    //   1. What would be more efficient?
    //   1. Would Piece.Pattern be a value or reference type?
}
