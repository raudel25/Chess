import time
from chess_game.player import Player
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


def draw(display, board, board_ui):
    display.fill('white')
    board_ui.draw(display, board)
    pygame.display.update()


def human_player(board, board_ui, screen):
    while True:
        board_ui.turn_human = True
        mx, my = pygame.mouse.get_pos()
        for event in pygame.event.get():
            if event.type == pygame.MOUSEBUTTONDOWN:
                # If the mouse is clicked
                if event.button == 1:
                    board_ui.handle_click(mx, my, board)

        if board_ui.move != '':
            board.push(chess.Move.from_uci(board_ui.move))
            board_ui.turn_human = False
            board_ui.move = ''
            board_ui.selected_square = (-1, -1)
            break

        draw(screen, board, board_ui)


player_white = Player(select_player('blanco'))
player_black = Player(select_player('negro'))
board = chess.Board()

WINDOW_SIZE = (600, 600)
screen = pygame.display.set_mode(WINDOW_SIZE)

board_ui = Board(WINDOW_SIZE[0], WINDOW_SIZE[1])

draw(screen, board, board_ui)

running = True
while running:
    player_game = player_white if board.turn else player_black

    if isinstance(player_game.strategy, strategy.HumanPlayer):
        human_player(board, board_ui, screen)
    else:
        board.push(player_game.play(board))

    for event in pygame.event.get():
        # Quit the game if the user presses the close button
        if event.type == pygame.QUIT:
            running = False

    draw(screen, board, board_ui)
    time.sleep(1)
    running = not board.is_game_over(claim_draw=True)

if board.is_checkmate():
    print('Las ' + ('blancas' if not board.turn else 'negras') + ' han ganado')
else:
    print('Tablas')
