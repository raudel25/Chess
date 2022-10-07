using ChessGame;

Game game=new Game(new HumanPlayer(),new RandomPLayer());

foreach (var item in game.RunGame())
{
    Console.Clear();

    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            if (item[i, j] == "B" || item[i, j] == "W")
            {
                Console.Write(" *  ");
                continue;
            }
            if(item[i,j].Length==2) Console.Write(item[i,j]+"  ");
            else
            {
                Console.Write(item[i,j]+" ");
            }
        }
        Console.WriteLine();
        
        
    }
    Thread.Sleep(3000);
    
}