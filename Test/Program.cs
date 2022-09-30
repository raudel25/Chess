using ChessEngine;

Table t=new Table();

Console.WriteLine(t[0,0]!.Positions.Current);
t.Copy[0, 0] = null;
Console.WriteLine(t[0,0]!.Positions.Current);