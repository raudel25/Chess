namespace ChessEngine;

public class Rey : Piece
{
    public override int Valor => 10;

    public override List<(int, int)> Move(Piece[,] table)
    {
        throw new NotImplementedException();
    }

    public Rey(Color color) : base(color)
    {
    }
}