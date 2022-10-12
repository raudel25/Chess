import time
import strategy
import os
from game import Game

game: Game = Game(strategy.MiniMaxPlayer(), strategy.GreedyPlayer())

for i in game.run_game():
    os.system('clear')
    print(i)
    time.sleep(2)
