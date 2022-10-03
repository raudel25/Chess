using ChessLogic;

Table t=new Table();

List<Play> l = ChessMoves.Move(t[1, 0]!,t);
l[0].PlayGame();

TableCopy copy = t.HistoryTable(0);

Print(t);
Console.WriteLine();
Print(copy);



static void Print(Table copy)
{
    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            if(copy[i,j] is not null) Console.Write(1+" ");
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