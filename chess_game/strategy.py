import chess
import random
from .evaluate_state import evaluate_game, sorted_moves
from abc import ABC, abstractmethod


class Strategy(ABC):

    @abstractmethod
    def move(self, board: chess.Board, white: bool) -> chess.Move:
        pass


class RandomPlayer(Strategy):
    def move(self, board: chess.Board, white: bool) -> chess.Move:
        possible: list = list(board.legal_moves)

        return possible[random.randint(0, len(possible) - 1)]


class GreedyPlayer(Strategy):
    def move(self, board: chess.Board, white: bool) -> chess.Move:
        possible: list = sorted_moves(board, white)

        return possible[0][0]


class HumanPlayer(Strategy):
    def move(self, board: chess.Board, white: bool) -> chess.Move:
        move: str = input('Intruduzca su jugada: ')

        while not chess.Move.from_uci(move) in board.legal_moves:
            print('Jugada invalida')
            move: str = input('Intruduzca su jugada: ')

        return chess.Move.from_uci(move)


class MiniMaxPlayer(Strategy):
    def move(self, board: chess.Board, white: bool) -> chess.Move:
        return MiniMaxPlayer.mini_max(board, 4, -1000000, 1000000, True, white)[1]

    @staticmethod
    def mini_max(board: chess.Board, depth: int, alpha: int, beta: int, is_maximizing: bool, white: bool) -> tuple:
        if depth == 0:
            return evaluate_game(board, white), None

        score: int = -1000000 if is_maximizing else 1000000
        play = None

        for move in board.legal_moves:
            board.push(move)

            valor: tuple = MiniMaxPlayer.mini_max(board, depth - 1, alpha, beta, not is_maximizing, white)
            if is_maximizing:
                if valor[0] > score:
                    (score, play) = (valor[0], move)
            else:
                if valor[0] < score:
                    (score, play) = (valor[0], move)

            board.pop()

            if is_maximizing:
                alpha = max(alpha, score)
            else:
                beta = min(beta, score)
            if alpha >= beta:
                return score, play

        return score, play
