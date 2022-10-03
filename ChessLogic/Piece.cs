namespace ChessLogic;

public enum Color
{
    White,
    Black
}

public abstract class Piece
{
    /// <summary>
    /// Color de la pieza
    /// </summary>
    public Color Color { get; private set; }

    /// <summary>
    /// Valor de la pieza
    /// </summary>
    public abstract int Valor { get; }

    /// <summary>
    /// Determina el movimiento de la pieza
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public virtual List<(int, int)> Move(Table table) => Moves.Move(this.Current, table);

    /// <summary>
    /// Determina el movimineto de captura de la pieza
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> MoveCapture(Table table) => Moves.MoveCapture(this.Current, table);

    // /// <summary>
    // /// Lista de posiciones del tablero que ha ocupado la pieza
    // /// </summary>
    // public Positions Positions { get; private set; }
    public bool NotMove { get; internal set; }

    public (int, int) Current { get; internal set; }

    /// <summary>
    /// Movimiento de la pieza
    /// </summary>
    protected abstract Moves Moves { get; }

    protected Piece(Color color)
    {
        this.Color = color;
        this.NotMove = true;
    }

    public abstract Piece Clone();
}