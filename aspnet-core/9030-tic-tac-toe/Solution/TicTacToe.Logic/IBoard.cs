using System;

namespace TicTacToe.Logic
{
    public interface IBoard
    {
        string GetWinner(string[] board);
    }
}
