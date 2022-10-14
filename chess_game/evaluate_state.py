import chess


class EvaluateState:
    @staticmethod
    def evaluate_valor(board: chess.Board, white: bool):
        score: int = 0

        for (piece, value) in [(chess.PAWN, 1), (chess.BISHOP, 3),
                               (chess.KNIGHT, 3), (chess.ROOK, 5),
                               (chess.QUEEN, 9), (chess.KING, 0)]:
            score += len(board.pieces(piece, white)) * value
            score -= len(board.pieces(piece, not white)) * value

        if board.turn == chess.BLACK and white and board.is_checkmate():
            score += 1000
        if board.turn == chess.WHITE and white and board.is_checkmate():
            score -= 1000

        return score
