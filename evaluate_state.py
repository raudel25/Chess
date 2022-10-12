import chess


def evaluate_valor(board: chess.Board, white: bool):
    score: int = 0

    for (piece, value) in [(chess.PAWN, 1), (chess.BISHOP, 3),
                           (chess.KNIGHT, 3), (chess.ROOK, 5),
                           (chess.QUEEN, 9), (chess.KING, 100)]:
        score += len(board.pieces(piece, white)) * value
        score -= len(board.pieces(piece, not white)) * value

    return score
