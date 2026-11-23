import 'position.dart';
import 'orientation.dart';

// Exercise: Robot Simulator
class Robot {
  // Put your code here
  final Map<Orientation, Orientation> Left = { Orientation.north: Orientation.west, Orientation.west: Orientation.south, Orientation.south: Orientation.east, Orientation.east: Orientation.north};
  final Map<Orientation, Orientation> Right = { Orientation.east: Orientation.south, Orientation.south: Orientation.west, Orientation.west: Orientation.north, Orientation.north: Orientation.east};
  final Map<Orientation, int> dx = {Orientation.north: 0, Orientation.west: -1, Orientation.south: 0, Orientation.east: 1};
  final Map<Orientation, int> dy = {Orientation.north: 1, Orientation.west: 0, Orientation.south: -1, Orientation.east: 0};

  late Position position;
  late Orientation orientation;

  // Create a new Robot instance with the given position and orientation.
  // @param Position position: Inital position.
  // @param Orientation orientation: Initial orientation.
  Robot(this.position, this.orientation);

  // Move or rotate the robot according to the instructions given.
  // @param String instructions: String with characters A, R, or L only. R and L turn left or right and A advances the robot in the current direction.
  void move(String instructions) {
    for(String instruction in instructions.toUpperCase().split("")) {
      switch(instruction) {
        case "L":
          this.orientation = Left[this.orientation]!;
        case "R":
          this.orientation = Right[this.orientation]!;
        case "A":
          this.position.x += dx[this.orientation]!;
          this.position.y += dy[this.orientation]!;
        default:
          throw new Exception("Invalid direction \"$instruction\".");
      }
    }
  }
}
