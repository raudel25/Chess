namespace ChessLogic;

public class Play
{
    public (int, int) PositionCurrent { get; private set; }

    public (int, int) PositionMove { get; private set; }

    public (int, int) PositionCapture { get; private set; }

    protected readonly Table Table;

    internal Play((int, int) positionCurrent, (int, int) positionMove, (int, int) positionCapture, Table table)
    {
        this.PositionCurrent = positionCurrent;
        this.PositionMove = positionMove;
        this.PositionCapture = positionCapture;
        this.Table = table;
    }

    protected void BasicPlayGame()
    {
        if (PositionCapture != (-1, -1)) Table.Capture(PositionCapture);
        Table.Move(PositionCurrent, PositionMove);
    }

    public virtual void PlayGame()
    {
        BasicPlayGame();
        Table.ActPosition();
    }
}

public class PlayEnRock : Play
{
    private readonly Play _playRock;

    internal PlayEnRock((int, int) kingCurrent, (int, int) rockCurrent, (int, int) kingMove,
        (int, int) rockMove, Table table) : base(kingCurrent, kingMove, (-1, -1), table)
    {
        _playRock = new Play(rockCurrent, rockMove, (-1, -1), table);
    }

    public override void PlayGame()
    {
        BasicPlayGame();
        _playRock.PlayGame();
        Table.ActPosition();
    }
}

public class PlayPawnToQueen : Play
{
    private Piece Piece { get;}

    internal PlayPawnToQueen(Pawn pawn, (int, int) positionCurrent, (int, int) positionMove,
        (int, int) positionCapture,Table table) : base(positionCurrent, positionMove, positionCapture,table)
    {
        this.Piece = new Queen(pawn.Color);
    }

    public override void PlayGame()
    {
        BasicPlayGame();
        Table.Convert(Piece, this.PositionMove);
        Table.ActPosition();
    }
}