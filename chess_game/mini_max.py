from .evaluate_state import evaluate_game
import chess


def max_player(board: chess.Board, depth: int, alpha: int, beta: int, my_color: bool) -> tuple:
    """
    Jugador max
    :param board: tablero
    :param depth: profundidad
    :param alpha: alfa
    :param beta: beta
    :param my_color: color del jugador
    :return: mejor jugada de max
    """
    if depth == 0:
        return evaluate_game(board, my_color), None

    score: int = -1000000
    best_board = None

    for move in board.legal_moves:
        board.push(move)

        valor: tuple = min_player(board, depth - 1, alpha, beta, my_color)
        if valor[0] > score:
            (score, best_board) = (valor[0], board.copy())

        board.pop()

        alpha = max(alpha, score)

        if alpha >= beta:
            return score, best_board

    return score, best_board


def min_player(board: chess.Board, depth: int, alpha: int, beta: int, my_color: bool) -> tuple:
    """
    Jugador min
    :param board: tablero
    :param depth: profundidad
    :param alpha: alfa
    :param beta: beta
    :param my_color: color del jugador
    :return: mejor jugada de min
    """
    if depth == 0:
        return evaluate_game(board, my_color), None

    score: int = 1000000
    best_board = None

    for move in board.legal_moves:
        board.push(move)

        valor: tuple = max_player(board, depth - 1, alpha, beta, my_color)

        if valor[0] < score:
            (score, best_board) = (valor[0], board.copy())

        board.pop()

        beta = min(beta, score)

        if alpha >= beta:
            return score, best_board

    return score, best_board
