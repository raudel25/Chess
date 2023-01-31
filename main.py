import time
from chess_game.player import Player
import chess_game.strategy as strategy
import pygame
from user_interface.board import Board
import chess
import pygame_menu


def select_player(ind) -> strategy.Strategy:
    players = [lambda: strategy.HumanPlayer(), lambda: strategy.RandomPlayer(), lambda: strategy.GreedyPlayer(),
               lambda: strategy.MiniMaxPlayer(), lambda: strategy.MTCSPlayer()]

    return players[ind]()


def draw(display, board, board_ui):
    display.fill('white')
    board_ui.draw(display, board)
    pygame.display.update()


def human_player(board, board_ui, screen):
    while True:
        board_ui.turn_human = True
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                return True
            if event.type == pygame.MOUSEBUTTONDOWN:
                if event.button == 1:
                    mx, my = pygame.mouse.get_pos()
                    board_ui.handle_click(mx, my, board)

        if board_ui.move != '':
            board.push(chess.Move.from_uci(board_ui.move))
            board_ui.turn_human = False
            board_ui.move = ''
            board_ui.selected_square = (-1, -1)

        draw(screen, board, board_ui)

        return False


pygame.init()
WINDOW_SIZE = (600, 600)
screen = pygame.display.set_mode(WINDOW_SIZE)
player_white_id = 0
player_black_id = 0


def select_player_w(value, ind):
    global player_white_id
    player_white_id = ind


def select_player_b(value, ind):
    global player_black_id
    player_black_id = ind


def play():
    player_white = Player(select_player(player_white_id))
    player_black = Player(select_player(player_black_id))
   
    board = chess.Board()
    board_ui = Board(WINDOW_SIZE[0], WINDOW_SIZE[1])

    draw(screen, board, board_ui)

    end = False
    while not board.is_game_over(claim_draw=True):
        player_game = player_white if board.turn else player_black

        if isinstance(player_game.strategy, strategy.HumanPlayer):
            end = human_player(board, board_ui, screen)
        else:
            board.push(player_game.play(board))

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                end = True
        if end:
            break

        draw(screen, board, board_ui)
        time.sleep(1)

    if not end:
        if board.is_checkmate():
            print('Las ' + ('blancas' if not board.turn else 'negras') + ' han ganado')
        else:
            print('Tablas')

menu = pygame_menu.Menu('Chess', WINDOW_SIZE[0], WINDOW_SIZE[0],
                        theme=pygame_menu.themes.THEME_DARK)

menu.add.selector(
    'Player White :', [('Human', 0), ('Random', 1), ('Greedy', 2), ('MiniMax', 3), ('MTCS', 4)], onchange=select_player_w)
menu.add.selector(
    'Player Black :', [('Human', 0), ('Random', 1), ('Greedy', 2), ('MiniMax', 3), ('MTCS', 4)], onchange=select_player_b)
menu.add.button('Play', play)
menu.add.button('Quit', pygame_menu.events.EXIT)
menu.mainloop(screen)
