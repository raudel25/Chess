# ChessPython

Este proyecto tiene como objetivo la implementación de un motor de ajedrez en python, con la interacción de jugadores y jugadores humanos. La interfaz gráfica está desarrollada en **pygame**.

### Dependencias

Para ejecutar el proyecto debe tener instaladas las bibliotecas: <a href="https://python-chess.readthedocs.io/en/latest/">chess</a>, <a href="https://numpy.org/">numpy</a>, <a href="https://www.pygame.org/news">pygame</a> y el cliente de <a href="https://pypi.org/project/stockfish/">stockfish para python</a>. Adicionalmente debe contar con <a href="https://stockfishchess.org/">stockfish</a> instalado en su pc, puede seguir la guía especificada en la documentación del cliente de <a href="https://pypi.org/project/stockfish/">stockfish para python</a>, la configuración de stockfish de este proyecto se encuentra de `chess_game\strategy.py` en la clase `StockfishStrategy`. Para ejecutar el proyecto debe ejecutar:

```bash
make
```
para los usuarios de Linux o correr el scrip `main.py` con su interprete de python.

### Jugadores

- **HumanPlayer**: Cuenta con la interacción de un jugador humano, manejada desde la insterfaz gráfica.
- **RandomPlayer**: De todos los movimientos válidos en el momento del juego selecciona uno aleatorio y realiza la jugada.
- **GreedyPlayer**: De todos los movimientos válidos en el momento del juego selecciona el que más peso tiene según la 
función de evaluación `evaluate_game`.
- **MiniMaxPlayer**: Cuenta con la implementación del algoritmo <a href="https://en.wikipedia.org/wiki/Alpha%E2%80%93beta_pruning">MiniMax con poda alfa-beta</a>, apoyandose en la función
de evaluación `evaluate_game`.
- **MTCSPlayer**: Cuenta con la implementación del algoritmo <a href="https://en.wikipedia.org/wiki/Monte_Carlo_tree_search">Monte Carlo tree search</a>.
- **Stockfish**: El famoso motor de ajedrez.

### Estrategias

Para implementar un nuevo jugador debe crear una clase que herede de `Estrategy` e implmentar el método `move`:
```python
def move(self, board: chess.Board) -> chess.Move:
```
con el respectivo algoritmo de su jugador.

### Función de evaluación

El método `evaluate_game` se encarga de determinar el valor heurístico que se le confiere a una determinada posición,
para ello se tiene en cuenta el valor de cada pieza y el peso que tiene la casillas donde se encuentra ubicada, según el
siguiente <a href="https://www.chessprogramming.org/Simplified_Evaluation_Function">algoritmo heurístico</a>. Para futuras implementaciones se desea mejorar está función evaluadora para que el motor sea más
preciso.
