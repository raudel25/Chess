using ChessLogic;

namespace ChessGame;

public class Player
{
    /// <summary>
    /// Color del jugador
    /// </summary>
    private readonly Color _color;

    /// <summary>
    /// Estrategia del jugador
    /// </summary>
    private readonly IStrategy _strategy;

    public Player(Color color, IStrategy strategy)
    {
        this._color = color;
        this._strategy = strategy;
    }

    public void PlayPlayer(Table table)
    {
        Play play = _strategy.Move(this._color, table);

        play.PlayGame();
    }
}