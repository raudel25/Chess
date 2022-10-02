using ChessLogic;

Table t=new Table();

TableCopy copy = t.Copy();

List<Play> l = ChessMoves.Move(copy[1, 0]!, copy);
l[0].PlayGame();
l = ChessMoves.Move(copy[2, 0]!, copy);
l[0].PlayGame();

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