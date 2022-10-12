from player import Player
from strategy import Strategy
import chess


class Game:
    def __init__(self, strategy_white: Strategy, strategy_black: Strategy):
        self._player_white: Player = Player(strategy_white, True)
        self._player_black: Player = Player(strategy_black, False)

    def run_game(self):
        turn: bool = True
        board: chess.Board = chess.Board()

        yield board

        while not board.is_game_over(claim_draw=True):
            player_game = self._player_white if turn else self._player_black

            board.push(player_game.play(board))

            turn = not turn

            yield board

        if board.is_checkmate():
            yield 'Las ' + ('blancas' if not turn else 'negras') + ' han ganado'
        else:
            yield 'Tablas'
