namespace Tetris.Tests
{
    public static class PiecesMockup
    {
        public static readonly bool[,] SinglePixel = new[,] { { true } };
        public static readonly bool[,] LBar = new[,] { { true, true, true }, { true, false, false} };
        public static readonly bool[,] LBarClockwiseRotated = new[,] { { true, true }, { false, true }, { false, true } };
    }
}
