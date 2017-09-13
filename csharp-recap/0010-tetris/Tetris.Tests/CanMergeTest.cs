using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class CanMergeTest
    {
        // QUIZ: What does `nameof` do?
        // LEARN MORE at https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/nameof
        [TestMethod]
        [TestCategory(nameof(Board.CanMergePatternIntoBoardContent))]
        public void TestSuccessfulCanMerge()
        {
            var boardContent = new BoardContentMockup { Content = new bool[,] { { false } } };
            Assert.IsTrue(new Board(boardContent).CanMergePatternIntoBoardContent(0, 0, PiecesMockup.SinglePixel));
        }

        [TestMethod]
        [TestCategory(nameof(Board.CanMergePatternIntoBoardContent))]
        public void TestFailingCanMerge()
        {
            var boardContent = new BoardContentMockup { Content = PiecesMockup.SinglePixel };
            Assert.IsFalse(new Board(boardContent).CanMergePatternIntoBoardContent(0, 0, PiecesMockup.SinglePixel));
        }
    }
}
