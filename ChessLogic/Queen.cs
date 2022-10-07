namespace ChessLogic;

public class Queen : Piece
{
    public override int Valor => 9;

    protected override Moves Moves { get; }

    internal Queen(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {0, 1, 2, 3, 4, 5, 6, 7}, new[] {0, 1, 2, 3, 4, 5, 6, 7}, color);
    }

    public override Piece Clone() => new Queen(this.Color);

    public override string ToString() => "Q" + (Color == Color.White ? "W" : "B");
}