import time
import chess_game.game as game
import chess_game.strategy as strategy
import os

my_game: game.Game = game.Game(strategy.MTCSPlayer(), strategy.MiniMaxPlayer())

for i in my_game.run_game():
    os.system('clear')
    print(i)
    time.sleep(2)
