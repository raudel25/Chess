namespace ChessLogic;

public class Play
{
    /// <summary>
    /// Posicion actual de la pieza
    /// </summary>
    public (int, int) PositionCurrent { get; private set; }

    /// <summary>
    /// Posicion para moverse
    /// </summary>
    public (int, int) PositionMove { get; private set; }

    /// <summary>
    /// Posicion de la pieza que se va a capturar
    /// </summary>
    public (int, int) PositionCapture { get; private set; }

    /// <summary>
    /// Tablero
    /// </summary>
    protected readonly Table Table;

    internal Play((int, int) positionCurrent, (int, int) positionMove, (int, int) positionCapture, Table table)
    {
        this.PositionCurrent = positionCurrent;
        this.PositionMove = positionMove;
        this.PositionCapture = positionCapture;
        this.Table = table;
    }

    internal void BasicPlayGame()
    {
        if (PositionCapture != (-1, -1)) Table.Capture(PositionCapture);
        Table.Move(PositionCurrent, PositionMove);
    }

    /// <summary>
    /// Realizar la jugada
    /// </summary>
    public virtual void PlayGame()
    {
        ImmediateMove();
        BasicPlayGame();
        Table.ActPosition();
    }

    protected virtual void ImmediateMove()
    {
        TableCopy? copy = Table as TableCopy;
        if (copy is not null)
        {
            List<ImmediateMove> immediateMoves = new List<ImmediateMove>();
            immediateMoves.Clear();
            immediateMoves.Add(new ImmediateMove(Table[PositionCurrent.Item1, PositionCurrent.Item2]!,
                PositionCapture == (-1, -1) ? null : Table[PositionCapture.Item1, PositionCapture.Item2],
                PositionCurrent, PositionMove, PositionCapture));

            copy.AddLastMove(immediateMoves);
        }
    }
}

public class PlayEnRock : Play
{
    /// <summary>
    /// Jugada de la torre
    /// </summary>
    private readonly Play _playRock;

    internal PlayEnRock((int, int) kingCurrent, (int, int) rockCurrent, (int, int) kingMove,
        (int, int) rockMove, Table table) : base(kingCurrent, kingMove, (-1, -1), table)
    {
        _playRock = new Play(rockCurrent, rockMove, (-1, -1), table);
    }

    public override void PlayGame()
    {
        ImmediateMove();
        BasicPlayGame();
        _playRock.BasicPlayGame();
        Table.ActPosition();
    }

    protected override void ImmediateMove()
    {
        TableCopy? copy = Table as TableCopy;
        if (copy is not null)
        {
            List<ImmediateMove> immediateMoves = new List<ImmediateMove>();
            immediateMoves.Add(new ImmediateMove(Table[PositionCurrent.Item1, PositionCurrent.Item2]!,
                null, PositionCurrent, PositionMove, PositionCapture));
            immediateMoves.Add(new ImmediateMove(
                Table[_playRock.PositionCurrent.Item1, _playRock.PositionCurrent.Item2]!,
                null, _playRock.PositionCurrent, _playRock.PositionMove, _playRock.PositionCapture));

            copy.AddLastMove(immediateMoves);
        }
    }
}

public class PlayPawnToQueen : Play
{
    /// <summary>
    /// Pieza para convertir
    /// </summary>
    private Piece _piece;

    internal PlayPawnToQueen(Color color, (int, int) positionCurrent, (int, int) positionMove,
        (int, int) positionCapture, Table table) : base(positionCurrent, positionMove, positionCapture, table)
    {
        this._piece = new Queen(color);
    }

    /// <summary>
    /// Seeccionar la pieza para convertir
    /// </summary>
    /// <param name="ind">Indice de la pieza a convertir</param>
    public void Convert(int ind)
    {
        switch (ind)
        {
            case 0:
                _piece = new Queen(_piece.Color);
                break;
            case 1:
                _piece = new Rock(_piece.Color);
                break;
            case 2:
                _piece = new Bishop(_piece.Color);
                break;
            default:
                _piece = new Knight(_piece.Color);
                break;
        }
    }

    public override void PlayGame()
    {
        BasicPlayGame();
        Table.Convert(_piece, this.PositionMove);
        Table.ActPosition();
    }
}