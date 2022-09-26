namespace ChessEngine;

public class Bishop : Piece
{
    public override int Valor => 3;

    public override List<(int, int)> Move(Piece[,] table) => TableMoves.Move(this, table, new[] {0, 2, 4, 6});

    public Bishop(Color color) : base(color)
    {
    }
}