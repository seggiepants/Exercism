import 'dart:math';

// Exercise: Flower Field
class FlowerField {
  // Put your code here.
  List<String> annotated = [];

  // For a given field of highly explosive and too traumatic to even mention mines -- er I mean "flowers".
  // fill empty spaces with the number of nearby ultraviolent -- flowers. Unless there are none nearby 
  // or if it is a mine in which case leave it as a space or asterisk (*).
  // Populates the class member variable "annotated" with the results.
  // @param List<String> grid: The play grid with the mines marked as "*" and blank spaces as " ". The rows must
  // all be the same length, and each string is a row in the grid.
  FlowerField(List<String> grid) {
    for(int y = 0; y < grid.length;y++) {
      String row = "";
      for(int x = 0; x < grid[y].length;x++) {
        if (grid[y][x] == "*") {
          row += "*";
        } else {
          int count = NeighborCount(grid, x, y);
          if (count == 0) {
            row += " ";
          } else {
            row += count.toString();
          }
        }
      }
      annotated.add(row);
    }
  }

  // Find the number of neighbors to a given location on the grid.
  // @param List<String> grid: The game board to be annotated "*" is a bomb, and " " is an empty space.
  // @param int x: column in the grid position to count neighbors from.
  // @param int y: row in the grid position to count neighbors from.
  // @returns int: The number of neighbors. Should be a number between 0 and 8.
  int NeighborCount(List<String> grid, int x, int y) {
    int maxY = grid.length;    
    int maxX = grid.map((row) => row.length).reduce(max);    
    int neighbors = 0;

    for(int j = y - 1; j < y + 2; j++) {
      if (j < 0 || j >= maxY) {
        continue;
      }
      for (int i = x - 1; i < x + 2; i++) {
        if (i == x && j == y) {
          continue;
        }
        if (i < 0 || i >= maxX) {
          continue;          
        }
        if (grid[j][i] == "*") {
          neighbors++;
        }
      }
    }
    
    return neighbors;
  }
}
