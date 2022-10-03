using ChessLogic;

Table t=new Table();

TableCopy copy = t.Copy();

// copy[2, 2] = copy[0, 0];
// Console.WriteLine(copy[0,0].Current);
// List<Play> l = ChessMoves.Move(copy[1, 0]!, copy);
// Console.WriteLine(copy[1,0].Current);

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