using ChessLogic;

namespace ChessGame;

public static class Printer
{
    public static string[,] Table(Table table)
    {
        string[,] print = new string[table.Rows, table.Columns];
        
        for (int i = 0; i < table.Rows; i++)
        {
            for (int j = 0; j < table.Columns; j++)
            {
                if (table[i, j] is not null) print[i, j] = table[i, j]!.ToString()!;
                else print[i, j] = ((i + j) & 1) == 0 ? "B" : "W";
            }            
        }

        return print;
    }
}