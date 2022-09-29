namespace ChessEngine;

public class MovePawn : Moves
{
    public MovePawn(int[] valid, Color color) : base(valid, color, true)
    {
    }

    protected override bool DecideToCapture(Piece?[,] table, (int, int) position, List<(int, int)> possible,
        int direction)
    {
        if (table[position.Item1, position.Item2]!.Color != this._color)
        {
            if ((direction & 1) == 1) return false;

            possible.Add(position);

            return true;
        }

        return false;
    }
}