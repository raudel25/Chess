using ChessLogic;

namespace ChessGame;

public class HumanPlayer : IStrategy
{
    public Play Move(Color color, Table table)
    {
        List<Play> plays = ChessMoves.PossibleMoves(color, table);

        Play? move = null;

        while (move is null)
        {
            Console.Write("Movimient: ");
            string? s = Console.ReadLine();

            if (s is null) continue;

            (int, int) currentPos = (s[1] - '1', 'h'-s[0]);
            (int, int) movePos = (s[3] - '1','h'- s[2]);

            move = Correct(currentPos, movePos, plays);
        }

        return move;
    }

    private Play? Correct((int, int) current, (int, int) move, List<Play> plays)
    {
        foreach (var item in plays)
            if (item.PositionCurrent == current && item.PositionMove == move)
                return item;

        return null;
    }
}