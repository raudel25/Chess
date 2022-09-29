namespace ChessEngine;

public class Moves
{
    private readonly int[] _valid;

    private readonly bool _oneMove;

    protected readonly Color _color;

    public static readonly (int, int)[] Direction =
    {
        //Direction
        (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1),
        //Horse Direction
        (-1, -2), (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2)
    };

    public Moves(int[] valid, Color color, bool oneMove = false)
    {
        this._valid = valid;
        this._oneMove = oneMove;
        this._color = color;
    }

    public List<(int, int)> Move((int, int) positionCurrent, Piece?[,] table)
    {
        List<(int, int)> possible = new List<(int, int)>();

        foreach (var item in this._valid)
        {
            (int, int) position = SumPosition(positionCurrent, Direction[item]);


            bool stop = false;
            while (DecideMove(table, position, possible, item) && !stop)
            {
                position = SumPosition(position, Direction[item]);
                stop = this._oneMove;
            }
        }

        return possible;
    }

    private bool DecideMove(Piece?[,] table, (int, int) position, List<(int, int)> possible,int direction)
    {
        if (!CorrectMove(position)) return false;

        if (table[position.Item1, position.Item2] is null)
        {
            possible.Add(position);

            return true;
        }

        if (table[position.Item1, position.Item2]!.Color == this._color) return false;

        return DecideToCapture(table, position, possible, direction);
    }

    protected virtual bool DecideToCapture(Piece?[,] table, (int, int) position, List<(int, int)> possible, int direction)
    {
        if (table[position.Item1, position.Item2]!.Color != this._color)
        {
            possible.Add(position);
            
            return true;
        }

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