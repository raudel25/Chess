namespace ChessEngine;

public class Pawn : Piece
{
    public override int Valor => 1;

    protected override Moves Moves { get; }

    public Pawn(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {0,1,2,4,5,6}, true);
    }
}