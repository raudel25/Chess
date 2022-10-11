using System.Text.RegularExpressions;

namespace ChessLogic;

public class ImmediateMove
{
    public Piece? Capture { get; private set; }
    public Piece Current { get; private set; }
    public (int, int) PosCapture { get; private set; }
    public (int, int) PosCurrent { get; private set; }
    public (int, int) PosMove { get; private set; }

    public ImmediateMove(Piece current, Piece? capture, (int, int) posCurrent, (int, int) posMove,
        (int, int) posCapture)
    {
        this.Capture = capture;
        this.Current = current;
        this.PosCapture = posCapture;
        this.PosCurrent = posCurrent;
        this.PosMove = posMove;
    }
}