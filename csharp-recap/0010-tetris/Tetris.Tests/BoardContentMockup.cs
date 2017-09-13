namespace Tetris.Tests
{
    public class BoardContentMockup : IBoardContent
    {
        public bool[,] Content { get; set; }

        public bool this[int row, int col]
        {
            get => Content[row, col];
            set => Content[row, col] = value;
        }

        public int Width => Content.GetLength(1);

        public int Height => Content.GetLength(0);
    }
}
