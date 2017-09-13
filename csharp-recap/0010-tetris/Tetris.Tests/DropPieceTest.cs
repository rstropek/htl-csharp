using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class DropPieceTest
    {
        [TestMethod]
        public void TestDroppingPieceEmptyBoard()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false }, { false } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();
            board.DropPiece();

            Assert.AreEqual(1, board.CurrentRow);
        }

        [TestMethod]
        public void TestDroppingPieceNonEmptyBoard()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false }, { false }, { true } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();
            board.DropPiece();

            Assert.AreEqual(1, board.CurrentRow);
        }
    }
}
