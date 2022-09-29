namespace ChessEngine;

public class Tower : Piece
{
    public override int Valor => 5;

    protected override Moves Moves { get; }

    public Tower(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {1, 3, 5, 7},color);
    }
}