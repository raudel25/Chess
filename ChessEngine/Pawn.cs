namespace ChessEngine;

public class Pawn : Piece
{
    public override int Valor => 1;

    protected override Moves Moves { get; }

    public Pawn(Color color) : base(color)
    {
        int[] aux = color == Color.White ? new[] {5} : new[] {1};
        int[] auxCapture = color == Color.White ? new[] {6, 4} : new[] {0, 2};
        this.Moves = new Moves(aux, auxCapture, color, true);
    }
}