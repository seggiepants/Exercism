public static class Camicia
{
    static Dictionary<string, int> royalty = new(){
        ["J"] = 1,
        ["Q"] = 2, 
        ["K"] = 3,
        ["A"] = 4
    };

    public class Status
    {
        public GameStatus? GameStatus {get; set;}
        public int Cards {get; set;}
        public int Tricks {get; set;}

        public Status(GameStatus? status, int cards, int tricks)
        {
            GameStatus = status;
            Cards = cards;
            Tricks = tricks;
        }
    }

    public enum GameStatus
    {
        Finished,
        Loop
    }

    public record GameResult(GameStatus Status, int Tricks, int Cards);

    private record Cards(List<string>playerA, List<string>playerB, List<string>pile);

    public static GameResult SimulateGame(string[] playerA, string[] playerB)
    {
        Status ret = new Status(null, 0, 0);
        
        HashSet<string> moves = new HashSet<string>();
        int turn = 0; // will use 1 - turn to toggle between 0 and 1.
        int[] flag = [0, 0];

        List<string> _playerA = new(playerA);
        List<string> _playerB = new(playerB);
        List<string> _pile = new List<string>();

        moves.Add(state2String(_playerA, _playerB));
        while (ret.GameStatus == null)
        {
            string? card = nextCard(turn, _playerA, _playerB);
            flag[turn] = 1;
            if (card == null)
            {
                Cards result = nullCard(1 - turn, _playerA, _playerB, _pile, ret);
                _playerA = result.playerA;
                _playerB = result.playerB;
                _pile = result.pile;
            }
            else 
            {
                _pile.Add(card);
                ret.Cards++;
            }

            if (card != null && royalty.ContainsKey(card))
            {
                int penalty = royalty[card];
                int recipient = turn;
                turn = 1 - turn;
                while (penalty > 0)
                {
                    string? next = nextCard(turn, _playerA, _playerB);
                    flag[turn] = 1;
                    if (next == null)
                    {
                        Cards result = nullCard(recipient, _playerA, _playerB, _pile, ret);
                        _playerA = result.playerA;
                        _playerB = result.playerB;
                        _pile = result.pile;
                        break;
                    }
                    else
                    {
                        _pile.Add(next);
                        ret.Cards++;
                        penalty--;
                    }
        
                    if (royalty.ContainsKey(next))
                    {          
                        penalty = royalty[next];
                        recipient = turn;
                        turn = 1 - turn;
                    }
                }

                if (penalty == 0)
                {
                    Cards result = collectPile(recipient, _playerA, _playerB, _pile, ret);
                    _playerA = result.playerA;
                    _playerB = result.playerB;
                    _pile = result.pile;
                    turn = 1 - recipient;
                }
            }
            turn = 1 - turn;
            if (flag[0] == flag[1] && flag[0] == 1)
            {
                updateMoves(_playerA, _playerB, _pile, moves, ret);
                flag[0] = 0;
                flag[1] = 0;
            }
            isFinished(_playerA, _playerB, _pile, ret);
        }
        return new GameResult((GameStatus)ret.GameStatus, ret.Tricks, ret.Cards);
    }

    static Cards collectPile(int turn, List<string>playerA, List<string>playerB, List<string>pile, Status status)
    {
        if (turn == 0)
            playerA.AddRange(pile);
        else
            playerB.AddRange(pile);

        status.Tricks++;
        pile = new List<string>();
        return new Cards(playerA, playerB, pile);
    }

    static bool isFinished(List<string>playerA, List<string>playerB, List<string>pile, Status status)
    {
        if (status.GameStatus != null) // Don't break an already set status
            return true;

        int total = playerA.Count + playerB.Count + pile.Count ;
        if (playerA.Count == total || playerB.Count == total)
        {
            status.GameStatus = GameStatus.Finished;
            return true;
        }
        return false;
    }

    static string? nextCard(int turn, List<string>playerA, List<string>playerB)
    {
        if (turn == 0)
        { 
            if (playerA.Count == 0)
                return null;
            string next = playerA[0];
            playerA.RemoveAt(0);
            return next;
        }
        else 
        {
            if (playerB.Count == 0)
                return null;
            string next = playerB[0];
            playerB.RemoveAt(0);
            return next;
        }   
    }

    static string state2String(List<string>playerA, List<string>playerB)
    {
        return String.Join("",(from string str in playerA
        select royalty.ContainsKey(str) ? str : "N")) + "|" + 
        String.Join("", (from string str in playerB
        select royalty.ContainsKey(str) ? str : "N"));
    }

    static Cards nullCard(int turn, List<string>playerA, List<string>playerB, List<string>pile, Status status)
    {
        Cards ret = collectPile(turn, playerA, playerB, pile, status);
        playerA = ret.playerA;
        playerB = ret.playerB;
        pile = ret.pile;
        isFinished(playerA, playerB, pile, status);
        return new Cards(playerA, playerB, pile);
    }

    static void updateMoves(List<string>playerA, List<string>playerB, List<string>pile, HashSet<string> moves, Status status)
    {
        string move = state2String(playerA, playerB);
        if (moves.Contains(move))
            status.GameStatus = GameStatus.Loop;
        moves.Add(move);
    }
}
