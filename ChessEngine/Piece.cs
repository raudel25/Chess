namespace ChessEngine;

public enum Color
{
    White,
    Black
}

public abstract class Piece
{
    public Color Color { get; private set; }

    public abstract int Valor { get; }

    public List<(int, int)> Move(Piece?[,] table) => Moves.Move(this.Positions.Current, table);
    
    public List<(int, int)> MoveCapture(Piece?[,] table) => Moves.MoveCapture(this.Positions.Current, table);

    public Positions Positions { get; private set; }

    protected abstract Moves Moves { get; }

    protected Piece(Color color)
    {
        this.Color = color;
        this.Positions = new Positions();
    }

    public bool NotMove()
    {
        (int, int) aux = this.Positions.Current;

        foreach (var item in this.Positions)
            if (item != aux)
                return false;

        return true;
    }
}