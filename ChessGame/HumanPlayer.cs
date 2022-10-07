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
            Console.Write("Actual: ");
            string? s = Console.ReadLine();

            if (s is null) continue;

            string[] s1 = s.Split();

            (int, int) currentPos = (int.Parse(s1[0]), int.Parse(s1[1]));

            Console.Write("Movimiento: ");
            s = Console.ReadLine();

            if (s is null) continue;

            s1 = s.Split();

            (int, int) movePos = (int.Parse(s1[0]), int.Parse(s1[1]));

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