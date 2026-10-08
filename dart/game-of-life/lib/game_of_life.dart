// Exercise: Game of Life

// Runs the Game of Life simulation.
class GameOfLife {
  late List<List<int>> board;

  // Constructor
  // @param List<List<int>> input: Initial state of the game of life board.
  GameOfLife(List<List<int>> input) {
    this.board = input;
  }

  // Return the current state of the board.
  // @returns List<List<int>>: The current state of the board. Please don't modify.
  List<List<int>> matrix() {
    return this.board;
  }

  // Update the simulation by one step.
  void tick() {
    List<List<int>> next = [];

    // Initialize the next state.
    for(int j = 0; j < this.board.length; j++) {
      List<int> row = [];
      for(int i = 0; i < this.board[j].length; i++) {
        row.add(0);
      }
      next.add(row);
    }

    // Calculate living cells in the next state
    for (int j = 0; j < this.board.length; j++) {
      for(int i = 0; i < this.board[j].length; i++) {
        int neighbors = this.neighborCount(i, j);
        if (this.board[j][i] != 0 && (neighbors == 2 || neighbors == 3)) {
          next[j][i] = 1;
        } else if (this.board[j][i] == 0 && neighbors == 3) {
          next[j][i] = 1;
        } // all others already initialized to zero.  
      }
    }

    // Next state becomes current state.
    for(int j = 0; j < this.board.length; j++) {
      for(int i = 0; i < this.board[j].length; i++) {
        this.board[j][i] = next[j][i];
      }
    }
  }

  // Get the number of neighbors for a given cell on the board.
  // taking care not to count out of bounds or to count ones self.
  // Assumes the board is filled with only 0 or 1.
  // @param int x: x-coordinate of the point to count neighbors from
  // @param int y: y-coordinate of the point to count neighbors from.
  // @returns int: The number of neighbors.
  int neighborCount(int x, int y) {
    int maxY = this.board.length;
    if (y < 0 || y >= maxY) {
      return 0;
    }
    int maxX = this.board[y].length;
    if (x < 0 || x >= maxX) {
      return 0;
    }
    int result = 0;
    for(int j = y - 1; j <= y + 1; j++) {
      if (j >= 0 && j < maxY) {
        for(int i = x - 1; i <= x + 1; i++) {
          if (i >= 0 && i < maxX) {
            result += this.board[j][i];
          }
        }
      }
    }
    return result - this.board[y][x];
  }
}
