import chess
import numpy as np
from collections import defaultdict


class MonteCarloTreeSearch:

    def __init__(self, state: chess.Board, parent=None, parent_action: chess.Move = None):
        self.state: chess.Board = state
        self.parent: MonteCarloTreeSearch = parent
        self.parent_action: chess.Move = parent_action
        self.children: list = []
        self._number_of_visits: int = 0
        self._results = defaultdict(int)
        self._results[1] = 0
        self._results[-1] = 0
        self._untried_actions: list = self.untried_actions()

    def untried_actions(self) -> list:
        self._untried_actions = list(self.state.legal_moves)
        return self._untried_actions

    def q(self) -> int:
        wins = self._results[1]
        loses = self._results[-1]
        return wins - loses

    def n(self) -> int:
        return self._number_of_visits

    def expand(self):
        action = self._untried_actions.pop()
        self.state.push(action)

        child_node = MonteCarloTreeSearch(
            self.state.copy(), parent=self, parent_action=action)

        self.state.pop()

        self.children.append(child_node)
        return child_node

    def is_terminal_node(self) -> bool:
        return self.state.is_game_over()

    def rollout(self) -> int:
        current_rollout_state: chess.Board = self.state.copy()

        while not current_rollout_state.is_game_over():
            possible_moves = list(current_rollout_state.legal_moves)

            action: chess.Move = self.rollout_policy(possible_moves)
            current_rollout_state.push(action)

        if current_rollout_state.is_checkmate():
            return 1 if self.state.turn != current_rollout_state.turn else -1

        return 0

    @staticmethod
    def rollout_policy(possible_moves) -> chess.Move:
        return possible_moves[np.random.randint(len(possible_moves))]

    def backpropagation(self, result):
        self._number_of_visits += 1.
        self._results[result] += 1.
        if self.parent:
            self.parent.backpropagation(result)

    def is_fully_expanded(self) -> bool:
        return len(self._untried_actions) == 0

    def best_child(self, c_param=0.1):

        choices_weights = [(c.q() / c.n()) + c_param * np.sqrt((2 * np.log(self.n()) / c.n())) for c in self.children]
        return self.children[np.argmax(choices_weights)]

    def _tree_policy(self):

        current_node = self
        while not current_node.is_terminal_node():

            if not current_node.is_fully_expanded():
                return current_node.expand()
            else:
                current_node = current_node.best_child()
        return current_node

    def best_action(self) -> chess.Move:
        simulation_no = 100

        for i in range(simulation_no):
            v = self._tree_policy()
            reward = v.rollout()
            v.backpropagation(reward)

        return self.best_child(c_param=0.).parent_action
