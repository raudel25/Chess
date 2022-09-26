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

    public abstract List<(int, int)> Move(Piece[,] table);

    public Positions Positions { get; private set; }

    public Piece(Color color)
    {
        this.Color = color;
        this.Positions = new Positions();
    }
}