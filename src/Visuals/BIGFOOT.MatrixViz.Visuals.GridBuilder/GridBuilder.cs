using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Models;
using System.Linq;
using BIGFOOT.MatrixViz.Inputs.Drivers.Enums;
using System.Threading;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Enums;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Constants;
using System;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder
{
    // GridBuilder: playable modelled as a grid with a selector 
    // driven with controller inputs
    //      directional to move tile selector
    //      non-directional to execute actions on selected tile
    public class GridBuilder<TMatrix, TCanvas> : Playable<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        public TCanvas _canvas { get; set; }
        public GridBuilderTile[,] Grid { private set; get; }
        private GridTileSelector _selector { get; set; }
        private bool _finishedBuilding { get; set; }
        private readonly string _initialGridStateStr;

        public GridBuilder(TMatrix matrix, ControllerInputDriverBase input, string serializedMapStateToLoad = null) : base(matrix, input)
        {
            _initialGridStateStr = serializedMapStateToLoad;
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
            Grid = DeserializeGrid(_initialGridStateStr);
            _selector = new GridTileSelector(Rows);
        }

        private void ExecutePlayableTick()
        {
            Draw();
            ExecuteQueueInputEvents();
        }

        private void ExecuteQueueInputEvents()
        {
            foreach (var input in InputQueue)
                HandleInputEvent(input);

            InputQueue.Clear();
        }

        protected override void Draw()
        {
            _canvas.Clear();
            DrawEntities();
            _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
        }

        private GridBuilderTile[,] DeserializeGrid(string serializedGrid = "")
        {
            var lines = serializedGrid.Split('\n');
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
                    case GridBuilderTileType.START:
                        stateStr += '%';
                        break;
                    case GridBuilderTileType.TARGET:
                        stateStr += '$';
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

        private void HandleInputEvent(ControllerInput input) 
        {
            switch (input)
            {
                case ControllerInput.ACTION_A:
                    Grid[_selector.X, _selector.Y].PlaceNewTile(GridBuilderTileType.BLOCK);
                    break;
                case ControllerInput.ACTION_B:
                    var tileUnderSelector = Grid[_selector.X, _selector.Y];
                    if (tileUnderSelector.Type == GridBuilderTileType.START)
                    {
                        tileUnderSelector.ClearTile();
                        break;
                    }

                    var existingStartTiles = from GridBuilderTile tile in Grid
                                             where tile.Type == GridBuilderTileType.START
                                             select tile;
                    
                    existingStartTiles.ToList().ForEach(tile => tile.ClearTile());
                    tileUnderSelector.PlaceNewTile(GridBuilderTileType.START);
                    
                    break;
                case ControllerInput.ACTION_X:
                    Grid[_selector.X, _selector.Y].PlaceNewTile(GridBuilderTileType.TARGET);
                    break;
                case ControllerInput.ACTION_Y:
                    // does nothing atm
                    break;
                case ControllerInput.DELETE:
                    Grid[_selector.X, _selector.Y].ClearTile();
                    break;
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
            }
        }

        private void DrawEntities()
        {
            DrawTiles();
            DrawSelector();
        }

        private void DrawSelector()
        {
            if (_selector.AsyncIsCurrentlyTransparent)
                return;

            _canvas.SetPixel(_selector.X, _selector.Y, GridBuilderTileColors.SELECTOR);
        }

        private void DrawTiles()
        {
            int bound = Rows * Rows;
            for (int i = 0; i < bound; i++)
            {
                (int x, int y) = (i / 64, i % 64);
               
                var tile = Grid[x, y];
                if (tile.IsEmpty)
                    continue;

                _canvas.SetPixel(x, y, DetermineTileColor(tile.Type));
            }
        }

        private static Color DetermineTileColor(GridBuilderTileType tileType)
        {
            switch (tileType)
            {
                case GridBuilderTileType.BLOCK:
                    return GridBuilderTileColors.BLOCK;
                case GridBuilderTileType.START:
                    return GridBuilderTileColors.START;
                case GridBuilderTileType.TARGET:
                    return GridBuilderTileColors.TARGET;
                default:
                    throw new Exception("GridBuilder could not draw unrecognized GridBuilderTileType: " + tileType);
            }
        }
    }

}
