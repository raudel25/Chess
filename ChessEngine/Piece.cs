namespace ChessEngine;

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
    public List<(int, int)> Move(Table table) => Moves.Move(this.Positions.Current, table);

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

    /// <summary>
    /// Movimiento de la pieza
    /// </summary>
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