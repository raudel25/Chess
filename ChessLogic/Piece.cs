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
    public virtual List<(int, int)> Move(Table table) => Moves.Move(this.Positions.Current, table);

    /// <summary>
    /// Determina el movimineto de captura de la pieza
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> MoveCapture(Table table) => Moves.MoveCapture(this.Positions.Current, table);

    /// <summary>
    /// Lista de posiciones del tablero que ha ocupado la pieza
    /// </summary>
    public Positions Positions { get; private set; }
    
    public int Convert { get; }

    /// <summary>
    /// Movimiento de la pieza
    /// </summary>
    protected abstract Moves Moves { get; }

    protected Piece(Color color)
    {
        this.Convert = -1;
        this.Color = color;
        this.Positions = new Positions();
    }
    protected Piece(Color color,Positions positions)
    {
        this.Convert = positions.Count;
        this.Color = color;
        this.Positions = positions;
    }

    public bool NotMove(int ind = 0)
    {
        (int, int) aux = this.Positions[0];

        foreach (var item in this.Positions.Take(this.Positions.Count - ind))
            if (item != aux)
                return false;

        return true;
    }

    public abstract Piece Clone();
}