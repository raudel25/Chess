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
        if (piece is King) return PossibleJake(piece.Move(table), table, piece.Color);

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
        if (piece is King) return PossibleJake(piece.MoveCapture(table), table, piece.Color);

        return piece.Move(table);
    }

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
    /// Determina las casillas en las que el rey no puede jugar
    /// </summary>
    /// <param name="possible">Lista de posibles jugadas</param>
    /// <param name="table">Tablero</param>
    /// <param name="color">Color</param>
    /// <returns>Lista de posibles jugadas</returns>
    public List<(int, int)> PossibleJake(List<(int, int)> possible, Piece?[,] table, Color color)
    {
        List<(int, int)> possibleAct = new List<(int, int)>();

        foreach (var item in possible)
        {
            if (!TreatPosition(table, item, color, true)) possibleAct.Add(item);
        }

        return possibleAct;
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