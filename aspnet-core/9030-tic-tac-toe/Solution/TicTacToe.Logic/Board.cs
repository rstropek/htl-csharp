namespace TicTacToe.Logic
{
    public class Board : IBoard
    {
        public string GetWinner(string[] board)
        {
            for (var row = 0; row < 3; row++)
            {
                if (!string.IsNullOrWhiteSpace(board[row * 3])
                    && board[row * 3] == board[row * 3 + 1] 
                    && board[row * 3] == board[row * 3 + 2])
                {
                    return board[row * 3];
                }
            }

            for (var column = 0; column < 3; column++)
            {
                if (!string.IsNullOrWhiteSpace(board[column])
                    && board[column] == board[3 + column] 
                    && board[column] == board[2 * 3 + column])
                {
                    return board[column];
                }
            }

            if (!string.IsNullOrWhiteSpace(board[0])
                && board[0] == board[3 + 1] 
                && board[0] == board[2 * 3 + 2])
            {
                return board[0];
            }

            if (!string.IsNullOrWhiteSpace(board[2])
                && board[2] == board[3 + 1] 
                && board[2] == board[2 * 3])
            {
                return board[2];
            }

            return null;
        }
    }
}
