using ConnectFour.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConnectFour.Tests
{
    public class GameBoardTests
    {
        [Fact]
        public void SetStoneInInvalidColumn()
        {
            var b = new GameBoard();

            var previousPlayer = b.playerOne;
            Assert.Throws<ArgumentOutOfRangeException>(() => b.SetStone(99));
            Assert.Equal(previousPlayer, b.playerOne);
        }

        [Fact]
        public void NextPlayerAfterSuccessfulMove()
        {
            var b = new GameBoard();

            var previousPlayer = b.playerOne;
            b.SetStone(0);
            Assert.NotEqual(previousPlayer, b.playerOne);
        }

        [Fact]
        public void NoWinnerAfterSingleMove()
        {
            var b = new GameBoard();
            var winner = b.SetStone(0);
            Assert.Equal(0, winner);
        }

        [Fact]
        public void SetStoneInFullColumn()
        {
            var b = new GameBoard();
            for(var i = 0; i < 6; i++)
            {
                b.SetStone(0);
            }

            var previousPlayer = b.playerOne;
            Assert.Throws<InvalidOperationException>(() => b.SetStone(0));
            Assert.Equal(previousPlayer, b.playerOne);
        }

        [Fact]
        public void VerticalWin()
        {
            var b = new GameBoard();
            for(var i = 0; i < 3; i++)
            {
                b.SetStone(0);
                b.SetStone(1);
            }

            var result = b.SetStone(0);
            Assert.Equal(1, result);
        }
    }
}
