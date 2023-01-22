from .strategy import Strategy
import chess


class Player:
    def __init__(self, my_strategy: Strategy):
        self.strategy = my_strategy

    def play(self, board) -> chess.Move:
        """
        Jugada
        :param board: tablero
        :return: jugada
        """
        return self.strategy.move(board)
