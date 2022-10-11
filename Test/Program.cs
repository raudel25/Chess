using ChessGame;
using ChessLogic;

// Table t = new Table();
// TableCopy t1 = t.Copy();
//
// List<Play> l=ChessMoves.PossibleMoves(Color.White,t1);
// l[4].PlayGame();
// t1.ResetPlay();
// l[5].PlayGame();
//
// Print(Printer.Table(t1));
Game game = new Game(new HumanPlayer(), new MinMaxPlayer());

foreach (var item in game.RunGame())
{

    Print(item, true);

    Thread.Sleep(1000);
}

static void Print(string[,] item, bool invert=true)
{
    Console.Clear();


    for (int i = invert ? 7 : 0; invert ? i >= 0 : i < 8; i = invert ? i - 1 : i + 1)
    {
        Console.Write(i + 1 + " ");
        for (int j = 0; j < 8; j++)
        {
            if (item[i, j] == "B" || item[i, j] == "W")
            {
                Console.Write(" * ");
                continue;
            }

            Console.Write(item[i, j] + " ");
        }

        Console.WriteLine();
    }
    Console.Write("  ");
    for (int i = 0; i < 8; i++) Console.Write(" " + (char)('A' + i) + " ");
    Console.WriteLine();
}