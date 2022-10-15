from .player import Player
from .strategy import Strategy
import chess


class Game:
    def __init__(self, strategy_white: Strategy, strategy_black: Strategy):
        self.__player_white: Player = Player(strategy_white)
        self.__player_black: Player = Player(strategy_black)

    def run_game(self):
        board: chess.Board = chess.Board()

        yield board

        while not board.is_game_over(claim_draw=True):
            player_game = self.__player_white if board.turn else self.__player_black

            board.push(player_game.play(board))

            yield str(board)

        if board.is_checkmate():
            yield 'Las ' + ('blancas' if not board.turn else 'negras') + ' han ganado'
        else:
            yield 'Tablas'
