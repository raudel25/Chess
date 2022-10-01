using ChessLogic;

Table t=new Table();

List<(int,int)> l=t[0,1]!.Move(t);

foreach (var VARIABLE in l)
{
    Console.WriteLine(VARIABLE);
}