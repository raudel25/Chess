namespace ChessLogic;

public class TableCopy : Table
{
    private readonly Piece?[,] _copy;

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
        }
    }

    public TableCopy(Piece?[,] table) : base(BuildCopy(table))
    {
        this._copy = BuildCopy(table);
    }

    public void Reset()
    {
        this.TablePieces = BuildCopy(_copy);
    }

    private static Piece?[,] BuildCopy(Piece?[,] table)
    {
        Piece?[,] copy = new Piece[8, 8];

        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if(table[i,j] is not null)
                {
                    copy[i, j] = table[i, j]!.Clone();
                    copy[i, j]!.Positions.Add((i, j));
                }
            }
        }

        return copy;
    }
}