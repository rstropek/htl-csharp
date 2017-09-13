using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class DropPieceTest
    {
        [TestMethod]
        [TestCategory(nameof(Board.DropPiece))]
        public void TestDroppingPieceEmptyBoard()
        {
            var boardContent = new BoardContentMockup { Content = new bool[,] { { false }, { false } } };
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.SinglePixel));
            board.NewPiece();
            board.DropPiece();

            Assert.AreEqual(1, board.CurrentRow);
        }

        [TestMethod]
        [TestCategory(nameof(Board.DropPiece))]
        public void TestDroppingPieceNonEmptyBoard()
        {
            var boardContent = new BoardContentMockup { Content = new bool[,] { { false }, { false }, { true } } };
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.SinglePixel));
            board.NewPiece();
            board.DropPiece();

            Assert.AreEqual(1, board.CurrentRow);
        }
    }
}
