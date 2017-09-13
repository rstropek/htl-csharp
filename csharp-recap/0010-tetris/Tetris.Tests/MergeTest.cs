using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class MergeTest
    {
        // QUIZ: What does `nameof` do?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/nameof
        [TestMethod]
        [TestCategory(nameof(Board.CanMergePatternIntoBoardContent))]
        public void TestSuccessfulCanMerge()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false } });
            var board = new Board(boardContent);
            Assert.IsTrue(board.CanMergePatternIntoBoardContent(0, 0, PiecesMockup.SinglePixel));

            Assert.IsTrue(board.TryMergingPatternIntoBoardContent(0, 0, PiecesMockup.SinglePixel));
            Assert.IsTrue(boardContent.Content[0, 0]);
        }

        [TestMethod]
        public void TestFailingCanMerge()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { true } });
            var board = new Board(boardContent);
            Assert.IsFalse(board.CanMergePatternIntoBoardContent(0, 0, PiecesMockup.SinglePixel));
            Assert.IsFalse(board.TryMergingPatternIntoBoardContent(0, 0, PiecesMockup.SinglePixel));
        }

        [TestMethod]
        [ExpectedException(typeof(BoardException))]
        public void TestMergeException()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { true } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();
            board.MergeCurrentPieceIntoBoardContent();
        }
    }
}
