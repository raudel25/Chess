import time
import chess_game.game as game
import chess_game.strategy as strategy
import os
import pygame
from user_interface.board import Board
import chess


def select_player(color: str) -> strategy.Strategy:
    players = [lambda: strategy.HumanPlayer(), lambda: strategy.RandomPlayer(), lambda: strategy.GreedyPlayer(),
               lambda: strategy.MiniMaxPlayer(), lambda: strategy.MTCSPlayer()]

    while True:
        os.system('clear')
        print('Seleccione el jugador ' + color)
        print('0 para el jugador Humano')
        print('1 para el jugador Random')
        print('2 para el jugador Greedy')
        print('3 para el jugador MiniMax')
        print('4 para el jugador MCTS')

        select: int = int(input())
        if 0 > select or select > 4:
            os.system('clear')
            input('Seleccion incorrecta')
        else:
            return players[select]()


def best_str_board(board) -> str:
    str_board: str = str(board)
    s: str = '   '

    for i in range(8):
        aux = chr(ord('a') + i)
        s = f'{s}{aux} '
    s = f'{s}\n\n'

    for i in range(8):
        s = f'{s}{8 - i}  {str_board[i * 16:i * 16 + 15]}  {8 - i}\n'

    s = f'{s}\n   '

    for i in range(8):
        aux = chr(ord('a') + i)
        s = f'{s}{aux} '

    return s


my_game: game.Game = game.Game(select_player('blanco'), select_player('negro'))

WINDOW_SIZE = (600, 600)
screen = pygame.display.set_mode(WINDOW_SIZE)

board = Board(WINDOW_SIZE[0], WINDOW_SIZE[1])


def draw(display, board1):
    display.fill('white')
    board.draw(display, board1)
    pygame.display.update()


if __name__ == '__main__':
    for i in my_game.run_game():
        os.system('clear')
        # print(best_str_board(i))
        draw(screen, i)
        time.sleep(1)
