namespace ChessLogic;

public static class ChessRules
{
    public static bool JakeMate(Color color, Table table) =>
        ChessMoves.Jake(table, color) && ChessMoves.PossibleMoves(color, table).Count == 0;

    public static bool Draw(Table table, Color color) =>
        Draw50StepKing(table) || DrawDrownedKing(color, table) || DrawEqualsPositions(table);
    
    public static bool DrawDrownedKing(Color color, Table table) =>
        !ChessMoves.Jake(table, color) && ChessMoves.PossibleMoves(color, table).Count == 0;

    public static bool DrawEqualsPositions(Table table)
    {
        if (table.CantTurns < 8) return false;

        List<Piece?[,]> positions = new List<Piece?[,]>();
        for (int i = 8; i >= 0; i++)   positions.Add(table.HistoryTable(table.CantTurns-i));

        bool a = Table.EqualTable(positions[0], positions[4]) && Table.EqualTable(positions[4], positions[8]);
        bool b = Table.EqualTable(positions[1], positions[5]);
        bool c = Table.EqualTable(positions[2], positions[6]);
        bool d = Table.EqualTable(positions[3], positions[7]);

        return a && b && c && d;
    }

    public static bool Draw50StepKing(Table table)
    {
        if (table.CantTurns < 99) return false;

        return OnePiece(table.HistoryTable(table.CantTurns - 99));
    }

    private static bool OnePiece(Piece?[,] table)
    {
        (int white, int black) = (0, 0);
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if (table[i, j] is not null)
                {
                    if (table[i, j]!.Color == Color.White) white++;
                    else black++;
                }
            }
        }

        return white == 1 || black == 1;
    }
}