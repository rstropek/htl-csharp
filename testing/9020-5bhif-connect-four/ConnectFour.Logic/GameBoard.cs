using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectFour.Logic
{
    public class GameBoard
    {
        private readonly byte[,] board = new byte[7, 6];

        internal bool playerOne = true;

        private byte CheckForVerticalWin(byte column, byte row)
        {
            if (row < 3)
            {
                return 0;
            }

            var player = board[column, row];
            for(var i = 0; i < 3; i++)
            {
                if (board[column, row - i - 1] != player)
                {
                    return 0;
                }
            }

            return player;
        }

        private byte CheckForWin(byte column, byte row)
        {
            var winner = CheckForVerticalWin(column, row);
            return winner;
        }

        public byte SetStone(byte column)
        {
            if (column > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            for(byte row = 0; row < 6; row++)
            {
                if (board[column, row] == 0)
                {
                    board[column, row] = playerOne ? (byte)1 : (byte)2;
                    playerOne = !playerOne;
                    return CheckForWin(column, row);
                }
            }

            throw new InvalidOperationException("Column is full");
        }
    }
}
