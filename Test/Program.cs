using ChessEngine;

Piece[,] table = new Piece[8,8];

Bishop r = new Bishop(Color.White);
Tower t = new Tower(Color.White);
t.Positions.Add((1, 4));
r.Positions.Add((1,1));

table[1, 1] = r;
table[1, 4] = t;

List<(int, int)> l = r.Move(table);

foreach (var VARIABLE in l)
{
    Console.WriteLine(VARIABLE);
} 