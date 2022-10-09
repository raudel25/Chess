using ChessLogic;

namespace ChessGame;

public class RandomPlayer : IStrategy
{
    public Play Move(Color color, Table table)
    {
        List<Play> plays = ChessMoves.PossibleMoves(color, table);

        Random rnd = new Random();

        return plays[rnd.Next(plays.Count)];
    }
}