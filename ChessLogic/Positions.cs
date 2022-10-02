using System.Collections;

namespace ChessLogic;

public class Positions : IEnumerable<(int, int)>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<(int, int)> GetEnumerator()
    {
        foreach (var item in _positions)
        {
            yield return item;
        }
    }

    public (int, int) this[int index]
    {
        get
        {
            if (index < 0 || index > _positions.Count) throw new IndexOutOfRangeException();
            return _positions[index];
        }
    }

    /// <summary>
    /// Annadir una nueva posicion
    /// </summary>
    /// <param name="pos">Posicion para annadir</param>
    internal void Add((int, int) pos) => _positions.Add(pos);

    /// <summary>
    /// Determina la posicion actual
    /// </summary>
    /// <exception cref="Exception">La posicion inicial no se ha definido</exception>
    public (int, int) Current
    {
        get
        {
            if (_positions.Count == 0) throw new Exception("The initial position has not be defined");
            return _positions[_positions.Count - 1];
        }
    }

    public int Count => _positions.Count;

    private readonly List<(int, int)> _positions;

    internal Positions()
    {
        this._positions = new List<(int, int)>();
    }
    
    private Positions(List<(int,int)> positions)
    {
        this._positions = positions;
    }

    internal Positions Clone() => new Positions(_positions.ToList());
}