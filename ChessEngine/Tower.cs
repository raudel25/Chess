namespace ChessEngine;

public class Tower : Piece
{
    public override int Valor => 5;

    public override List<(int, int)> Move(Piece[,] table) => TableMoves.Move(this, table, new[] {1, 3, 5, 7});

    public Tower(Color color) : base(color)
    {
    }
}