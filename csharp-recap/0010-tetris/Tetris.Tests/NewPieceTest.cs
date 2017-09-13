using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class NewPieceTest
    {
        [TestMethod]
        [TestCategory(nameof(Board.NewPiece))]
        public void TestSuccessfulNewPiece()
        {
            var boardContent = new BoardContentMockup { Content = new bool[,] { { false, false, false }, { false, false, false } } };
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.SinglePixel));
            board.NewPiece();

            Assert.IsNotNull(board.CurrentPiece);
            Assert.IsNotNull(board.NextPiece);
            Assert.AreEqual(1, board.CurrentCol);
            Assert.AreEqual(0, board.CurrentRow);
        }

        [TestMethod]
        [TestCategory(nameof(Board.NewPiece))]
        [ExpectedException(typeof(BoardException))]
        public void TestFailingNewPiece()
        {
            var boardContent = new BoardContentMockup { Content = PiecesMockup.SinglePixel };
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.SinglePixel));
            board.NewPiece();
        }
    }
}
