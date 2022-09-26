namespace ChessEngine;

public class Queen : Piece
{
    public override List<(int, int)> Move(Piece?[,] table) =>
        TableMoves.Move(this, table, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8});

    public override int Valor
    {
        get => 9;
    }

    public Queen(Color color) : base(color)
    {
        
    }
}