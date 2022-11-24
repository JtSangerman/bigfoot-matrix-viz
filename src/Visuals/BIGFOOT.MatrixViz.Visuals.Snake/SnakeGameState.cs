using BIGFOOT.MatrixViz.Visuals.Snake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIGFOOT.MatrixViz.Visuals.Snake
{
    public class SnakeGameState
    {
        private readonly int _boardSize;
        internal readonly Tile[,] Board;
        private LinkedList<GameTile> _snake;
        private GameTile _goal;
        /// 
        private int Id = 0;


        internal bool IsGameOver { get; set; }

        public SnakeGameState(int boardSize)
        {
            _boardSize = boardSize;
            _snake = new LinkedList<GameTile>();
            Board = new Tile[boardSize, boardSize];

            Init();
        }

        private void Init()
        {
            // set all board tiles to default
            ClearBoard();

            //_snake.AddFirst(new GameTile(_boardSize / 2, _boardSize / 2, Tile.SNAKE_HEAD, ++Id));
            _snake.AddFirst(new GameTile(30, 30, Tile.SNAKE_HEAD, ++Id));

            PlaceRandomGoal();
        }


        // TODO throw exception if called innapropriately -- ie board is not set
        private void PlaceRandomGoal()
        {
            var random = new Random();
            int x, y;
            do
            {
                x = random.Next(_boardSize/2); // DEBUG lower possible spawn location
                y = random.Next(_boardSize/2); // DEBUG lower possible spawn location
            } while (Board[x, y] != Tile.EMPTY);

            _goal = new GameTile(x, y, Tile.GOAL);
            Board[_goal.X, _goal.Y] = Tile.GOAL;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < _boardSize * _boardSize; i++)
            {
                var x = i % _boardSize;
                var y = i / _boardSize;
                Board[x, y] = Tile.EMPTY;
            }
        }

        private void SetBoard()
        {
            Board[_goal.X, _goal.Y] = Tile.GOAL;
            
            foreach (var snakeSegment in _snake)
            {
                Board[snakeSegment.X, snakeSegment.Y] = snakeSegment.Type;
            }
        }

        internal async Task Tick(Direction direction)
        {
            //ClearSnake();
            ClearBoard();
            MoveSnake(direction);
            SetBoard();
            //PlaceSnake();
        }

        private void MoveSnake(Direction direction)
        {
            // execute goal sequence if goal is found
            GameTile newHead;
            if ((newHead = CheckIfWillLandOnGoal(direction)) != null)
            {
                var oldHeadNode = _snake.First;
                var oldHeadTile = oldHeadNode.Value;
                oldHeadTile.Type = Tile.SNAKE_BODY;

                 _snake.AddFirst(newHead);

                PlaceRandomGoal();
                return; 
            }

            // execute lose sequence if a losing move
            if (CheckIfWillBeLosingMove(direction))
            {
                IsGameOver = true;
                return;
            }

            // execute a normal move on the head
            var snakeHeadTile = _snake.First.Value;
            GameTile tempOldSnakeHead = new GameTile(snakeHeadTile.X, snakeHeadTile.Y, snakeHeadTile.Type, snakeHeadTile.Id);
            MoveTile(snakeHeadTile, direction);


            var list = _snake.ToList();
            var listCopy = list.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var tile = listCopy[i];
                if (tile.Type == Tile.SNAKE_HEAD)
                {
                    continue;
                }
                else
                {
                    if (i == 1)
                    {
                        list[i] = tempOldSnakeHead.Copy();
                        list[i].Type = Tile.SNAKE_BODY;
                    }
                    else
                    {
                        list[i] = listCopy[i - 1].Copy();
                    }
                }
            }

            list.Reverse();
            _snake.Clear();
            foreach(var t in list)
            {
                _snake.AddFirst(t);
            }

            //WrapOutOfBoundsTiles();
        }

        private bool CheckIfWillBeLosingMove(Direction direction)
        {
            var headTile = _snake.First.Value;
            var tempHead = new GameTile(headTile.X, headTile.Y, headTile.Type);

            tempHead = MoveTile(tempHead, direction);

            //if (
            //    tempHead.X >= _boardSize || 
            //    tempHead.X < 0 || 
            //    tempHead.Y >= _boardSize || 
            //    tempHead.Y < 0)
            //{
            //    Console.WriteLine("GAME OVER: Snake went out of bounds.");
            //    return true;
            //}
            if (_snake.Any(tile => (tile.X, tile.Y) == (tempHead.X, tempHead.Y)))
            {
                Console.WriteLine("GAME OVER: Snake ate itself.");
                return true;
            }

            return false;
        }

        private GameTile CheckIfWillLandOnGoal(Direction direction)
        {
            var headTile = _snake.First.Value;
            var tempHead = new GameTile(headTile.X, headTile.Y, headTile.Type);

            tempHead = MoveTile(tempHead, direction);

            if ((tempHead.X, tempHead.Y) == (_goal.X, _goal.Y))
            {
                return new GameTile(tempHead.X, tempHead.Y, Tile.SNAKE_HEAD, ++Id);
            }

            return null;
        }

        private GameTile MoveTile(GameTile tile, Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    Up(tile);
                    break;
                case Direction.DOWN:
                    Down(tile);
                    break;
                case Direction.LEFT:
                    Left(tile);
                    break;
                case Direction.RIGHT:
                    Right(tile);
                    break;
                default:
                    break;
            }

            return tile;
        }

        private void ClearSnake()
        {
            foreach (var tile in _snake)
            {
                Board[tile.X, tile.Y] = Tile.EMPTY;
            }
        } 

        private void PlaceSnake()
        {
            foreach (var tile in _snake)
            {
                Board[tile.X, tile.Y] = tile.Type;
            }
        }

        private void Up(GameTile tile)
        {
            tile.Up();
            //tile.Y++;
        }

        private void Down(GameTile tile)
        {
            tile.Down();
            //tile.Y--;
        }

        private void Left(GameTile tile)
        {
            tile.Left();
            //tile.X--;
        }

        private void Right(GameTile tile)
        {
            tile.Right();
            //tile.X++;
        }
    }
}
