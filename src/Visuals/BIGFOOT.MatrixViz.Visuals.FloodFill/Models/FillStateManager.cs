
using BIGFOOT.MatrixViz.Visuals.Enums;
using BIGFOOT.MatrixViz.Visuals.Maze;
using BIGFOOT.MatrixViz.Visuals.Models;
using System.Linq;

using System;

namespace BIGFOOT.MatrixViz.Visuals.FloodFill.Models
{
    internal class FillStateManager
    {
        public MatrixGridTile[,] CurrentState => _grid;
        public bool FillCompleted { get; private set; }

        private readonly MatrixGridTile[,] _grid;
        private readonly int _gridSize;

        public FillStateManager(string serializedInitialGrid, int size = 64)
        {
            _gridSize = size;

            if (string.IsNullOrWhiteSpace(serializedInitialGrid))
                _grid = MatrixGridUtility.EmptyGrid(_gridSize);
                //serializedInitialGrid = new SimpleMaze(_gridSize, _gridSize).Serialize();
            else
                _grid = MatrixGridUtility.Deserialize(serializedInitialGrid, _gridSize);
            
            SanitizeStateForSingleStartSeedCondition();
        }

        // initial implementation will only support filling from a single start seed
        // if exists, find the first start seed and clear others 
        // otherwise add one at a random position
        private void SanitizeStateForSingleStartSeedCondition()
        {
            var existingStartSeeds = from MatrixGridTile tile in _grid
                                     where tile.Type == MatrixGridTileType.START
                                     select tile;

            if (existingStartSeeds.Any())
            {
                existingStartSeeds.Skip(1).ToList().ForEach(excessStartSeedTile => { excessStartSeedTile.ClearTile(); });
                return;
            }

            for(Random random = new Random(); ;)
            {
                var randomlyChosenTile = _grid[random.Next(_gridSize), random.Next(_gridSize)];
                if (randomlyChosenTile.IsEmpty)
                {
                    randomlyChosenTile.PlaceNewTile(MatrixGridTileType.START);
                    break;
                }
            } 
        }

        public void StepStateForward()
        {
            TempStateChanger();
        }


        private void TempStateChanger()
        {
            //Array values = Enum.GetValues(typeof(MatrixGridTileType));
            //var randomTileType = (MatrixGridTileType)values.GetValue(random.Next(values.Length));
            
            Random random = new Random();

            int bound = _gridSize * _gridSize;
            for (int i = 0; i < bound; i++)
                if (random.Next(10) == 8)
                {
                    MatrixGridTileType emptyOrVisited = MatrixGridTileType.EMPTY; 
                    if (random.Next(2) == 1)
                        emptyOrVisited = MatrixGridTileType.VISITED;

                    _grid[i / _gridSize, i % _gridSize] = new MatrixGridTile(emptyOrVisited);
                }
        }
    }
}
