import 'dart:math'; // for max

// High Scores exercise.

// Class to manage high scores.
class HighScores {
  // Put your code here

  late List<int> scores; // The high scores list.

  // Initialize the class saving the scores to a local variable.
  HighScores(this.scores);

  // Return the most recent score. Assumes there is at least one score.
  // @returns int: The most recently added score.
  int latest() {
    return this.scores.last;
  }

  // Return the best score in the high scores
  // @returns int: The maximum score in the high score list.
  int personalBest() {
    return this.scores.reduce(max);
  }

  // Return the top three results from the scores.
  // Doesn't mutate the original list so that latest still works.
  // @returns List<int>: Top three (or all if three or less values) results sorted best to worst.
  List<int> personalTopThree() {
    List<int> temp = [...this.scores];
    temp.sort((int a, int b) => b - a );
    return temp.take(3).toList();
  }
}
