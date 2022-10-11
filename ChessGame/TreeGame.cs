using ChessLogic;

namespace ChessGame;

public class TreeGame
{
    public int Valor { get; private set; }

    public int Count => this._children.Count;
    
    public int Victory { get; set; }
    
    public int Games { get; set; }

    public TreeGame this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count) throw new IndexOutOfRangeException();
            return this._children[index];
        }
    }

    private readonly List<TreeGame> _children;

    public TreeGame(int valor)
    {
        this.Valor = valor;
        this._children = new List<TreeGame>();
        
    }

    public void Add(TreeGame tree)
    {
        this._children.Add(tree);
    }

    public bool FindChild(int index)
    {
        foreach (var item in _children)
        {
            if (item.Valor == index) return true;
        }

        return false;
    }
}