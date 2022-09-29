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

    public Positions Positions { get; private set; }

    protected abstract Moves Moves { get; }

    protected Piece(Color color)
    {
        this.Color = color;
        this.Positions = new Positions();
    }
}