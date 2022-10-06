using ChessLogic;

namespace ChessGame;

public class RandomPLayer : IStrategy
{
    public Play Move(Color color, Table table)
    {
        List<Play> plays = ChessMoves.PossibleMoves(color, table);

        Random rnd = new Random();
        
        return plays[rnd.Next(plays.Count)];
    }
}