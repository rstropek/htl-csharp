# Automated Unit Testing

## Introduction

In this exercise, we implement a simple [*Connect Four* game](https://en.wikipedia.org/wiki/Connect_Four). The game itself is not the focus, we focus on creating automated unit tests.

We started with this exercise two weeks ago in POSE. You find the code that we developed together here:

* [5AHIF](https://github.com/rstropek/htl-csharp/tree/master/testing/9020-5ahif-connect-four)
* [5BHIF](https://github.com/rstropek/htl-csharp/tree/master/testing/9020-5bhif-connect-four)

## Requirements

Two players must be able to play our game using *stdin*. The players enter the index of the column, in which they want to place their stones, one after the other. *4313233* would e.g. mean the following:

* Player one places a stone in column 4.
* Player two places a stone in column 3.
* Player one places a stone in column 1.
* Player two places a stone in column 3.
* ...

Invalid placements have to be *ignored*. A placement is invalid in the following cases:

* Invalid column index (too low or too high).
* Column is already full.

The game has to *end with an appropriate status message* on *stdout* in the following cases:

* A player has won (horizontally, vertically, or diagonally four adjacent stones).
* Board is completely full and no player has won.

## Tasks

* Implement the logic as defined in the previous chapter in a .NET Standard class library.
* Write automated unit tests (*xUnit*) that verify that the logic works as specified. Your tests have to cover *at least* the following cases (additional tests are welcome):
  * Correctly placed stones are handled appropriately.
  * Invalid placements are ignored.
    * Wrong index
    * Column full
  * Game ends if player wins.
    * Horizontally
    * Vertically
    * Diagonally (lower-left-to-upper-right and upper-left-to-lower-right)
  * Game ends if board is full and no player has won.
* Your tests verify the *logic*. You should *not* test the "UI" (i.e. input via *stdin*, output via *stdout*).

It is perfectly fine if you collaborate as a class or in groups to come up with a solution.

## Extra Points

* Check in your solution with all working tests to get an extra point for your grade.
* Write a simple WPF UI using the MVVM pattern and provide at least three meaningful unit tests for the ViewModel to get up to two additional extra points for your grade.

Mention me in a GitHub issue if you think you earned extra points. If you created the solution as a team, let me know the team members for extra points.
