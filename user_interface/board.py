import pygame
import chess


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
        self.selected_piece = None
        self.turn = 'white'
        self.squares = self.generate_squares()
        self.images = self.generate_images()

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

    def draw(self, display, board: chess.Board):
        board_str = str(board)

        for i in range(0, len(board_str), 2):
            if board_str[i] == '.':
                self.get_square_from_pos(
                    (i//2 % 8, (i//2)//8)).piece_image = None
            else:
                self.get_square_from_pos(
                    (i//2 % 8, (i//2)//8)).piece_image = self.images[board_str[i]]
        # if self.selected_piece is not None:
        #     self.get_square_from_pos(self.selected_piece.pos).highlight = True
        #     for square in self.selected_piece.get_valid_moves(self):
        #         square.highlight = True
        for square in self.squares:
            square.draw(display)
