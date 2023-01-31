import pygame
import chess
import os


class Square:
    def __init__(self, x, y, width, height):
        self.x = x
        self.y = y
        self.width = width
        self.height = height
        self.abs_x = x * width
        self.abs_y = y * height
        self.abs_pos = (self.abs_x, self.abs_y)
        self.pos = (x, y)
        self.draw_color = (
            220, 208, 194) if (x + y) % 2 == 0 else (53, 53, 53)
        self.highlight_color = (
            100, 249, 83) if (x + y) % 2 == 0 else (0, 228, 10)
        self.piece_image = None
        self.coord = self.get_coord()
        self.highlight = False
        self.rect = pygame.Rect(
            self.abs_x,
            self.abs_y,
            self.width,
            self.height
        )

    # get the formal notation of the tile
    def get_coord(self):
        columns = 'abcdefgh'
        return columns[self.x] + str(self.y + 1)

    def draw(self, display):
        # configures if tile should be light or dark or highlighted tile
        if self.highlight:
            pygame.draw.rect(display, self.highlight_color, self.rect)
        else:
            pygame.draw.rect(display, self.draw_color, self.rect)
        # adds the chess piece icons
        if self.piece_image != None:
            centering_rect = self.piece_image.get_rect()
            centering_rect.center = self.rect.center
            display.blit(self.piece_image, centering_rect.topleft)


class Board:
    def __init__(self, width, height):
        self.width = width
        self.height = height
        self.tile_width = width // 8
        self.tile_height = height // 8
        self.selected_square = (-1, -1)
        self.squares = self.generate_squares()
        self.images = self.generate_images()
        self.turn_human = False
        self.to_crown = False
        self.move = ''

    def generate_squares(self):
        output = []
        for y in range(8):
            for x in range(8):
                output.append(
                    Square(x,  y, self.tile_width, self.tile_height)
                )
        return output

    def get_square_from_pos(self, pos):
        for square in self.squares:
            if (square.x, square.y) == (pos[0], pos[1]):
                return square

    def generate_images(self):
        images = {}
        pieces = ['p', 'n', 'b', 'r', 'q', 'k']

        for i in pieces:
            img = pygame.image.load('user_interface/img/Chess_'+i+'lt60.png')
            images[i.upper()] = img
        for i in pieces:
            img = pygame.image.load('user_interface/img/Chess_'+i+'dt60.png')
            images[i] = img

        return images

    def handle_click(self, mx, my, board: chess.Board):
        x = mx // self.tile_width
        y = my // self.tile_height

        if (x, y) == self.selected_square:
            self.selected_square = (-1, -1)
        else:
            if self.turn_human:
                if self.selected_square == (-1, -1):
                    self.selected_square = (x, y)
                else:
                    move = f'{Board.coord_to_move(self.selected_square)}{Board.coord_to_move((x,y))}'

                    find = False
                    for i in board.legal_moves:
                        if str(i)[:4] == move:
                            find = True
                            if len(str(i)) == 5:
                                self.to_crown = True

                    if find:
                        self.move = move
                    else:
                        self.selected_square = (-1, -1)

    def draw(self, display, board: chess.Board):
        board_str = str(board)

        for i in range(0, len(board_str), 2):
            if board_str[i] == '.':
                self.get_square_from_pos(
                    (i//2 % 8, (i//2)//8)).piece_image = None
            else:
                self.get_square_from_pos(
                    (i//2 % 8, (i//2)//8)).piece_image = self.images[board_str[i]]

        for square in self.squares:
            square.highlight = False

        if self.selected_square != (-1, -1):
            for i in Board.legalmoves_to_coord(board, self.selected_square):
                self.get_square_from_pos(i).highlight = True

        for square in self.squares:
            square.draw(display)

    @staticmethod
    def move_to_coord(move):
        return ord(str(move)[2])-ord('a'), 8-int(str(move)[3])

    @staticmethod
    def coord_to_move(coord):
        x = chr(coord[0]+ord('a'))
        y = 8-coord[1]
        return f'{x}{y}'

    @staticmethod
    def legalmoves_to_coord(board, coord):
        coord = Board.coord_to_move(coord)
        legalmoves = []
        for i in board.legal_moves:
            if coord == str(i)[:2]:
                (x, y) = Board.move_to_coord(i)
                legalmoves.append((x, y))

        return legalmoves
