namespace ChessLogic;

public class Queen : Piece
{
    public override int Valor => 9;

    protected override Moves Moves { get; }

    public Queen(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8},new[] {0, 1, 2, 3, 4, 5, 6, 7, 8},color);
    }
}