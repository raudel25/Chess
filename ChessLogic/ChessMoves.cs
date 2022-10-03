namespace ChessLogic;

public static class ChessMoves
{
    #region BasicMoves

    /// <summary>
    /// Determina todos los posibles movimientos de un jugador
    /// </summary>
    /// <param name="color">Color del jugador</param>
    /// <param name="table">Tablero</param>
    /// <returns>Posibles movimientos</returns>
    public static List<Play> PossibleMoves(Color color, Table table)
    {
        IEnumerable<Play> possible = new List<Play>();
        for (int i = 0; i < table.Rows; i++)
        {
            for (int j = 0; j < table.Columns; j++)
            {
                if (table[i, j] is not null) possible = possible.Concat(PossibleMoves(table[i, j]!, table));
            }
        }

        return possible.ToList();
    }

    /// <summary>
    /// Determina todos los posibles movimientos de una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Posibles movimientos</returns>
    public static List<Play> PossibleMoves(Piece piece, Table table) =>
        Move(piece, table).Concat(MoveCapture(piece, table)).Concat(MoveEnRock(piece, table))
            .Concat(MovePawnToQueen(piece, table)).Concat(MovePawnToStep(piece, table)).ToList();

    /// <summary>
    /// Determina el movimiento de una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles jugadas</returns>
    public static List<Play> Move(Piece piece, Table table)
    {
        if (ConditionPawnToQueen(piece)) return new List<Play>();

        List<(int, int)> aux = PossibleMoves(piece, piece.Move(table), table);
        List<Play> possible = new List<Play>();

        foreach (var item in aux) possible.Add(new Play(piece.Current, item, (-1, -1), table));

        return possible;
    }

    /// <summary>
    /// Determina el movimiento de captura de una pieza
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles jugadas</returns>
    public static List<Play> MoveCapture(Piece piece, Table table)
    {
        if (ConditionPawnToQueen(piece)) return new List<Play>();

        List<(int, int)> aux = PossibleMoves(piece, piece.MoveCapture(table), table);
        List<Play> possible = new List<Play>();

        foreach (var item in aux) possible.Add(new Play(piece.Current, item, item, table));

        return possible;
    }

    #endregion

    #region ComplexMoves

    /// <summary>
    /// Determina el enroque
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles jugadas</returns>
    public static List<PlayEnRock> MoveEnRock(Piece piece, Table table)
    {
        if (!piece.NotMove || piece is not King) return new List<PlayEnRock>();

        if (piece.Color == Color.White) return DeterminateEnRock(table, piece, 0);

        return DeterminateEnRock(table, piece, 7);
    }

    /// <summary>
    /// Determina el enroque
    /// </summary>
    /// <param name="piece">Piece</param>
    /// <param name="ind">indice</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    private static List<PlayEnRock> DeterminateEnRock(Table table, Piece piece, int ind)
    {
        List<PlayEnRock> possible = new List<PlayEnRock>();
        if (table[ind, 0] is Rock && table[ind, 1] is null && table[ind, 2] is null)
        {
            if (!table[ind, 0]!.NotMove) return possible;

            if (!TreatPosition(table, (ind, 1), piece.Color) && !TreatPosition(table, (ind, 2), piece.Color))
                possible.Add(new PlayEnRock(piece.Current, (ind, 0), (ind, 1), (ind, 2), table));
        }

        if (table[ind, 7] is Rock && table[ind, 6] is null && table[ind, 5] is null && table[ind, 4] is null)
        {
            if (!table[ind, 7]!.NotMove) return possible;

            if (!TreatPosition(table, (ind, 6), piece.Color) && !TreatPosition(table, (ind, 5), piece.Color) &&
                !TreatPosition(table, (ind, 4), piece.Color))
                possible.Add(new PlayEnRock(piece.Current, (ind, 6), (ind, 5), (ind, 4), table));
        }

        return possible;
    }

    /// <summary>
    /// Determina las jugadas con las que un peon puede coronar
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de jugadas</returns>
    public static List<PlayPawnToQueen> MovePawnToQueen(Piece piece, Table table)
    {
        if (!ConditionPawnToQueen(piece)) return new List<PlayPawnToQueen>();

        List<PlayPawnToQueen> possible = new List<PlayPawnToQueen>();

        foreach (var item in PossibleMoves(piece, piece.Move(table), table)
                     .Concat(PossibleMoves(piece, piece.MoveCapture(table), table)))
            possible.Add(new PlayPawnToQueen((Pawn) piece, piece.Current, item, item, table));

        return possible;
    }

    /// <summary>
    /// Determina la condicion para que un peon pueda coronar
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <returns>Determina la condicion para que un peon pueda coronar</returns>
    private static bool ConditionPawnToQueen(Piece piece)
    {
        if (piece is not Pawn) return false;

        if (piece.Color == Color.White && piece.Current.Item1 == 7) return true;

        if (piece.Color == Color.Black && piece.Current.Item1 == 1) return true;

        return false;
    }

    /// <summary>
    /// Determina el movimiento del peon al paso
    /// </summary>
    /// <param name="piece">Pieza</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public static List<Play> MovePawnToStep(Piece piece, Table table)
    {
        List<Play> possible = new List<Play>();

        if (piece is not Pawn) return possible;
        (int, int) current = piece.Current;
        int initialRow = piece.Color == Color.White ? 1 : 6;

        if (Math.Abs(current.Item1 - initialRow) == 3)
        {
            (int, int) directionMove = piece.Color == Color.White ? (1, 0) : (-1, 0);
            (int, int) positionKing = PositionKing(table, piece.Color);

            ((int, int) result1, (int, int) result2) = ((0, 0), (0, 0));
            bool aux = DecidePawnToStep(current, (0, 1), directionMove, table, ref result1, ref result2);
            if (aux)
            {
                if (!PossibleJake(piece, result1, result2, positionKing, table))
                    possible.Add(new Play(piece.Current, result1, result2, table));
            }

            aux = DecidePawnToStep(current, (0, -1), directionMove, table, ref result1, ref result2);
            if (aux)
            {
                if (!PossibleJake(piece, result1, result2, positionKing, table))
                    possible.Add(new Play(piece.Current, result1, result2, table));
            }
        }

        return possible;
    }

    /// <summary>
    /// Decide si es posible el peon al paso
    /// </summary>
    /// <param name="current">Posicion actual</param>
    /// <param name="directionPaw">Direccion del peon a capturar</param>
    /// <param name="directionMove">Direccion del movimiento</param>
    /// <param name="table">Tablero</param>
    /// <param name="result1">Posicion final</param>
    /// <param name="result2">Posicion del peon a capturar</param>
    /// <returns>Decide si es posible el peon al paso</returns>
    private static bool DecidePawnToStep((int, int) current, (int, int) directionPaw, (int, int) directionMove,
        Table table, ref (int, int) result1, ref (int, int) result2)
    {
        (int, int) positionPaw = Moves.SumPosition(current, directionPaw);
        (int, int) positionMove = Moves.SumPosition(positionPaw, directionMove);

        if (Moves.CorrectMove(positionPaw))
        {
            if (table[positionPaw.Item1, positionPaw.Item2] is Pawn)
            {
                Color color = table[positionPaw.Item1, positionPaw.Item2]!.Color;
                TableCopy history = table.HistoryTable(table.CantTurns - 1);
                
                if (history[color == Color.White ? 1 : 6, positionPaw.Item2] is Pawn &&
                    history[positionPaw.Item1, positionPaw.Item2] is null &&
                    table[positionMove.Item1, positionMove.Item2] is null)
                {
                    (result1, result2) = ((positionMove.Item1, positionMove.Item2),
                        (positionPaw.Item1, positionPaw.Item2));

                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    #region JakePositions

    /// <summary>
    /// Determina si el rey esta en jake
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="color">Color</param>
    /// <returns>Determina si el rey esta en jake</returns>
    public static bool Jake(Table table, Color color) => TreatPosition(table, PositionKing(table, color), color);

    /// <summary>
    /// Determina si una casilla esta amenazada
    /// </summary>
    /// <param name="table">tablero</param>
    /// <param name="position">Casilla</param>
    /// <param name="color">Color del jugador</param>
    /// <returns>Determina si una casilla esta amenazada</returns>
    private static bool TreatPosition(Table table, (int, int) position, Color color) =>
        TreatPosition(table, position, color, false);

    private static bool TreatPosition(Table table, (int, int) position, Color color, bool singleMove)
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
                            singleMove
                                ? table[i, j]!.MoveCapture(table)
                                : PossibleMoves(table[i, j]!, table[i, j]!.MoveCapture(table), table);
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
    private static List<(int, int)> PossibleMoves(Piece piece, List<(int, int)> possible, Table table)
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
    private static bool PossibleJake(Piece piece, (int, int) position, (int, int) positionCapture,
        (int, int) positionKing,
        Table table)
    {
        bool possible = false;
        (int, int) current = piece.Current;

        TableCopy copy = table.Copy();

        Piece? aux = table[positionCapture.Item1, positionCapture.Item2];
        (copy[position.Item1, position.Item2], copy[current.Item1, current.Item2]) =
            (copy[current.Item1, current.Item2], null);
        Console.WriteLine(piece.Current+"*");

        if (TreatPosition(copy, positionKing, piece.Color, true)) possible = true;

        (copy[positionCapture.Item1, positionCapture.Item2], copy[current.Item1, current.Item2]) =
            (aux, table[position.Item1, position.Item2]);

        return possible;
    }

    /// <summary>
    /// Determina la posicion del Rey
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="color">Color</param>
    /// <returns>Determina la posicion del Rey</returns>
    private static (int, int) PositionKing(Table table, Color color)
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

    #endregion
}