namespace ChessEngine;

public class JudgeMoves
{
    /// <summary>
    /// Determina el movimiento de una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> Move(Piece piece, Table table) => ConditionPawnToQueen(piece)
        ? new List<(int, int)>()
        : PossibleMoves(piece, piece.Move(table), table);

    /// <summary>
    /// Determina el enroque
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int, int, int)> MoveEnRock(Piece piece, Table table)
    {
        if (!piece.NotMove() || piece is not King) return new List<(int, int, int, int)>();

        if (piece.Color == Color.White) return DeterminateEnRock(table, piece.Color, 0);

        return DeterminateEnRock(table, piece.Color, 7);
    }

    /// <summary>
    /// Determina las jugadas con las que un peon puede coronar
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de casillas</returns>
    public List<(int, int)> MovePawnToQueen(Piece piece, Table table) => ConditionPawnToQueen(piece)
        ? PossibleMoves(piece, piece.Move(table), table)
            .Concat(PossibleMoves(piece, piece.MoveCapture(table), table)).ToList()
        : new List<(int, int)>();

    /// <summary>
    /// Determina el movimiento de captura de una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> MoveCapture(Piece piece, Table table) => ConditionPawnToQueen(piece)
        ? new List<(int, int)>()
        : PossibleMoves(piece, piece.Move(table), table);

    /// <summary>
    /// Determina el movimiento del peon al paso
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int, int, int)> MovePawnToStep(Piece piece, Table table)
    {
        List<(int, int, int, int)> possible = new List<(int, int, int, int)>();

        if (piece is not Pawn) return possible;
        (int, int) current = piece.Positions.Current;
        (int, int) initial = piece.Positions[0];

        if (Math.Abs(current.Item1 - initial.Item1) == 3)
        {
            (int, int) directionMove = piece.Color == Color.White ? (1, 0) : (-1, 0);
            (int, int) positionKing = PositionKing(table, piece.Color);

            var aux = DecidePawnToStep(current, (0, 1), directionMove, table);
            if (aux != (-1, -1, -1, -1))
            {
                if (!PossibleJake(piece, (aux.Item1, aux.Item2), (aux.Item3, aux.Item4), positionKing, table))
                    possible.Add(aux);
            }

            aux = DecidePawnToStep(current, (0, -1), directionMove, table);
            if (aux != (-1, -1, -1, -1))
            {
                if (!PossibleJake(piece, (aux.Item1, aux.Item2), (aux.Item3, aux.Item4), positionKing, table))
                    possible.Add(aux);
            }
        }

        return possible;
    }

    /// <summary>
    /// Determina si el rey esta en jake
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="color">Color</param>
    /// <returns>Determina si el rey esta en jake</returns>
    public bool Jake(Table table, Color color) => TreatPosition(table, PositionKing(table, color), color);

    /// <summary>
    /// Determina si una casilla esta amenazada
    /// </summary>
    /// <param name="table">tablero</param>
    /// <param name="position">Casilla</param>
    /// <param name="color">Color del jugador</param>
    /// <returns>Determina si una casilla esta amenazada</returns>
    private bool TreatPosition(Table table, (int, int) position, Color color) =>
        TreatPosition(table, position, color, false);

    /// <summary>
    /// Determina la condicion para que un peon pueda coronar
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <returns>Determina la condicion para que un peon pueda coronar</returns>
    private bool ConditionPawnToQueen(Piece piece)
    {
        if (piece is not Pawn) return false;

        if (piece.Color == Color.White && piece.Positions.Current.Item1 == 1) return true;

        if (piece.Color == Color.Black && piece.Positions.Current.Item1 == 7) return true;

        return false;
    }

    /// <summary>
    /// Decide si es posible el peon al paso
    /// </summary>
    /// <param name="current">Posicion actual</param>
    /// <param name="directionPaw">Direccion del peon a capturar</param>
    /// <param name="directionMove">Direccion del movimiento</param>
    /// <param name="table">Tablero</param>
    /// <returns>Posicion final y peon a capturar</returns>
    private (int, int, int, int) DecidePawnToStep((int, int) current, (int, int) directionPaw, (int, int) directionMove,
        Table table)
    {
        (int, int) positionPaw = Moves.SumPosition(current, directionPaw);
        (int, int) positionMove = Moves.SumPosition(positionPaw, directionMove);

        if (Moves.CorrectMove(positionPaw))
        {
            if (table[positionPaw.Item1, positionPaw.Item2] is Pawn)
            {
                if (table[positionPaw.Item1, positionPaw.Item2]!.NotMove(1) &&
                    table[positionMove.Item1, positionMove.Item2] is null)
                    return (positionMove.Item1, positionMove.Item2, positionPaw.Item1, positionPaw.Item2);
            }
        }

        return (-1, -1, -1, -1);
    }

    private bool TreatPosition(Table table, (int, int) position, Color color, bool singleMove)
    {
        for (int i = 0; i < table.Rows; i++)
        {
            for (int j = 0; j < table.Columns; j++)
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
    /// Determina si la jugada es valida para los distintos movimientos
    /// </summary>
    /// <param name="possible">Lista de posibles jugadas</param>
    /// <param name="table">Tablero</param>
    /// <param name="piece">Pieza</param>
    /// <returns>Lista de posibles jugadas</returns>
    private List<(int, int)> PossibleMoves(Piece piece, List<(int, int)> possible, Table table)
    {
        List<(int, int)> possibleAct = new List<(int, int)>();

        bool king = piece is King;
        (int, int) positionKing = king ? (-1, -1) : PositionKing(table, piece.Color);

        foreach (var item in possible)
        {
            if (!PossibleJake(piece, item, item, king ? item : positionKing, table)) possibleAct.Add(item);
        }

        return possibleAct;
    }

    /// <summary>
    /// Determina si El rey queda en jake para un movimiento
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="position">Posicion</param>
    /// <param name="positionCapture">Posicion de la pieza a capturar</param>
    /// <param name="positionKing">Posicion del rey</param>
    /// <param name="table">Tablero</param>
    /// <returns>Determina si El rey queda en jake para un movimiento</returns>
    private bool PossibleJake(Piece piece, (int, int) position, (int, int) positionCapture, (int, int) positionKing,
        Table table)
    {
        bool possible = false;
        (int, int) current = piece.Positions.Current;

        Piece? aux = table[positionCapture.Item1, positionCapture.Item2];
        (table.Copy[position.Item1, position.Item2], table.Copy[current.Item1, current.Item2]) =
            (table[current.Item1, current.Item2], null);

        if (TreatPosition(table, positionKing, piece.Color, true)) possible = true;

        (table.Copy[positionCapture.Item1, positionCapture.Item2], table.Copy[current.Item1, current.Item2]) =
            (aux, table[position.Item1, position.Item2]);

        return possible;
    }

    /// <summary>
    /// Determina la posicion del Rey
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="color">Color</param>
    /// <returns>Determina la posicion del Rey</returns>
    private (int, int) PositionKing(Table table, Color color)
    {
        for (int i = 0; i < table.Rows; i++)
        {
            for (int j = 0; j < table.Columns; j++)
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
    private List<(int, int, int, int)> DeterminateEnRock(Table table, Color color, int ind)
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