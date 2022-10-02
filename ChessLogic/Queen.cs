namespace ChessLogic;

public class Queen : Piece
{
    public override int Valor => 9;

    protected override Moves Moves { get; }

    internal Queen(Color color) : base(color)
    {
        this.Moves = new Moves(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8}, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8}, color);
    }

    internal Queen(Color color, Positions positions) : base(color, positions)
    {
        this.Moves = new Moves(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8}, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8}, color);
    }
    
    public override Piece Clone() => new Queen(this.Color,this.Positions.Clone());
}