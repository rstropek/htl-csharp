using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class MovementTest
    {
        [TestMethod]
        public void TestSuccessfulMoves()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false, false, false }, { false, false, false } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();

            Assert.IsTrue(board.IsMovePossible(Direction.Down));
            Assert.IsTrue(board.IsMovePossible(Direction.Left));
            Assert.IsTrue(board.IsMovePossible(Direction.Right));

            Assert.IsTrue(board.TryMove(Direction.Down));
            Assert.AreEqual(board.CurrentRow, 1);

            Assert.IsTrue(board.TryMove(Direction.Left));
            Assert.AreEqual(board.CurrentCol, 0);

            Assert.IsTrue(board.TryMove(Direction.Right));
            Assert.AreEqual(board.CurrentRow, 1);
        }

        [TestMethod]
        public void TestFailingMoves()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { true, false, true }, { false, true, false } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();

            Assert.IsFalse(board.IsMovePossible(Direction.Down));
            Assert.IsFalse(board.IsMovePossible(Direction.Left));
            Assert.IsFalse(board.IsMovePossible(Direction.Right));
        }

        [TestMethod]
        public void TestFailingMovesOutsideBounds()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false } });
            var board = new Board(boardContent, PiecesMockup.SinglePixelGenerator);
            board.NewPiece();

            Assert.IsFalse(board.IsMovePossible(Direction.Down));
            Assert.IsFalse(board.IsMovePossible(Direction.Left));
            Assert.IsFalse(board.IsMovePossible(Direction.Right));
        }
    }
}
