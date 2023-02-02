import pygame
import pygame_menu


class MenuToCrown:
    def __init__(self):
        self.select_piece_value = 'q'

    def select_to_crown(self, screen, WINDOW_SIZE):
        end_menu = False

        menu_to_crown = pygame_menu.Menu('To Crown', WINDOW_SIZE[0], WINDOW_SIZE[1],
                                         theme=pygame_menu.themes.THEME_DARK)

        menu_to_crown.add.label('Press space to play')
        menu_to_crown.add.selector(
            'Select Piece :', [('Queen', 'q'), ('Rock', 'r'), ('Knight', 'k'), ('Bishop', 'b')], onchange=self.select_piece)

        while not end_menu:
            events = pygame.event.get()
            for i in events:
                if i.type == pygame.KEYDOWN:
                    if i.key == pygame.K_SPACE:
                        end_menu = True
                        break

            menu_to_crown.update(events)
            menu_to_crown.draw(screen)

            pygame.display.update()

        return self.select_piece_value

    def select_piece(self, _, piece):
        self.select_piece_value = piece
