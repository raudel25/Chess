using ChessLogic;

namespace ChessGame;

public class Game
{
    /// <summary>
    /// Estado del juego
    /// </summary>
    private enum GameState
    {
        JakeMate,
        Draw,
        ContinueGame
    }

    /// <summary>
    /// Tablero
    /// </summary>
    private readonly Table _table;

    /// <summary>
    /// Jugador blanco
    /// </summary>
    private readonly Player _playerWhite;
    
    /// <summary>
    /// Jugador negro
    /// </summary>
    private readonly Player _playerBlack;

    public Game(IStrategy strategyWhite, IStrategy strategyBlack)
    {
        this._table = new Table();
        this._playerWhite = new Player(Color.White, strategyWhite);
        this._playerBlack = new Player(Color.Black, strategyBlack);
    }

    public IEnumerable<string[,]> RunGame()
    {
        Color colorAct = Color.White;
        int cantTurns = 0;
        GameState state = GameState.ContinueGame;

        while (true)
        {
            yield return Printer.Table(_table);
            
            state = CheckFinalGame(colorAct);
            if (state != GameState.ContinueGame) break;

            if (colorAct == Color.White) _playerWhite.PlayPlayer(_table);
            else _playerBlack.PlayPlayer(_table);

            CheckCorrectGame(cantTurns, colorAct);

            colorAct = colorAct == Color.White ? Color.Black : Color.White;
            cantTurns++;
        }
    }

    /// <summary>
    /// Chequea el correcto funcionamiento del juego
    /// </summary>
    /// <param name="cantTurns">Cantidad de turnos segun el juez</param>
    /// <param name="color">Color</param>
    /// <exception cref="Exception">Movimiento invalido</exception>
    private void CheckCorrectGame(int cantTurns, Color color)
    {
        if (_table.CantTurns != cantTurns + 1) throw new Exception("Invalid move");
        if (_table.Turn == color) throw new Exception("Invalid move");
    }

    /// <summary>
    /// Determina el final del juego
    /// </summary>
    /// <param name="color">Color</param>
    /// <returns>Estado del juego</returns>
    private GameState CheckFinalGame(Color color)
    {
        if (ChessRules.JakeMate(color, _table)) return GameState.JakeMate;
        if (ChessRules.Draw(color, _table)) return GameState.Draw;

        return GameState.ContinueGame;
    }
}