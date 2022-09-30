namespace ChessEngine;

public class JudgeMoves
{
    /// <summary>
    /// Determina el movimiento de una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> Move(Piece piece, Piece?[,] table)
    {
        if (piece is King) return PossibleJake(piece, piece.Move(table), table);

        return piece.Move(table);
    }

    /// <summary>
    /// Determina el enroque
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int, int, int)> MoveEnRock(Piece piece, Piece?[,] table)
    {
        if (!piece.NotMove()) return new List<(int, int, int, int)>();

        if (piece.Color == Color.White) return DeterminateEnRock(table, piece.Color, 0);

        return DeterminateEnRock(table, piece.Color, 7);
    }

    /// <summary>
    /// Determina el movimiento de captura de una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> MoveCapture(Piece piece, Piece?[,] table)
    {
        if (piece is King) return PossibleJake(piece, piece.MoveCapture(table), table);

        return piece.Move(table);
    }

    /// <summary>
    /// Determina si el rey esta en jake
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="color">Color</param>
    /// <returns>Determina si el rey esta en jake</returns>
    public bool Jake(Piece?[,] table, Color color) => TreatPosition(table, PositionKing(table, color), color);

    /// <summary>
    /// Determina si una casilla esta amenazada
    /// </summary>
    /// <param name="table">tablero</param>
    /// <param name="position">Casilla</param>
    /// <param name="color">Color del jugador</param>
    /// <returns>Determina si una casilla esta amenazada</returns>
    private bool TreatPosition(Piece?[,] table, (int, int) position, Color color) =>
        TreatPosition(table, position, color, false);

    private bool TreatPosition(Piece?[,] table, (int, int) position, Color color, bool singleMove)
    {
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if (table[i, j] is not null)
                {
                    if (table[i, j]!.Color == color)
                    {
                        List<(int, int)> positions =
                            singleMove ? table[i, j]!.MoveCapture(table) : MoveCapture(table[i, j]!, table);
                        if (positions.Contains(position)) return true;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Determina si el rey esta en jake para los distintos movimientos
    /// </summary>
    /// <param name="possible">Lista de posibles jugadas</param>
    /// <param name="table">Tablero</param>
    /// <param name="piece">Pieza</param>
    /// <returns>Lista de posibles jugadas</returns>
    private List<(int, int)> PossibleJake(Piece piece, List<(int, int)> possible, Piece?[,] table)
    {
        List<(int, int)> possibleAct = new List<(int, int)>();

        bool king = piece is King;
        (int, int) positionKing = king ? (-1, -1) : PositionKing(table, piece.Color);
        (int, int) current = piece.Positions.Current;

        foreach (var item in possible)
        {
            Piece? aux = table[item.Item1, item.Item2];
            (table[item.Item1, item.Item2], table[current.Item1, current.Item2]) =
                (table[current.Item1, current.Item2], null);

            if (!TreatPosition(table, king ? item : positionKing, piece.Color, true)) possibleAct.Add(item);

            (table[item.Item1, item.Item2], table[current.Item1, current.Item2]) = (aux, table[item.Item1, item.Item2]);
        }

        return possibleAct;
    }

    /// <summary>
    /// Determina la posicion del Rey
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="color">Color</param>
    /// <returns>Determina la posicion del Rey</returns>
    private (int, int) PositionKing(Piece?[,] table, Color color)
    {
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if (table[i, j] is King)
                {
                    if (table[i, j]!.Color == color) return (i, j);
                }
            }
        }

        return (-1, -1);
    }

    /// <summary>
    /// Determina el enroque
    /// </summary>
    /// <param name="color">Color</param>
    /// <param name="ind">indice</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    private List<(int, int, int, int)> DeterminateEnRock(Piece?[,] table, Color color, int ind)
    {
        List<(int, int, int, int)> possible = new List<(int, int, int, int)>();
        if (table[ind, 0] is Rock && table[ind, 1] is null && table[ind, 2] is null)
        {
            if (!table[ind, 0]!.NotMove()) return possible;

            if (!TreatPosition(table, (ind, 1), color) && !TreatPosition(table, (ind, 2), color))
                possible.Add((ind, 1, ind, 2));
        }

        if (table[ind, 7] is Rock && table[ind, 6] is null && table[ind, 5] is null && table[ind, 4] is null)
        {
            if (!table[ind, 7]!.NotMove()) return possible;

            if (!TreatPosition(table, (ind, 6), color) && !TreatPosition(table, (ind, 5), color) &&
                !TreatPosition(table, (ind, 4), color)) possible.Add((ind, 5, ind, 4));
        }

        return possible;
    }
}