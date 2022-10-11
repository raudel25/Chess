using ChessLogic;

namespace ChessGame;

public class MctsPlayer:IStrategy
{
    private TreeGame _tree;

    public MctsPlayer(Table table)
    {
        this._tree = new TreeGame(0);
    }
    public Play Move(Color color, Table table)
    {
        return ChessMoves.PossibleMoves(color, table)[0];
    }

    // public (int,int) MtcsSimulation(TreeGame game,Color myColor,Color colorSimulation, TableCopy table)
    // {
    //     
    // }
}