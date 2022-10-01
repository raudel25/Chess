namespace ChessLogic;

public class Table
{
    /// <summary>
    /// Tablero
    /// </summary>
    private readonly Piece?[,] _table;

    /// <summary>
    /// Cantidad de filas
    /// </summary>
    public int Rows => 8;

    /// <summary>
    /// Cantidad de columnas
    /// </summary>
    public int Columns => 8;
    
    /// <summary>
    /// Copia del tablero
    /// </summary>
    public Piece?[,] Copy { get; set; }

    public Piece? this[int i, int j]
    {
        get
        {
            if (i < 0 || j < 0 || i >= 8 || j >= 8) throw new IndexOutOfRangeException();
            return _table[i, j];
        }
    }

    public Table()
    {
        this._table = StartPosition();
        this.Copy = new Piece[8, 8];
        Reset();
    }

    private Table(Piece?[,] table)
    {
        this._table = table;
        this.Copy = new Piece[8, 8];
        Reset();
    }

    /// <summary>
    /// Distribuir las posiciones iniciales del tablero
    /// </summary>
    /// <returns>Posiciones iniciales</returns>
    private Piece?[,] StartPosition()
    {
        Piece?[,] table = new Piece[8, 8];

        (table[0, 0], table[0, 7]) = (new Rock(Color.White),new Rock(Color.White));
        (table[7, 0], table[7, 7]) = (new Rock(Color.Black),new Rock(Color.Black));
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
                if(table[i,j] is not null) table[i,j]!.Positions.Add((i,j));
            }
        }

        return table;
    }

    public void Reset()
    {
        for (int i = 0; i < _table.GetLength(0); i++)
        {
            for (int j = 0; j < _table.GetLength(1); j++) Copy[i, j] = _table[i, j];
        }
    }

    public void Capture((int, int) position) => _table[position.Item1, position.Item2] = null;
    
    public void Move((int, int) positionCurrent, (int, int) positionMove) =>
        (_table[positionCurrent.Item1, positionCurrent.Item2], _table[positionMove.Item1, positionMove.Item2]) =
        (null, _table[positionCurrent.Item1, positionCurrent.Item2]);

    public void Convert(Piece piece, (int, int) position) => _table[position.Item1, position.Item2] = piece;

    public Table ActPosition()
    {
        Piece?[,] table = new Piece[_table.GetLength(0), _table.GetLength(1)];

        for (int i = 0; i < _table.GetLength(0); i++)
        {
            for (int j = 0; j < _table.GetLength(1); j++)
            {
                if(_table[i,j] is not null) _table[i,j]!.Positions.Add((i,j));
                table[i, j] = _table[i, j];
            }
        }

        return new Table(table);
    }
}