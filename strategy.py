import chess
import random
from abc import ABC, abstractmethod


class Strategy(ABC):

    @abstractmethod
    def move(self, board: chess.Board, white: bool):
        return


class RandomPlayer(Strategy):
    def move(self, board: chess.Board, white: bool):
        possible: list = list(board.legal_moves)

        return possible[random.randint(0, len(possible) - 1)]


class HumanPlayer(Strategy):
    def move(self, board: chess.Board, white: bool):

        move: str = input('Intruduzca su jugada: ')
        print(chess.Move.from_uci(move) in board.legal_moves)
        while not chess.Move.from_uci(move) in board.legal_moves:
            print('Jugada invalida')
            move: str = input('Intruduzca su jugada: ')

        return chess.Move.from_uci(move)
