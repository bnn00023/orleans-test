# Game Design Overview

This document summarizes the core concepts of the Gomoku system built with DDD and Orleans.

## Domain Layer

| Class | Responsibility |
|-------|---------------|
| `GameId` | Represents the unique identifier for a game instance. |
| `Player` | Value object describing the participant (id, name, stone color). |
| `Board` | Represents the game board grid and tracks stone positions. |
| `Coordinate` | X/Y coordinate on the board. |
| `Stone` | Enum defining black or white stones. |
| `WinRule` | Encapsulates the logic for detecting a winning sequence. |
| `GameAggregate` | Aggregate root managing board state, players, turns and applying rules. |

## Application Layer

| Command/Query | Purpose |
|---------------|---------|
| `StartGameCommand` | Creates a new game and initializes the board. |
| `PlaceStoneCommand` | Places a stone on the board for the active player. |
| `GetGameStatusQuery` | Returns current board state and game result. |

## Key Use Cases

1. **Start a Game**
   - Client issues `StartGameCommand` to begin a new match.
2. **Make a Move**
   - Player sends `PlaceStoneCommand` with a coordinate.
   - `GameAggregate` validates move and applies `WinRule` to check for victory.
3. **Query Game Status**
   - A consumer executes `GetGameStatusQuery` to obtain board layout and whose turn it is.

These building blocks are the foundation for implementing the Gomoku game using the Orleans actor framework and following DDD best practices.
