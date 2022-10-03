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
    /// Determina si la ficha no se ha movido
    /// </summary>
    public bool NotMove { get; internal set; }

    /// <summary>
    /// Determina la posicion actual de la pieza
    /// </summary>
    public (int, int) Current { get; internal set; }

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

    /// <summary>
    /// Movimiento de la pieza
    /// </summary>
    protected abstract Moves Moves { get; }

    protected Piece(Color color)
    {
        this.Color = color;
        this.NotMove = true;
    }

    /// <summary>
    /// Devuelve una nueva instancia del objeto
    /// </summary>
    /// <returns>Nueva instancia del objeto</returns>
    public abstract Piece Clone();
}