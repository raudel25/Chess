namespace ChessLogic;

public class Bishop : Piece
{
    public override int Valor => 3;

    protected override Moves Moves { get; }

    internal Bishop(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {0, 2, 4, 6}, new[] {0, 2, 4, 6}, color);
    }

    public override Piece Clone() => new Bishop(this.Color);
    
    public override string ToString() => "B" + (Color == Color.White ? "W" : "B");
}