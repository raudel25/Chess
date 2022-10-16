# ChessPython

Este proyecto tiene como objetivo la implementación de un motor de ajedrez en python, con la interacción de jugadores y jugadores humanos.

### Dependencias

Para ejecutar el proyecto debe tener instaladas las bibliotecas: `chess` y `numpy`. Para ejecutar el proyecto debe ejecutar:

```bash
    make
```
para los usuarios de Linux o correr el scrip `main.py` con su interprete de python.

### Jugadores

- **HumanPlayer**: Cuenta con la interacción de un jugador humano, para manejar este debe introducir la casilla
donde se encuetra la pieza que desee mover y la casilla hacia donde la quiere mover, ejemp: `e2e4` mueve el peón rey 2 filas 
hacia delante
- **RandomPlayer**: De todos los movimientos válidos en el momento del juego selecciona uno aleatorio y realiza la jugada
- **GreedyPlayer**: De todos los movimientos válidos en el momento del juego selecciona el que más peso tiene según la 
función de evaluación `evaluate_game`.
- **MiniMaxPlayer**: Cuenta con la implementación del algoritmo <a href="https://en.wikipedia.org/wiki/Alpha%E2%80%93beta_pruning">MiniMax con poda alfa-beta</a>, apoyandose en la funciṕn
de evaluación `evaluate_game`.
- **MTCSPlayer**: Cuenta con la implementación del algoritmo <a href="https://en.wikipedia.org/wiki/Monte_Carlo_tree_search">Monte Carlo tree search</a>.

### Estrategias

Para implementar un nuevo jugador debe crear una clase que erede de `Estrategy` e implmentar el método `move`:
```python
    def move(self, board: chess.Board) -> chess.Move:
```
con el respectivo algoritmo de su jugador.

### Función de evaluación

El método `evaluate_game` se encarga de determinar el valor heurístico que se le confiere a una determinada posición,
para ello se tiene en cuenta el valor de cada pieza y el peso que tiene la casillas donde se encuentra ubicada, según el
siguiente <a href="https://www.chessprogramming.org/Simplified_Evaluation_Function">algoritmo heurístico</a>. Para futuras implementaciones se desea mejorar está función evaluadora para que el motor sea más
preciso.