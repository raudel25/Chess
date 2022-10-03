namespace ChessLogic;

public class TableCopy : Table
{
    private readonly Piece?[,] _copy;

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
                if(_pieces.Contains(TablePieces[i,j]!)) TablePieces[i, j]!.Current = (i, j);
            }
        }
    }

    public TableCopy(Piece?[,] table) : base(BuildCopy(table))
    {
        this._copy = BuildCopy(table);
        this._pieces = new HashSet<Piece>();
        SetSave();
    }

    public void Reset()
    {
        this.TablePieces = BuildCopy(_copy);
        SetSave();
    }

    private static Piece?[,] BuildCopy(Piece?[,] table)
    {
        Piece?[,] copy = new Piece[8, 8];

        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if (table[i, j] is not null)
                {
                    copy[i, j] = table[i, j]!.Clone();
                    copy[i, j]!.Current = (i, j);
                }
            }
        }

        return copy;
    }

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