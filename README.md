# Tic Tac Toe
## Overview

Some time ago I've finished a test task for one company and I've decided to use it as a code example if it fits the requirements.

> Note: The game mostly adapted for mobile vertical aspect ratio.

It is a classical Tic Tac Toe game. We have two different players X and O. Each player moves with their turn and tries to make a win line streak. 
There are 3 game modes:
- Player vs Player (local match)
- Player vs AI
- AI vs AI

For AI there are different difficulty settings.
- Easy: AI player moves randomly
- Pro: AI player moves using the best possible move

## Different features:
- Stopwatch - Counts the duration of the game.
- Hint - Suggest the next move for the player (Only in Player vs AI mode).
- Undo - Undo the previous moves (Only in Player vs AI mode).
- Restart Game

## Unit Tests:
According to test task I had to implement Unit Tests. You can find them under .../Scripts/Tests/Editor/...

## Adressable Assets:
According to test task I had to implement a custom editor script which has references to some assets and it can bake AssetBundle and put it under Streaming Asset folder. This code is just for fullfil that bulletpoint. _I'd use Addressable Assets in real project instead._
