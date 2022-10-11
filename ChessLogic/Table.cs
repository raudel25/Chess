namespace ChessLogic;

public class Table
{
    /// <summary>
    /// Tablero
    /// </summary>
    protected Piece?[,] TablePieces;

    /// <summary>
    /// Historial de posiciones
    /// </summary>
    protected readonly List<Piece?[,]> History;

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
    public int CantTurns { get; protected set; }

    /// <summary>
    /// Turno inmediato
    /// </summary>
    public Color Turn { get; protected set; }

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
        this.History = new List<Piece?[,]>();
        this.Turn = Color.White;
        this.History.Add(BuildCopy(TablePieces,true));
    }

    protected Table(Piece?[,] table,List<Piece?[,]> history)
    {
        this.TablePieces = table;
        this.History = history;
        this.Turn = Color.White;
        this.History.Add(BuildCopy(TablePieces,true));
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
                if (table[i, j] is not null) table[i, j]!.Current = (i, j);
            }
        }

        return table;
    }

    /// <summary>
    /// Devuleve una copia del tablero
    /// </summary>
    /// <returns></returns>
    public TableCopy Copy() => new TableCopy(BuildCopy(TablePieces),History.Take(10).ToList());

    /// <summary>
    /// Movimiento de captura
    /// </summary>
    /// <param name="position">Posicion</param>
    internal void Capture((int, int) position) => TablePieces[position.Item1, position.Item2] = null;
    
    /// <summary>
    /// Movimiento
    /// </summary>
    /// <param name="positionCurrent">Posicion actual</param>
    /// <param name="positionMove">Posicion para moverse</param>
    internal void Move((int, int) positionCurrent, (int, int) positionMove)
    {
        Turn = TablePieces[positionCurrent.Item1, positionCurrent.Item2]!.Color == Color.White
            ? Color.Black
            : Color.White;

        TablePieces[positionCurrent.Item1, positionCurrent.Item2]!.NotMove = false;
            
        (TablePieces[positionCurrent.Item1, positionCurrent.Item2],
                TablePieces[positionMove.Item1, positionMove.Item2]) =
            (null, TablePieces[positionCurrent.Item1, positionCurrent.Item2]);

        TablePieces[positionMove.Item1, positionMove.Item2]!.Current = positionMove;
    }

    /// <summary>
    /// Movimento de convertir una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="position">Posicion</param>
    internal void Convert(Piece piece, (int, int) position)
    {
        TablePieces[position.Item1, position.Item2] = piece;
        TablePieces[position.Item1, position.Item2]!.Current = position;
    }

    /// <summary>
    /// Actualizar las posiciones de las piezas
    /// </summary>
    internal virtual void ActPosition()
    {
        History.Add(TableCopy.BuildCopy(TablePieces,true));

        CantTurns++;
    }
    
    /// <summary>
    /// Devuelve una copia de un tablero
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="reference">Determina si las piezas son pasadas por referencia</param>
    /// <returns>Copia del tablero</returns>
    protected static Piece?[,] BuildCopy(Piece?[,] table, bool reference = false)
    {
        Piece?[,] copy = new Piece[8, 8];

        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if (table[i, j] is not null)
                {
                    if (reference) copy[i, j] = table[i, j];
                    else
                    {
                        copy[i, j] = table[i, j]!.Clone();
                        copy[i, j]!.Current = (i, j);
                        copy[i, j]!.NotMove = table[i, j]!.NotMove;
                    }
                }
            }
        }

        return copy;
    }

    /// <summary>
    /// Determina si dos tableros son iguales
    /// </summary>
    /// <param name="a">Tablero a</param>
    /// <param name="b">Tablero b</param>
    /// <returns>Determina si dos tableros son iguales</returns>
    public static bool EqualTable(Piece?[,] a, Piece?[,] b)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < b.GetLength(1); j++)
            {
                if (a[i, j] is not null)
                {
                    if (!a[i, j]!.Equals(b[i, j])) return false;
                }
                else
                {
                    if (b[i, j] is not null) return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Mostrar el historial del tablero
    /// </summary>
    /// <param name="ind">Indice del historial</param>
    /// <returns>Tablero</returns>
    public Piece?[,] HistoryTable(int ind) => History[ind];

    public Piece?[,] CurrentTable => History[History.Count - 1];
}