namespace ChessLogic;

public class Knight : Piece
{
    public override int Valor => 3;

    protected override Moves Moves { get; }

    internal Knight(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {8, 9, 10, 11, 12, 13, 14, 15},new[] {8, 9, 10, 11, 12, 13, 14, 15},color, true);
    }
}