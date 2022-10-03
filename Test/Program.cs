using ChessLogic;

Table t=new Table();

List<Play> l;
// l = ChessMoves.Move(t[0, 6]!,t);
// l[1].PlayGame();
// l = ChessMoves.Move(t[2, 5]!, t);
// l[2].PlayGame();
// l = ChessMoves.MoveCapture(t[4, 6]!, t);
// l[1].PlayGame();
l = ChessMoves.Move(t[6, 2]!, t);
l[0].PlayGame();
l = ChessMoves.Move(t[5, 2]!, t);
l[0].PlayGame();
l = ChessMoves.MovePawnToStep(t[4, 1]!, t);
Console.WriteLine(l.Count);

// l = ChessMoves.Move(t[2, 2]!, t);
// l[0].PlayGame();
// Console.WriteLine(t.HistoryTable(t.CantTurns)[0,1] is not null);
// Console.WriteLine(Table.EqualTable(t.HistoryTable(0),t.HistoryTable(t.CantTurns)));

// TableCopy copy = new TableCopy(t.HistoryTable(0));

Print(t);
Console.WriteLine();



static void Print(Table copy)
{
    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            if (copy[i, j] is not null)
            {
                if(copy[i,j] is Knight) Console.Write("C"+" ");
                else Console.Write(1+" ");
            }
            else Console.Write(0+" ");
        }
        Console.WriteLine();
    }
}

// static void Print1(TableCopy copy)
// {
//     for (int i = 0; i < 8; i++)
//     {
//         for (int j = 0; j < 8; j++)
//         {
//             if(copy[i,j] is not null) Console.Write(1+" ");
//             else Console.Write(0+" ");
//         }
//         Console.WriteLine();
//     }
// }