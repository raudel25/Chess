from .strategy import Strategy
import chess


class Player:
    def __init__(self, my_strategy: Strategy, white: bool):
        self.__strategy = my_strategy
        self.white = white

    def play(self, board) -> chess.Move:
        return self.__strategy.move(board, self.white)
