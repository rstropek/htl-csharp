using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class NewPieceTest
    {
        [TestMethod]
        public void TestSuccessfulNewPiece()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false, false, false }, { false, false, false } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();

            Assert.IsNotNull(board.CurrentPiece);
            Assert.IsNotNull(board.NextPiece);
            Assert.AreEqual(1, board.CurrentCol);
            Assert.AreEqual(0, board.CurrentRow);
        }

        [TestMethod]
        [ExpectedException(typeof(BoardException))]
        public void TestFailingNewPiece()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { true } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();
        }
    }
}
