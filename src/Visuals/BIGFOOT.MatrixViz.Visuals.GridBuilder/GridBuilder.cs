using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Models;
using BIGFOOT.MatrixViz.Visuals.Models;
using System.Threading.Tasks;
using System.Linq;
using BIGFOOT.MatrixViz.Inputs.Drivers.Enums;
using System.Threading;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Enums;
using System.Collections.Generic;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Constants;
using System;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder
{
    // visual playable modelled as a grid with a selector 
    // driven with controller inputs
    //      directional to move tile selector
    //      non-directional to execute actions on selected tile
    public class GridBuilder<TMatrix, TCanvas> : Playable<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        public TCanvas _canvas { get; set; }
        public GridBuilderTile[,] Grid { private set; get; }
        //public List<GridBuilderTile> Tiles { private set; get; }
        private GridTileSelector _selector { get; set; }
        private bool _finishedBuilding { get; set; }
        private readonly string _initialGridStateStr;
        private readonly int _totalNumMapSpots;

        public GridBuilder(TMatrix matrix, ControllerInputDriverBase input, string serializedMapStateToLoad = null) : base(matrix, input)
        {
            _initialGridStateStr = serializedMapStateToLoad;
            _totalNumMapSpots = Rows * Rows;
        }


        protected override void Run()
        {
            InitBuilder();

            while (!_finishedBuilding)
            {
                ExecutePlayableTick();
                Thread.Sleep(TickMs);
            }


        }
        private void InitBuilder()
        {
            _canvas = Matrix.InterfacedGetCanvas();
            _selector = new GridTileSelector(Rows);
            Grid = LoadGridNonCute(_initialGridStateStr);
        }

        protected override void Draw()
        {
            _canvas.Clear();
            DrawEntities();
            _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
        }

        private IEnumerable<IEnumerable<GridBuilderTile>> ParseMapRows(IEnumerable<char> map)
        {
            int charCount = map.Count();
            int strMapPtr = 0;

            for (int rowIdx = 0; rowIdx < Rows; rowIdx++)
            {
                int lower = strMapPtr;

                for (int i = 0; i < 32; i++)
                {
                    if (strMapPtr < charCount && map.ElementAt(strMapPtr) != '\n')
                    {
                        strMapPtr++;
                    }
                    else break;
                }

                int upper = strMapPtr;

                yield return ParseMapRow(string.Join("", map.Where((c, idx) => idx >= lower && idx < upper)));
            }
        }

        private IEnumerable<GridBuilderTile> ParseMapRow(IEnumerable<char> row)
        {
            for (int strMapPtr = 0; strMapPtr < Rows; strMapPtr++)
            {
                if (strMapPtr >= row.Count() || row.ElementAt(strMapPtr) == '\n')
                {
                    yield return new GridBuilderTile(' ');
                }
                else
                {
                    yield return new GridBuilderTile(row.ElementAt(strMapPtr));
                }
            }
        }

        private  GridBuilderTile[,] LoadGrid(string serializedGridToLoad = "")
        {
            var grid = new GridBuilderTile[Rows, Rows];

            var rows = ParseMapRows(serializedGridToLoad).ToList();
            for (int i = 0; i < Rows; i++)
            {
                var row = rows.ElementAt(i).ToList();
                for (int j = 0; j < Rows; j++)
                {
                    grid[i, j] = row.ElementAt(j);
                }
            }

            return grid;
        }

        private GridBuilderTile[,] LoadGridNonCute(string serializedGridToLoad = "")
        {
            var lines = serializedGridToLoad.Split('\n');
            var grid = new GridBuilderTile[Rows, Rows];

            for (int i = 0; i < Rows; i++)
            {
                var line = i < lines.Length ? lines[i] : string.Join("", Enumerable.Repeat(" ", Rows));
                for (int j = 0; j < Rows; j++)
                {
                    var c = j < line.Length ? line[j] : ' ';
                    GridBuilderTileType t = c == '#' ? GridBuilderTileType.BLOCK : GridBuilderTileType.EMPTY;
                    grid[j, i] = new GridBuilderTile(t);
                }
            }

            return grid;
        }

        public string SerializeMapState()
        {
            (int b0, int b1) = (Grid.GetLength(0), Grid.GetLength(1));
            int bound = b0 * b1;

            string stateStr = string.Empty;
            //for (int i = Grid.GetLength(0) * Grid.GetLength(1) - 1; i >= 0; i--)
            for (int i = 0; i < bound; i++)
            {
                (int x, int y) = (i / b0, i % b1);

                GridBuilderTileType type = Grid[y, x].Type;
                switch (type)
                {
                    case GridBuilderTileType.BLOCK:
                        stateStr += '#';
                        break;
                    case GridBuilderTileType.EMPTY:
                        stateStr += ' ';
                        break;
                    default:
                        throw new Exception($"Unknown tile type '{type}' at ({x},{y}))");
                }

                if (y == 63)
                {
                    stateStr += '\n';
                    continue;
                }
            }
            return stateStr;
        }

        private void ExecutePlayableTick()
        {
            HandleQueuedInputs();
            Draw();
        }

        private void HandleQueuedInputs()
        {
            foreach (var input in InputQueue)
                HandledQueuedDirectionInput(input);               

            InputQueue.Clear();
        }

        private void HandledQueuedDirectionInput(ControllerInput input) 
        {
            var prevSelectorPosition = (_selector.X, _selector.Y);
            switch (input)
            {
                case ControllerInput.UP:
                    _selector.Move(0, 1);
                    break;
                case ControllerInput.DOWN:
                    _selector.Move(0, -1);
                    break;
                case ControllerInput.LEFT:
                    _selector.Move(-1, 0);
                    break;
                case ControllerInput.RIGHT:
                    _selector.Move(1, 0);
                    break;
                case ControllerInput.ACTION_A:
                    _placeTileBtnActiveFramesLeft = 1;
                    Grid[_selector.X, _selector.Y] = new GridBuilderTile(GridBuilderTileType.BLOCK);
                    break;
            }

            if (prevSelectorPosition != (_selector.X, _selector.Y))
            {
                _placeTileBtnActiveFramesLeft = 0;
            }
        }

        private int _placeTileBtnActiveFramesLeft = 0;
        protected override void Handle_E_INPUT_ACTION_A(){ } // ?

        private void DrawEntities()
        {
            DrawTiles();
            DrawSelector();
        }

        private void DrawSelector()
        {
            var color = GridBuilderTileColors.SELECTOR_DEFAULT;
            if (_placeTileBtnActiveFramesLeft > 0)
            {
                color = GridBuilderTileColors.SELECTOR_ACTIVE;
                _placeTileBtnActiveFramesLeft--;
            }

            _canvas.SetPixel(_selector.X, _selector.Y, color);
        }

        private void DrawTiles()
        {
            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (Grid[x, y].IsEmpty || (x, y) == (_selector.X, _selector.Y))
                    {
                        continue;
                    }

                    _canvas.SetPixel(x, y, GridBuilderTileColors.BLOCK);
                }
            }
        }
    }

}
