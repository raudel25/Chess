namespace ChessLogic;

public class Table
{
    /// <summary>
    /// Tablero
    /// </summary>
    protected Piece?[,] TablePieces;

    /// <summary>
    /// Lista de piezas capturadas
    /// </summary>
    private readonly List<Piece> _captured;

    /// <summary>
    /// Cantidad de filas
    /// </summary>
    public int Rows => 8;

    /// <summary>
    /// Cantidad de columnas
    /// </summary>
    public int Columns => 8;

    /// <summary>
    /// Cantidad de turnos
    /// </summary>
    public int CantTurns { get; private set; }

    /// <summary>
    /// Turno inmediato
    /// </summary>
    public Color Turn { get; private set; }

    /// <summary>
    /// Copia del tablero
    /// </summary>
    //public Piece?[,] Copy { get; private set; }

    public Piece? this[int i, int j]
    {
        get
        {
            if (i < 0 || j < 0 || i >= 8 || j >= 8) throw new IndexOutOfRangeException();
            return TablePieces[i, j];
        }
    }

    public Table()
    {
        this.TablePieces = StartPosition();
        this._captured = new List<Piece>();
        this.Turn = Color.White;
    }

    protected Table(Piece?[,] table)
    {
        this.TablePieces = table;
        this._captured = new List<Piece>();
        this.Turn = Color.White;
    }

    /// <summary>
    /// Distribuir las posiciones iniciales del tablero
    /// </summary>
    /// <returns>Posiciones iniciales</returns>
    private Piece?[,] StartPosition()
    {
        Piece?[,] table = new Piece[8, 8];

        (table[0, 0], table[0, 7]) = (new Rock(Color.White), new Rock(Color.White));
        (table[7, 0], table[7, 7]) = (new Rock(Color.Black), new Rock(Color.Black));
        (table[0, 1], table[0, 6]) = (new Knight(Color.White), new Knight(Color.White));
        (table[7, 1], table[7, 6]) = (new Knight(Color.Black), new Knight(Color.Black));
        (table[0, 2], table[0, 5]) = (new Bishop(Color.White), new Bishop(Color.White));
        (table[7, 2], table[7, 5]) = (new Bishop(Color.Black), new Bishop(Color.Black));
        (table[0, 3], table[0, 4]) = (new King(Color.White), new Queen(Color.White));
        (table[7, 3], table[7, 4]) = (new King(Color.Black), new Queen(Color.Black));

        for (int i = 0; i < 8; i++)
        {
            table[1, i] = new Pawn(Color.White);
            table[6, i] = new Pawn(Color.Black);
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (table[i, j] is not null) table[i, j]!.Positions.Add((i, j));
            }
        }

        return table;
    }

    public TableCopy Copy() => new TableCopy(TablePieces);

    /// <summary>
    /// Movimiento de captura
    /// </summary>
    /// <param name="position">Posicion</param>
    internal void Capture((int, int) position)
    {
        _captured.Add(TablePieces[position.Item1, position.Item2]!);
        TablePieces[position.Item1, position.Item2] = null;
    }

    /// <summary>
    /// Movimiento
    /// </summary>
    /// <param name="positionCurrent">Posicion actual</param>
    /// <param name="positionMove">Posicion para moverse</param>
    internal void Move((int, int) positionCurrent, (int, int) positionMove)
    {
        Turn = TablePieces[positionCurrent.Item1, positionCurrent.Item2]!.Color == Color.White ? Color.Black : Color.White;
        (TablePieces[positionCurrent.Item1, positionCurrent.Item2], TablePieces[positionMove.Item1, positionMove.Item2]) =
            (null, TablePieces[positionCurrent.Item1, positionCurrent.Item2]);
    }

    /// <summary>
    /// Movimento de convertir una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="position">Posicion</param>
    internal void Convert(Piece piece, (int, int) position) => TablePieces[position.Item1, position.Item2] = piece;

    /// <summary>
    /// Actualizar las posiciones de las piezas
    /// </summary>
    internal virtual void ActPosition()
    {
        for (int i = 0; i < TablePieces.GetLength(0); i++)
        {
            for (int j = 0; j < TablePieces.GetLength(1); j++)
                if (TablePieces[i, j] is not null)
                    TablePieces[i, j]!.Positions.Add((i, j));
        }

        CantTurns++;
    }

    /// <summary>
    /// Mostrar el historial del tablero
    /// </summary>
    /// <param name="ind">Indice del historial</param>
    /// <returns>Tablero</returns>
    public Piece?[,] HistoryTable(int ind)
    {
        Piece?[,] table = new Piece[8, 8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (TablePieces[i, j] is not null)
                {
                    var aux = TablePieces[i, j]!.Positions[ind];
                    if (TablePieces[i, j]!.Convert > ind) table[aux.Item1, aux.Item2] = new Pawn(TablePieces[i, j]!.Color);
                    else table[aux.Item1, aux.Item2] = TablePieces[i, j];
                }
            }
        }

        foreach (var item in _captured)
        {
            if (ind < item.Positions.Count)
            {
                var aux = item.Positions[ind];
                table[aux.Item1, aux.Item2] = item;
            }
        }

        return table;
    }
}