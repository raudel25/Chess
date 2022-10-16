import time
import chess_game.game as game
import chess_game.strategy as strategy
import os


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


my_game: game.Game = game.Game(select_player('blanco'), select_player('negro'))

for i in my_game.run_game():
    os.system('clear')
    print(i)
    time.sleep(1)
