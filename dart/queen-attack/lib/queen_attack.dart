// Exercise: Queen Attack
class Queen {
  // Put your code here
  late int x; 
  late int y;

  // Create a new queen at location col, row
  // @param int row: Row of the board to occupy must be integer 0-7
  // @param int col: Column of the board to occupy must be integer 0-7
  // @raises Assertion Error: If row and/or col outside of 0-7.
  Queen(int row, int col) {
    assert(row >= 0, 'row not positive');
    assert(col >= 0, 'column not positive');
    assert(row < 8, 'row not on board');
    assert(col < 8, 'column not on board');

    this.y = row;
    this.x = col;
  }

  // Check if this queen can attack another (Horizontally, Vertically, or Diagonally)
  // @param Queen other: The queen to check this one against
  // @returns bool: True if this queen can attack the other one.
  bool canAttack(Queen other) {
    assert(!(other.x == this.x && other.y == this.y));
    // shared horizontal
    if (this.y == other.y) {
      return true;
    }
    // shared vertical
    if (this.x == other.x) {
      return true;
    }

    // Shared diagonal
    int dx = (this.x - other.x).abs();
    int dy = (this.y - other.y).abs();
    if (dx == dy) {
      return true;
    }

    return false;
  }
}
