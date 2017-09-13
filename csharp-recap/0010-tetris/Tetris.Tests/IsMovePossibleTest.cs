using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class IsMovePossibleTest
    {
        [TestMethod]
        [TestCategory(nameof(Board.IsMovePossible))]
        public void TestSuccessfulMoves()
        {
            var boardContent = new BoardContentMockup { Content = new bool[,] { { false, false, false }, { false, false, false } } };
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.SinglePixel));
            board.NewPiece();

            Assert.IsTrue(board.IsMovePossible(Direction.Down));
            Assert.IsTrue(board.IsMovePossible(Direction.Left));
            Assert.IsTrue(board.IsMovePossible(Direction.Right));
        }

        [TestMethod]
        [TestCategory(nameof(Board.IsMovePossible))]
        public void TestFailingMoves()
        {
            var boardContent = new BoardContentMockup { Content = new bool[,] { { true, false, true }, { false, true, false } } };
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.SinglePixel));
            board.NewPiece();

            Assert.IsFalse(board.IsMovePossible(Direction.Down));
            Assert.IsFalse(board.IsMovePossible(Direction.Left));
            Assert.IsFalse(board.IsMovePossible(Direction.Right));
        }

        [TestMethod]
        [TestCategory(nameof(Board.IsMovePossible))]
        public void TestFailingMovesOutsideBounds()
        {
            var boardContent = new BoardContentMockup { Content = new bool[,] { { false } } };
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.SinglePixel));
            board.NewPiece();

            Assert.IsFalse(board.IsMovePossible(Direction.Down));
            Assert.IsFalse(board.IsMovePossible(Direction.Left));
            Assert.IsFalse(board.IsMovePossible(Direction.Right));
        }
    }
}
