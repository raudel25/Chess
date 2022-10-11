namespace ChessLogic;

public class TableCopy : Table
{
    /// <summary>
    /// Copia del tablero original
    /// </summary>
    private readonly Piece?[,] _copy;

    /// <summary>
    /// Ultimos movientos
    /// </summary>
    private readonly List<List<ImmediateMove>> _moves;

    public TableCopy(Piece?[,] table, List<Piece?[,]> history) : base(BuildCopy(table), history)
    {
        this._copy = table;
        this._moves = new List<List<ImmediateMove>>();
    }

    /// <summary>
    /// Devolver el tablero a su estado original
    /// </summary>
    public void Reset()
    {
        this.TablePieces = BuildCopy(_copy);
    }

    public void ResetPlay()
    {
        if (_moves.Count == 0) return;

        foreach (var item in _moves[_moves.Count-1])
        {
            TablePieces[item.PosMove.Item1, item.PosMove.Item2] = null;
            TablePieces[item.PosCurrent.Item1, item.PosCurrent.Item2] = item.Current;
            TablePieces[item.PosCurrent.Item1, item.PosCurrent.Item2]!.Current = item.PosCurrent;
            if (item.PosCapture != (-1, -1))
            {
                TablePieces[item.PosCapture.Item1, item.PosCapture.Item2] = item.Capture;
                TablePieces[item.PosCapture.Item1, item.PosCapture.Item2]!.Current = item.PosCapture;
            }
        }

        _moves.Remove(_moves[_moves.Count - 1]);
        CantTurns--;
        Turn = Turn == Color.White ? Color.Black : Color.White;
    }

    public void AddLastMove(List<ImmediateMove> moves) => _moves.Add(moves);
}