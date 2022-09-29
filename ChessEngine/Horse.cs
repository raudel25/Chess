namespace ChessEngine;

public class Horse : Piece
{
    public override int Valor => 3;

    protected override Moves Moves { get; }

    public Horse(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {8, 9, 10, 11, 12, 13, 14, 15},color, true);
    }
}