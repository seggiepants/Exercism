import 'dart:math';

// Exercise: Darts
class Darts {
  // Put your code here

  // Score a throw in a game of darts. The board has a radius of 10, the middle ring is between 1 and 5, outer ring > 5 <= 10, and inner ring <= 1
  // inner ring 10 points, middle ring 5 points, outer ring 1 point, and out of bounds 0 points.
  // @param num x: The x-coordinate where the dart hit (center of the board is at 0,0)
  // @param num y: The y-coordinate where the dart hit (center of the board is at 0,0)
  // @returns int: The calculated score 0 - out of bounds, 1 outer ring, 5 middle ring, and 10 for inner most ring.
  int score(num x, num y) {
    double hypotenuse = sqrt((x * x) + (y * y));
    if (hypotenuse > 10.0) return 0;

    if (hypotenuse > 5.0) return 1;

    if (hypotenuse > 1.0) return 5;

    return 10;

  }
}
