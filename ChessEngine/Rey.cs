namespace ChessEngine;

public class Rey : Piece
{
    public override int Valor
    {
        get => 10;
    }

    public override List<(int, int)> Move(Piece[,] table)
    {
        throw new NotImplementedException();
    }

    public Rey(Color color) : base(color)
    {
    }
}