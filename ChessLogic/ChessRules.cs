using System.ComponentModel;

namespace ChessLogic;

public static class ChessRules
{
    public static bool JakeMate(Color color, Table table) =>
        ChessMoves.Jake(table, color) && ChessMoves.PossibleMoves(color, table).Count == 0;

    public static bool DrawDrownedKing(Color color, Table table) =>
        !ChessMoves.Jake(table, color) && ChessMoves.PossibleMoves(color, table).Count == 0;

}