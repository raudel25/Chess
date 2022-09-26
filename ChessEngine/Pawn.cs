namespace ChessEngine;

public class Pawn : Piece
{
    public override int Valor => 1;

    public override List<(int, int)> Move(Piece[,] table)
    {
        throw new NotImplementedException();
    }

    public Pawn(Color color) : base(color)
    {
    }
}