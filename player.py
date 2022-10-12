from strategy import Strategy


class Player:
    def __init__(self, my_strategy: Strategy, white: bool):
        self._strategy = my_strategy
        self.white = white

    def play(self, board):
        return self._strategy.move(board, self.white)
