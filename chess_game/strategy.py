import chess
import random
from .mcts import MonteCarloTreeSearch
from .evaluate_state import sorted_moves
from .mini_max import max_player, min_player
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

        while not HumanPlayer.check_move(move, board):
            print('Jugada invalida')
            move: str = input('Intruduzca su jugada: ')

        return chess.Move.from_uci(move)

    @staticmethod
    def check_move(move: str, board: chess.Board) -> bool:
        if len(move) != 4:
            return False
        if move[0] < 'a' or move[0] > 'h' or move[2] < 'a' or move[2] > 'h':
            return False
        if move[1] < '1' or move[1] > '8' or move[3] < '1' or move[3] > '8':
            return False

        return chess.Move.from_uci(move) in board.legal_moves


class MiniMaxPlayer(Strategy):
    def move(self, board: chess.Board) -> chess.Move:
        best_moves: list = []
        my_color = board.turn
        possible: list = list(board.legal_moves)
        alpha = -1000000
        beta = 1000000

        for i in range(len(possible)):
            board.push(possible[i])
            aux: tuple = min_player(board, 3, alpha, beta, my_color)
            board.pop()

            alpha = max(alpha, aux[0])
            best_moves.append(aux)

        play: int = 0
        score: int = -1000000
        for i in range(len(best_moves)):
            valor: int = best_moves[i][0] if best_moves[i][1] is None else \
                max_player(best_moves[i][1], 2, -1000000, 1000000, best_moves[i][1].turn)[0]
            if valor > score:
                (score, play) = (valor, i)

        return possible[play]


class MTCSPlayer(Strategy):
    def move(self, board: chess.Board) -> chess.Move:
        t = MonteCarloTreeSearch(board)
        return t.best_action()
