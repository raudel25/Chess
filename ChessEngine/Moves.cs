namespace ChessEngine;

public class Moves
{
    /// <summary>
    /// Movimiento
    /// </summary>
    private readonly int[] _validMove;

    /// <summary>
    /// Movimiento de captura
    /// </summary>
    private readonly int[] _validCapture;

    /// <summary>
    /// Determina si solo se puede avanzar una casilla
    /// </summary>
    private readonly bool _oneMove;

    /// <summary>
    /// Color
    /// </summary>
    private readonly Color _color;

    /// <summary>
    /// Array de direccion
    /// </summary>
    private static readonly (int, int)[] Direction =
    {
        //Direction
        (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1),
        //Horse Direction
        (-1, -2), (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2)
    };

    public Moves(int[] validMove, int[] validCapture, Color color, bool oneMove = false)
    {
        this._validMove = validMove;
        this._validCapture = validCapture;
        this._oneMove = oneMove;
        this._color = color;
    }

    /// <summary>
    /// Determina el movimieto de una pieza
    /// </summary>
    /// <param name="positionCurrent">Posicion actual</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> Move((int, int) positionCurrent, Table table)
    {
        List<(int, int)> possible = new List<(int, int)>();

        foreach (var item in this._validMove)
        {
            (int, int) position = SumPosition(positionCurrent, Direction[item]);
            
            while (DecideMove(table, position, possible))
            {
                position = SumPosition(position, Direction[item]);
                if(this._oneMove) break;
            }
        }

        return possible;
    }

    /// <summary>
    /// Determina el movimieto de captura de una pieza
    /// </summary>
    /// <param name="positionCurrent">Posicion actual</param>
    /// <param name="table">Tablero</param>
    /// <returns>Lista de posibles casillas</returns>
    public List<(int, int)> MoveCapture((int, int) positionCurrent, Table table)
    {
        List<(int, int)> possible = new List<(int, int)>();

        foreach (var item in this._validCapture)
        {
            (int, int) position = SumPosition(positionCurrent, Direction[item]);
            
            while (DecideToCapture(table, position, possible))
            {
                position = SumPosition(position, Direction[item]);
                if(this._oneMove) break;
            }
        }

        return possible;
    }

    /// <summary>
    /// Determina si es coorecto el movimiento
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="position">Posicion</param>
    /// <param name="possible">Lista de posibles casillas</param>
    /// <returns>Lista de posibles casillas</returns>
    private bool DecideMove(Table table, (int, int) position, List<(int, int)> possible)
    {
        if (!CorrectMove(position)) return false;

        if (table[position.Item1, position.Item2] is null)
        {
            possible.Add(position);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Determina si es coorecto el movimiento de captura
    /// </summary>
    /// <param name="table">Tablero</param>
    /// <param name="position">Posicion</param>
    /// <param name="possible">Lista de posibles casillas</param>
    /// <returns>Lista de posibles casillas</returns>
    private bool DecideToCapture(Table table, (int, int) position, List<(int, int)> possible)
    {
        if (!CorrectMove(position)) return false;

        if (table[position.Item1, position.Item2] is null) return true;

        if (table[position.Item1, position.Item2]!.Color != this._color)
        {
            possible.Add(position);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Determina que la casilla no este fuera del tablero
    /// </summary>
    /// <param name="move">Casilla</param>
    /// <returns>Determina que la casilla no este fuera del tablero</returns>
    public static bool CorrectMove((int, int) move)
    {
        if (move.Item1 < 0 || move.Item2 < 0) return false;
        if (move.Item1 >= 8 || move.Item2 >= 8) return false;

        return true;
    }

    /// <summary>
    /// Determina en una aasilla el coorespondiente incremento del array de movimiento
    /// </summary>
    /// <param name="a">Casilla</param>
    /// <param name="b">Incremento</param>
    /// <returns>Nueva casilla</returns>
    public static (int, int) SumPosition((int, int) a, (int, int) b) => (a.Item1 + b.Item1, a.Item2 + b.Item2);
}