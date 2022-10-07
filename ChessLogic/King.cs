namespace ChessLogic;

public class King : Piece
{
    public override int Valor => 10;

    protected override Moves Moves { get; }

    internal King(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {1, 3, 5, 7},new[] {1, 3, 5, 7}, color,true);
    }

    public override Piece Clone() => new King(this.Color);
    
    public override string ToString() => "K" + (Color == Color.White ? "W" : "B");
}