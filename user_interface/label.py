import pygame


class Label:
    def __init__(self, screen, text, x, y, color='white'):
        self.font = pygame.font.SysFont('Arial', 20)
        self.image = self.font.render(text, 1, color)
        _, _, w, h = self.image.get_rect()
        self.rect = pygame.Rect(x, y, w, h)
        self.screen = screen

    def change_text(self, newtext, color="white"):
        self.image = self.font.render(newtext, 1, color)

    def change_font(self, font, size, color="white"):
        self.font = pygame.font.SysFont(font, size)
        self.change_text(self.text, color)

    def draw(self):
        self.screen.blit(self.image, (self.rect))
