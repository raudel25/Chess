namespace ChessLogic;

public class Play
{
    public (int, int) PositionCurrent { get; private set; }

    public (int, int) PositionMove { get; private set; }

    public (int, int) PositionCapture { get; private set; }

    internal Play((int, int) positionCurrent, (int, int) positionMove, (int, int) positionCapture)
    {
        this.PositionCurrent = positionCurrent;
        this.PositionMove = positionMove;
        this.PositionCapture = positionCapture;
    }

    public virtual void PlayGame(Table table)
    {
        if (PositionCapture != (-1, -1)) table.Capture(PositionCapture);
        table.Move(PositionCurrent, PositionMove);
    }
}

public class PlayEnRock : Play
{
    private readonly Play _playRock;

    internal PlayEnRock((int, int) kingCurrent, (int, int) rockCurrent, (int, int) kingMove, (int, int) rockMove) : base(
        kingCurrent, kingMove, (-1, -1))
    {
        _playRock = new Play(rockCurrent, rockMove, (-1, -1));
    }

    public override void PlayGame(Table table)
    {
        base.PlayGame(table);
        _playRock.PlayGame(table);
    }
}

public class PlayPawnToQueen : Play
{
    public Piece Piece { get; set; }

    internal PlayPawnToQueen(Color color, (int, int) positionCurrent, (int, int) positionMove,
        (int, int) positionCapture) : base(
        positionCurrent, positionMove, positionCapture)
    {
        this.Piece = new Queen(color);
    }

    public override void PlayGame(Table table)
    {
        base.PlayGame(table);
        table.Convert(Piece, this.PositionMove);
    }
}