using ChessLogic;

Table t=new Table();

List<Play> l = ChessMoves.Move(t[1, 0]!, t);

foreach (var VARIABLE in l)
{
    Console.WriteLine(VARIABLE.PositionMove);
}

l[1].PlayGame();

List<Play> l1=ChessMoves.Move(t[0,0]!,t);


foreach (var VARIABLE in t[3,0]!.Positions)
{
    Console.WriteLine(VARIABLE+"**");
}
foreach (var VARIABLE in l1)
{
    Console.WriteLine(VARIABLE.PositionMove);
}