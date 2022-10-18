import chess
import random


def sorted_moves(board: chess.Board, white: bool, is_maximizing: bool = True) -> list:
    possible = []

    for move in board.legal_moves:
        board.push(move)
        possible.append((move, evaluate_game(board, white)))
        board.pop()

    possible.sort(key=lambda x: x[1], reverse=is_maximizing)

    return possible


def evaluate_game(board: chess.Board, white: bool) -> int:
    score: int = random.randint(0, 1)

    for (piece, value, positions) in [(chess.PAWN, 100, position_pawns), (chess.BISHOP, 330, position_bishops),
                                      (chess.KNIGHT, 320, position_knights), (chess.ROOK, 500, position_rocks),
                                      (chess.QUEEN, 900, position_queens), (chess.KING, 20000, position_kings)]:
        pieces = board.pieces(piece, white)
        score += len(pieces) * value
        score += position_determinate(pieces, positions, white)

        pieces = board.pieces(piece, not white)
        score -= len(board.pieces(piece, not white)) * value
        score -= position_determinate(pieces, positions, not white)

    if board.turn == chess.BLACK and white and board.is_checkmate():
        score += 100000
    if board.turn == chess.WHITE and white and board.is_checkmate():
        score -= 100000

    return score


def position_determinate(pieces, position: list, white: bool):
    score: int = 0

    for i in pieces:
        score += position[i] if not white else position[len(position) - 1 - i]

    return score


position_pawns = [0, 0, 0, 0, 0, 0, 0, 0,
                  50, 50, 50, 50, 50, 50, 50, 50,
                  10, 10, 20, 45, 35, 20, 10, 10,
                  5, 5, 10, 42, 42, 10, 5, 5,
                  0, 0, 0, 40, 40, 0, 0, 0,
                  5, -5, -10, 0, 0, -10, -5, 5,
                  5, 10, 10, -50, -50, 10, 10, 5,
                  0, 0, 0, 0, 0, 0, 0, 0]

position_knights = [-50, -40, -30, -30, -30, -30, -40, -50,
                    -40, -20, 0, 0, 0, 0, -20, -40,
                    -30, 0, 10, 15, 15, 10, 0, -30,
                    -30, 5, 15, 20, 20, 15, 5, -30,
                    -30, 0, 15, 20, 20, 15, 0, -30,
                    -30, 5, 10, 15, 15, 10, 5, -30,
                    -40, -20, 0, 5, 5, 0, -20, -40,
                    -50, -40, -30, -30, -30, -30, -40, -50, ]

position_bishops = [-20, -10, -10, -10, -10, -10, -10, -20,
                    -10, 0, 0, 0, 0, 0, 0, -10,
                    -10, 0, 5, 10, 10, 5, 0, -10,
                    -10, 5, 5, 10, 10, 5, 5, -10,
                    -10, 0, 10, 10, 10, 10, 0, -10,
                    -10, 10, 10, 10, 10, 10, 10, -10,
                    -10, 5, 0, 0, 0, 0, 5, -10,
                    -20, -10, -10, -10, -10, -10, -10, -20, ]

position_rocks = [0, 0, 0, 0, 0, 0, 0, 0,
                  5, 10, 10, 10, 10, 10, 10, 5,
                  -5, 0, 0, 0, 0, 0, 0, -5,
                  -5, 0, 0, 0, 0, 0, 0, -5,
                  -5, 0, 0, 0, 0, 0, 0, -5,
                  -5, 0, 0, 0, 0, 0, 0, -5,
                  -5, 0, 0, 0, 0, 0, 0, -5,
                  0, 0, 0, 5, 5, 0, 0, 0]

position_queens = [-20, -10, -10, -5, -5, -10, -10, -20,
                   -10, 0, 0, 0, 0, 0, 0, -10,
                   -10, 0, 5, 5, 5, 5, 0, -10,
                   -5, 0, 5, 5, 5, 5, 0, -5,
                   0, 0, 5, 5, 5, 5, 0, -5,
                   -10, 5, 5, 5, 5, 5, 0, -10,
                   -10, 0, 5, 0, 0, 0, 0, -10,
                   -20, -10, -10, -5, -5, -10, -10, -20]

position_kings = [-30, -40, -40, -50, -50, -40, -40, -30,
                  -30, -40, -40, -50, -50, -40, -40, -30,
                  -30, -40, -40, -50, -50, -40, -40, -30,
                  -30, -40, -40, -50, -50, -40, -40, -30,
                  -20, -30, -30, -40, -40, -30, -30, -20,
                  -10, -20, -20, -20, -20, -20, -20, -10,
                  20, 20, 0, 0, 0, 0, 20, 20,
                  20, 30, 10, 0, 0, 10, 30, 20]
