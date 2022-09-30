namespace ChessEngine;

public class Pawn : Piece
{
    public override int Valor => 1;

    protected override Moves Moves { get; }

    public Pawn(Color color) : base(color)
    {
        int[] aux = color == Color.White ? new[] {5} : new[] {1};
        int[] auxCapture = color == Color.White ? new[] {6, 4} : new[] {0, 2};
        this.Moves = new Moves(aux, auxCapture, color, true);
    }

    public override List<(int, int)> Move(Table table)
    {
        List<(int, int)> possible = base.Move(table);
    
        if (possible.Count > 0)
        {
            if (this.NotMove()) return possible.Concat(this.Moves.Move(possible[0], table)).ToList();
        }
    
        return possible;
    }
}