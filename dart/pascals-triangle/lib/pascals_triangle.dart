// Exercise - Pascal's Triangle
class PascalsTriangle {
  // Put your code here

  // Return rows 1 to cutOff of Pascal's triangle.
  // @param int cutOff: The row to stop on. Should be >= 1.
  // @returns List<List<int>>: A 2D ragged list of rows in the triangle (no padding/centering all left-aligned).
  List<List<int>> rows<T>(int cutOff) {
    List<List<int>> result = [];
    for(int i = 0; i < cutOff; i++) {
      if (i == 0) {
        result.add([1]);
        continue;
      }
      List<int> row = [];
      for(int j = -1; j < result[i - 1].length; j++) {
        row.add(
          (j < 0 ? 0 : result[i - 1][j]) + 
          (j + 1 >= result[i - 1].length ? 0 : result[i - 1][j + 1] )
          );
      }
      result.add(row);
    }
    return result;
  }
}
