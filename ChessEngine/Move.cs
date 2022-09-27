namespace ChessEngine;

public class Moves
{
    private readonly int[] _valid;

    private readonly bool _oneMove;

    public static readonly (int, int)[] Direction =
    {
        //Direction
        (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1),
        //Horse Direction
        (-1, -2), (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2)
    };

    public Moves(int[] valid, bool oneMove = false)
    {
        this._valid = valid;
        this._oneMove = oneMove;
    }

    public List<(int, int)> Move(Piece piece, Piece?[,] table)
    {
        List<(int, int)> possible = new List<(int, int)>();

        foreach (var item in this._valid)
        {
            (int, int) position = SumPosition(piece.Positions.Current, Direction[item]);


            bool stop = false;
            while (DecideMove(piece, table, position, possible) && !stop)
            {
                position = SumPosition(position, Direction[item]);
                stop = this._oneMove;
            }
        }

        return possible;
    }

    public bool DecideMove(Piece piece, Piece?[,] table, (int, int) position, List<(int, int)> possible)
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

    private static bool CorrectMove((int, int) move)
    {
        if (move.Item1 < 0 || move.Item2 < 0) return false;
        if (move.Item1 >= 8 || move.Item2 >= 8) return false;

        return true;
    }

    private static (int, int) SumPosition((int, int) a, (int, int) b) => (a.Item1 + b.Item1, a.Item2 + b.Item2);

    public static bool TreatPosition(Piece?[,] table, (int, int) position, Color color)
    {
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if (table[i, j] is not null)
                {
                    if (table[i, j]!.Color == color)
                    {
                        if (table[i, j]!.Move(table).Contains(position)) return true;
                    }
                }
            }
        }

        return false;
    }
}