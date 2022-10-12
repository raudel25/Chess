import chess
import random
import evaluate_state
from abc import ABC, abstractmethod


class Strategy(ABC):

    @abstractmethod
    def move(self, board: chess.Board, white: bool):
        return


class RandomPlayer(Strategy):
    def move(self, board: chess.Board, white: bool):
        possible: list = list(board.legal_moves)

        return possible[random.randint(0, len(possible) - 1)]


class GreedyPlayer(Strategy):
    def move(self, board: chess.Board, white: bool):
        possible: list = []

        for move in board.legal_moves:
            board.push(move)
            possible.append((move, evaluate_state.evaluate_valor(board, white)))
            board.pop()

        possible.sort(key=lambda x: x[1], reverse=True)

        return possible[0][0]


class HumanPlayer(Strategy):
    def move(self, board: chess.Board, white: bool):
        move: str = input('Intruduzca su jugada: ')

        while not chess.Move.from_uci(move) in board.legal_moves:
            print('Jugada invalida')
            move: str = input('Intruduzca su jugada: ')

        return chess.Move.from_uci(move)


class MiniMaxPlayer(Strategy):
    def move(self, board: chess.Board, white: bool):
        score: int = -1000000
        play = None

        for move in board.legal_moves:
            board.push(move)

            value: int = self.best_move(board, 3, -1000000, 1000000, False, white)

            if value > score:
                (play, score) = (move, value)
            if value == score and (random.randint(0, 1) & 1) == 0:
                (play, score) = (move, value)

            board.pop()

        return play

    @staticmethod
    def best_move(board: chess.Board, depth: int, alpha: int, beta: int, is_maximizing: bool, white: bool) -> int:
        if depth == 0:
            return evaluate_state.evaluate_valor(board, white)

        score: int = -1000000 if is_maximizing else 1000000

        for move in board.legal_moves:
            board.push(move)

            valor: int = int(MiniMaxPlayer.best_move(board, depth - 1, alpha, beta, not is_maximizing, white))
            score = max(score, valor) if is_maximizing else min(score, valor)

            board.pop()

            if is_maximizing:
                alpha = max(alpha, score)
            else:
                beta = min(beta, score)
            if alpha >= beta:
                return score

        return score
