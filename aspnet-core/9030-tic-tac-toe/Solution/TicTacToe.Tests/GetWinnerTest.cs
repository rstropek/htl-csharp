using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Logic;

namespace TicTacToe.Tests
{
    [TestClass]
    public class GetWinnerTest
    {
        [TestMethod]
        public void BoardWithWinner()
        {
            var board = new Board();
            var winner = board.GetWinner(new[] { " ", "X", "O", "O", "X", " ", " ", "X", "O" });
            Assert.AreEqual("X", winner);
        }

        [TestMethod]
        public void BoardWithoutWinner()
        {
            var board = new Board();
            var winner = board.GetWinner(new[] { " ", "X", "O", "O", "X", " ", " ", "O", " " });
            Assert.IsNull(winner);
        }
    }
}
