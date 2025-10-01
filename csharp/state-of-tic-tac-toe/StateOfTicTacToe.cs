public enum State
{
    Win,
    Draw,
    Ongoing,
    Invalid
}

public class TicTacToe
{

    string[] board;

    public TicTacToe(string[] rows)
    {
        if (rows.Count() != 3)
            throw new InvalidDataException("Board should have three rows.");
        if (rows.Any<string>(s => s.Length != 3))
            throw new InvalidDataException("Each row on the board should have three places.");

        board = rows;
    }

    private bool isWin(string[] board, string players = "XO")
    {
        string[] winners = (from char player in players select new String(player, 3)).ToArray<string>();
        bool anyRow = board.Any<string>(s => winners.Contains(s));
        bool anyCol = false;
        for (int i = 0; i < board[0].Length; i++)
        {
            foreach (char player in players)
            {
                anyCol = anyCol || (from row in board where row[i] == player select player).Count() == board.Length;
            }
        }
        bool diagonal = false;
        foreach (char player in players)
        {
            diagonal = diagonal ||
                (board[0][0] == player && board[1][1] == player && board[2][2] == player) ||
                (board[2][0] == player && board[1][1] == player && board[0][2] == player);
        }
        return anyRow || anyCol || diagonal;
    }

    private bool isFull(string[] board)
    {
        return (from row in board from ch in row where ch != 'X' && ch != 'O' select ch).Count() == 0;
    }

    private bool isDraw(string[] board)
    {
        return isFull(board) && !isWin(board);
    }

    private bool isWrongOrder(string[] board)
    {
        int countX = (from row in board from ch in row where ch == 'X' select ch).Count();
        int countY = (from row in board from ch in row where ch == 'O' select ch).Count();

        return countX != countY && countX != (countY + 1);
    }

    private bool playedAfterWin(string[] board)
    {
        return isWin(board, "X") && isWin(board, "O");
    }
    
    public State State
    {
        get
        {
            if (playedAfterWin(board) || isWrongOrder(board))
                return State.Invalid;
            else if (isWin(board))
                return State.Win;
            else if (isDraw(board))
                return State.Draw;
            else
                return State.Ongoing;
        }
    }
}
