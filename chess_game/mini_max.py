from .evaluate_state import evaluate_game
import chess


def mini_max(board: chess.Board, depth: int, alpha: int, beta: int, is_maximizing: bool, my_color: bool) -> tuple:
    if depth == 0:
        return evaluate_game(board, my_color), None

    score: int = -1000000 if is_maximizing else 1000000
    play = None

    for move in board.legal_moves:
        board.push(move)

        valor: tuple = mini_max(board, depth - 1, alpha, beta, not is_maximizing, my_color)
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
