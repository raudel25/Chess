namespace ChessLogic;

public class Bishop : Piece
{
    public override int Valor => 3;

    protected override Moves Moves { get; }

    public Bishop(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {0, 2, 4, 6},new[] {0, 2, 4, 6},color);
    }
}