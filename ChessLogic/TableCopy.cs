namespace ChessLogic;

public class TableCopy : Table
{
    /// <summary>
    /// Copia del tablero original
    /// </summary>
    private readonly Piece?[,] _copy;

    /// <summary>
    /// Piezas propias del tablero
    /// </summary>
    private readonly HashSet<Piece> _pieces;

    public new Piece? this[int i, int j]
    {
        get
        {
            if (i < 0 || j < 0 || i >= 8 || j >= 8) throw new IndexOutOfRangeException();
            return TablePieces[i, j];
        }
        set
        {
            if (i < 0 || j < 0 || i >= 8 || j >= 8) throw new IndexOutOfRangeException();
            TablePieces[i, j] = value;

            if (TablePieces[i, j] is not null)
            {
                if (_pieces.Contains(TablePieces[i, j]!)) TablePieces[i, j]!.Current = (i, j);
            }
        }
    }

    public TableCopy(Piece?[,] table) : base(BuildCopy(table))
    {
        this._copy = BuildCopy(table);
        this._pieces = new HashSet<Piece>();
        SetSave();
    }

    /// <summary>
    /// Devolver el tablero a su estado original
    /// </summary>
    public void Reset()
    {
        this.TablePieces = BuildCopy(_copy);
        SetSave();
    }

    /// <summary>
    /// Guardar las piezas del tablero
    /// </summary>
    private void SetSave()
    {
        this._pieces.Clear();

        for (int i = 0; i < TablePieces.GetLength(0); i++)
        {
            for (int j = 0; j < TablePieces.GetLength(1); j++)
            {
                if (TablePieces[i, j] is not null) this._pieces.Add(TablePieces[i, j]!);
            }
        }
    }
}