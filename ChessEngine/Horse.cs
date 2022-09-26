namespace ChessEngine;

public class Horse : Piece
{
    public override int Valor => 3;

    public override List<(int, int)> Move(Piece[,] table)
    {
        throw new NotImplementedException();
    }

    public Horse(Color color) : base(color)
    {
    }
}