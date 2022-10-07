namespace ChessLogic;

public class Rock : Piece
{
    public override int Valor => 5;

    protected override Moves Moves { get; }

    internal Rock(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {1, 3, 5, 7}, new[] {1, 3, 5, 7}, color);
    }

    public override Piece Clone() => new Rock(this.Color);

    public override string ToString() => "R" + (Color == Color.White ? "W" : "B");
}