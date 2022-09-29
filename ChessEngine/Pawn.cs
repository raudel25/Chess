namespace ChessEngine;

public class Pawn : Piece
{
    public override int Valor => 1;

    protected override Moves Moves { get; }

    public Pawn(Color color) : base(color)
    {
        int[] aux = color == Color.White ? new[] {6, 5, 4} : new[] {0, 1, 2};
        this.Moves = new MovePawn(aux, color);
    }
}