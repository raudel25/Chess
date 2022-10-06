using ChessLogic;

namespace ChessGame;

public interface IStrategy
{
    /// <summary>
    /// Determina el movimiento a realizar por el jugador
    /// </summary>
    /// <param name="color">Color del jugador</param>
    /// <param name="table">Tablero</param>
    /// <returns>Movimiento</returns>
    public Play Move(Color color, Table table);
}