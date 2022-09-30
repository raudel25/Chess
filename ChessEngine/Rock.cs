namespace ChessEngine;

public class Rock : Piece
{
    public override int Valor => 5;

    protected override Moves Moves { get; }

    public Rock(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {1, 3, 5, 7}, new[] {1, 3, 5, 7}, color);
    }
}