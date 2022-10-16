import chess
import random
from .mcts import MonteCarloTreeSearch
from .evaluate_state import sorted_moves
from .mini_max import mini_max
from abc import ABC, abstractmethod


class Strategy(ABC):

    @abstractmethod
    def move(self, board: chess.Board) -> chess.Move:
        pass


class RandomPlayer(Strategy):
    def move(self, board: chess.Board) -> chess.Move:
        possible: list = list(board.legal_moves)

        return possible[random.randint(0, len(possible) - 1)]


class GreedyPlayer(Strategy):
    def move(self, board: chess.Board) -> chess.Move:
        possible: list = sorted_moves(board, board.turn)

        return possible[0][0]


class HumanPlayer(Strategy):
    def move(self, board: chess.Board) -> chess.Move:
        move: str = input('Intruduzca su jugada: ')

        while not chess.Move.from_uci(move) in board.legal_moves:
            print('Jugada invalida')
            move: str = input('Intruduzca su jugada: ')

        return chess.Move.from_uci(move)


class MiniMaxPlayer(Strategy):
    def move(self, board: chess.Board) -> chess.Move:
        return mini_max(board, 4, -1000000, 1000000, True, board.turn)[1]


class MTCSPlayer(Strategy):
    def move(self, board: chess.Board) -> chess.Move:
        t = MonteCarloTreeSearch(board)
        return t.best_action()
