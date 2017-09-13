namespace Tetris.Tests
{
    // QUIZ: What is a "mock object"?
    // LEARN MORE at https://en.wikipedia.org/wiki/Mock_object
    public class BoardContentMockup : IBoardContent
    {
        public BoardContentMockup(bool[,] content)
        {
            Content = content;
        }

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
