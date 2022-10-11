using ChessLogic;

namespace ChessGame;

public class MinMaxPlayer : IStrategy
{
    public Play Move(Color color, Table table)
    {
        TableCopy copy = table.Copy();

        List<Play> possible = ChessMoves.PossibleMoves(color, copy);
        int move = int.MinValue;
        int moveInd = 0;

        for (int i = 0; i < possible.Count; i++)
        {
            possible[i].PlayGame();
            int valor = BestMove(3, int.MinValue, int.MaxValue, color, color == Color.White ? Color.Black : Color.White,
                copy);
            if (valor >= move)
            {
                if (valor == move)
                {
                    Random rnd = new Random();
                    if ((rnd.Next(2) & 1) == 0) (move, moveInd) = (valor, i);
                }
                else (move, moveInd) = (valor, i);
            }

            copy.ResetPlay();
        }

        possible = ChessMoves.PossibleMoves(color, table);
        return possible[moveInd];
    }

    private int BestMove(int depth, int alpha, int beta, Color myColor, Color simulationColor, TableCopy table)
    {
        if (depth == 0) return Evaluate(myColor, table);

        List<Play> possible = ChessMoves.PossibleMoves(simulationColor, table);

        int move = myColor == simulationColor ? int.MinValue : int.MaxValue;
        foreach (var item in possible)
        {
            item.PlayGame();
            move = myColor == simulationColor
                ? Math.Max(
                    BestMove(depth - 1, alpha, beta, myColor,
                        simulationColor == Color.White ? Color.Black : Color.White, table), move)
                : Math.Min(
                    BestMove(depth - 1, alpha, beta, myColor,
                        simulationColor == Color.White ? Color.Black : Color.White, table), move);
            table.ResetPlay();

            if (myColor == simulationColor) alpha = Math.Max(alpha, move);
            else beta = Math.Min(beta, move);

            if (alpha >= beta) return move;
        }

        return move;
    }

    private int Evaluate(Color color, TableCopy table)
    {
        int sum = 0;
        for (int i = 0; i < table.Rows; i++)
        {
            for (int j = 0; j < table.Columns; j++)
            {
                if (table[i, j] is not null)
                {
                    int valor = table[i, j]!.NotMove ? table[i, j]!.Valor : table[i, j]!.Valor + 1;
                    if (table[i, j]!.Color == color) sum += valor;
                    else sum -= valor;
                }
            }
        }

        return sum;
    }
}