namespace ChessEngine;

public class TableMoves
{
    public static (int, int)[] Direction = {(-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1)};

    public static bool CorrectMove((int, int) move)
    {
        if (move.Item1 < 0 || move.Item2 < 0) return false;
        if (move.Item1 >= 8 || move.Item2 >= 8) return false;

        return true;
    }

    public static List<(int, int)> Move(Piece piece, Piece?[,] table, int[] valid)
    {
        List<(int, int)> possible = new List<(int, int)>();

        foreach (var item in valid)
        {
            (int, int) position = SumPosition(piece.Positions.Current, Direction[item]);

            while (DecideMove(piece, table, position, possible)) position = SumPosition(position, Direction[item]);
        }

        return possible;
    }

    private static bool DecideMove(Piece piece, Piece?[,] table, (int,int) position,List<(int,int)> possible)
    {
        if (!CorrectMove(position)) return false;

        if (table[position.Item1, position.Item2] is null)
        {
            possible.Add(position);

            return true;
        }
        
        if (table[position.Item1, position.Item2]!.Color == piece.Color) return false;

        possible.Add(position);

        return false;
    }

    public static (int, int) SumPosition((int, int) a, (int, int) b) => (a.Item1 + b.Item1, a.Item2 + b.Item2);
}