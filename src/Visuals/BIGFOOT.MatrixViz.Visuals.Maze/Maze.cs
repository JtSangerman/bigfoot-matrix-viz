using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Visuals.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


// TODO refactor class structure and the awful code in general.
namespace BIGFOOT.MatrixViz.Visuals.Maze
{
    public class Coordinate
    {

        private int r, c;

        public Coordinate(int r, int c)
        {
            this.r = r;
            this.c = c;
        }

        public int getRow() { return r; }
        public int getCol() { return c; }
    }

    public class MazeSolver<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private MazeStack<TMatrix, TCanvas> _randMazeGeneratorStack;
        public string SerializedMazeStr { get => _randMazeGeneratorStack.toString(); }

        public MazeSolver(TMatrix matrix, int? tickMs = null) : base(matrix, tickMs)
        {
            LoadGeneratedRandomMaze();
        }

        public MazeSolver(TMatrix matrix, string serializedMazeStr, int? tickMs = null) : base(matrix, tickMs)
        {
            LoadSerializedMaze(serializedMazeStr);
        }
        private void LoadSerializedMaze(string serializedMazeStr)
        {
            var maze = new char[Matrix.Size, Matrix.Size];
            var mazeOverrideLines = serializedMazeStr
                .Split('\n')
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();

            for (int i = 0; i < mazeOverrideLines.Length; i++)
            {
                var line = mazeOverrideLines[i];
                for (int j = 0; j < line.Length; j++) maze[i, j] = line[j];
            }

            _randMazeGeneratorStack = new MazeStack<TMatrix, TCanvas>(Matrix, TickMs, maze); // is needed?
        }

        private void LoadGeneratedRandomMaze()
        {
            var mazeCutterGrid = CreateEmptyMazeCutterGrid();
            _randMazeGeneratorStack = new MazeStack<TMatrix, TCanvas>(Matrix, TickMs);

            char[] viableCutDirections;
            Random rand = new Random();
            while (!_randMazeGeneratorStack.IsEmpty())
            {
                Coordinate next = _randMazeGeneratorStack.Peek();
                if ((viableCutDirections = GetViableCutDirections(mazeCutterGrid, next.getRow(), next.getCol())).Length != 0)
                {
                    char cutDir = viableCutDirections[rand.Next(viableCutDirections.Length)]; 
                    switch (cutDir)
                    {
                        case 'U': //UP
                            _randMazeGeneratorStack.Push(new Coordinate(next.getRow() - 1, next.getCol()));
                            mazeCutterGrid[next.getRow() - 1, next.getCol()] = ' ';
                            break;
                        case 'R': //RIGHT
                            _randMazeGeneratorStack.Push(new Coordinate(next.getRow(), next.getCol() + 1));
                            mazeCutterGrid[next.getRow(), next.getCol() + 1] = ' ';
                            break;
                        case 'D': //DOWN
                            _randMazeGeneratorStack.Push(new Coordinate(next.getRow() + 1, next.getCol()));
                            mazeCutterGrid[next.getRow() + 1, next.getCol()] = ' ';
                            break;
                        case 'L': //LEFT
                            _randMazeGeneratorStack.Push(new Coordinate(next.getRow(), next.getCol() - 1));
                            mazeCutterGrid[next.getRow(), next.getCol() - 1] = ' ';
                            break;
                        default: break; //("check dir switch"); break; //(debugging)
                    }
                    _randMazeGeneratorStack.Cut(next, cutDir); 
                }
                else _randMazeGeneratorStack.Pop();
            }
        }

        private char[,] CreateEmptyMazeCutterGrid()
        {
            var size = Matrix.Size / 2 - 1;
            var maze = new char[size, size];

            for (int i = 0; i < maze.GetLength(0); i++)
                for (int j = 0; j < maze.GetLength(1); j++)
                    maze[i, j] = '#';

            return maze;
        }
        public override void VisualizeOnHardware() => throw new NotImplementedException();
        public override void VisualizeVirtually() => _randMazeGeneratorStack.Solve();

        private char[] GetViableCutDirections(char[,] cutterGrid, int row, int col)
        {
            char[] dirs = new char[4];
            if (row - 1 >= 0 && cutterGrid[row - 1, col] == '#') //UP
                dirs[0] = 'U';
            else
                dirs[0] = 'x';
            if (col + 1 < cutterGrid.GetLength(0) && cutterGrid[row, col + 1] == '#') //RIGHT
                dirs[1] = 'R';
            else
                dirs[1] = 'x';
            if (row + 1 < cutterGrid.GetLength(0) && cutterGrid[row + 1, col] == '#') //DOWN
                dirs[2] = 'D';
            else
                dirs[2] = 'x';
            if (col - 1 >= 0 && cutterGrid[row, col - 1] == '#') //LEFT
                dirs[3] = 'L';
            else
                dirs[3] = 'x';

            int numDir = 0;
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i] != 'x')
                    numDir++;
            }

            char[] validDirs = new char[numDir];
            int index = 0;
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i] != 'x')
                {
                    validDirs[index] = dirs[i];
                    index++;
                }
            }

            return validDirs;
        }
    }

    public class MazeStack<TMatrix, TCanvas> 
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private LinkedList<Coordinate> MazeGenRandomizerStack;
        
        private char[,] _maze;
        private bool[,] _visited;
        private readonly int _tickMs;

        private TCanvas _canvas;
        private readonly TMatrix _matrix;

        private Coordinate _startPos;
        private Coordinate _end;




        private Color WallColor = MatrixGridTileColors.BLOCK;
        private Color EmptyPathColor = MatrixGridTileColors.EMPTY;
        private Color VistedPathColor = MatrixGridTileColors.VISITED;

        private Color WalkerColor = MatrixGridTileColors.SELECTOR;
        private Color WalkerSolvedRouteColor = MatrixGridTileColors.TARGET;

        private Color GoalColor = MatrixGridTileColors.TARGET;
        private Color StartColor = MatrixGridTileColors.START;


        public MazeStack(TMatrix matrix, int tickMs, char[,] overrideMaze = null)
        {
            _tickMs = tickMs;
            _matrix = matrix;
            Init(overrideMaze);
        }

        private void Init(char[,] mazeOverride = null)
        {
            _canvas = _matrix.InterfacedGetCanvas();
            _visited = new bool[_matrix.Size, _matrix.Size];
            MazeGenRandomizerStack = new LinkedList<Coordinate>();

            if (mazeOverride != null)
            {
                _maze = mazeOverride;
                return;
            }

            _maze = InitRandomMazeGeneration();
        }

        private char[,] InitRandomMazeGeneration()
        {
            var maze = new char[_matrix.Size, _matrix.Size];
            for (int i = 0; i < maze.GetLength(0); i++)
                for (int j = 0; j < maze.GetLength(0); j++)
                    maze[i, j] = '#';

            Random r = new Random();
            int mazeGenStartRow = r.Next(_matrix.Size / 2 - 1);
            int mazeGenStartCol = r.Next(_matrix.Size / 2 - 1);
            
            MazeGenRandomizerStack.AddFirst(new Coordinate(mazeGenStartRow, mazeGenStartCol));
            
            //maze[mazeGenStartRow, mazeGenStartCol] = ' ';
            return maze;
        }

        public void Push(Coordinate cord)
        {
            //Pushes an item onto the top of this stack. This has exactly the same effect as stacks push()
            MazeGenRandomizerStack.AddFirst(cord);
        }

        public Coordinate Peek()
        {
            //Looks at the object at the top of this stack without removing it from the stack.
            return MazeGenRandomizerStack != null && MazeGenRandomizerStack.Count > 0 ? MazeGenRandomizerStack.First.Value : null;
        }

        public Coordinate Pop()
        {
            //Removes the object at the top of this stack and returns that object as the value of this function.
            Coordinate temp = MazeGenRandomizerStack.First.Value;
            MazeGenRandomizerStack.RemoveFirst();
            return temp;
        }

        public void Cut(Coordinate cord, char dir)
        {
            //clears the current spot in the maze
            int row = 2 * cord.getRow() + 1;
            int col = 2 * cord.getCol() + 1;

            switch (dir)
            {
                case 'U':
                    for (int i = 0; i <= 2; i++)
                        _maze[row - i, col] = ' ';
                    break;
                case 'R':
                    for (int i = 0; i <= 2; i++)
                        _maze[row, col + i] = ' ';
                    break;
                case 'D':
                    for (int i = 0; i <= 2; i++)
                        _maze[row + i, col] = ' ';
                    break;
                case 'L':
                    for (int i = 0; i <= 2; i++)
                        _maze[row, col - i] = ' ';
                    break;
            }
        }

        public String toString()
        {
            String retVal = "";
            for (int i = 0; i < _maze.GetLength(0); i++)
            {
                for (int j = 0; j < _maze.GetLength(0); j++)
                {
                    retVal += _maze[i, j];
                    if (j == _maze.GetLength(0) - 1)
                        retVal += "\n";
                }
            }

            return retVal;
        }

        public void Draw()
        {
            _canvas = _matrix.InterfacedSwapOnVsync(_matrix.InterfacedGetCanvas());
            for (int i = 0; i < _maze.GetLength(0); i++)
            {
                int ind = 0;
                for (int j = 0; j < _maze.GetLength(0); j++)
                {
                    if (_maze[i, j] == '#' && j + 1 <= _maze.GetLength(0) - 1 && _maze[i, j + 1] == '#')
                        _canvas.DrawLine(ind, i, ind + 1, i, WallColor);
                    if (_maze[i, j] == '#' && i + 1 <= _maze.GetLength(0) - 1 && _maze[i + 1, j] == '#')
                        _canvas.DrawLine(ind, i, ind, i + 1, WallColor);
                    if (_maze[i, j] == '%')
                        _canvas.SetPixel(j, i, StartColor);
                    if (_maze[i, j] == '$')
                        _canvas.SetPixel(j, i, GoalColor);
                    if (_maze[i, j] == ' ')
                        _canvas.SetPixel(j, i, EmptyPathColor);
                    ind++;
                }
            }

        }

        public void MakeExits()
        {
            for (int i = 0; i < _maze.GetLength(0); i++)
            {
                for (int j = 0; j < _maze.GetLength(1); j++)
                {
                    if (_maze[i, j] == '%')
                    {
                        _startPos = new Coordinate(i, j);
                    }

                    if (_maze[i, j] == '$')
                    {
                        _end = new Coordinate(i, j);
                    }
                }
            }

            if (_startPos != null && _end != null) return;

            for (int i = 0; i < _maze.GetLength(0); i++) //first row
                _maze[0, i] = '#';

            for (int i = 0; i < _maze.GetLength(0); i++) //last column
                _maze[i, 0] = '#';

            for (int i = 0; i < _maze.GetLength(0); i++) //last row
                _maze[_maze.GetLength(0) - 1, i] = '#';

            for (int i = 0; i < _maze.GetLength(0); i++) //first column
                _maze[0, i] = '#';


            int entrance = _maze.GetLength(0) - 3;
            int exit = 1;
            _startPos = new Coordinate(entrance, 1);
            _end = new Coordinate(exit, _maze.GetLength(0) - 2);

            //we cut two spots to make sure the exits are accessible
            _maze[entrance, 0] = ' ';
            _maze[entrance, 1] = ' ';

            _maze[exit, _maze.GetLength(0) - 1] = ' ';
            _maze[exit, _maze.GetLength(0) - 2] = ' ';

        }

        public bool IsEmpty() { return MazeGenRandomizerStack.Count == 0; }

        public void Solve()
        {
            MakeExits();
            Draw();

            SolveMazeRecursively(_maze, _end.getCol() + 1, _end.getRow(), -1);

            _canvas = _matrix.InterfacedSwapOnVsync(_canvas);

            DrawAtEnd();
        }


        private int _prevRow = -1;
        private int _prevCol = -1;
        private bool _solved = false;
        private bool SolveMazeRecursively(char[,] StackMaze, int x, int y, int d)
        {
            _canvas = _matrix.InterfacedSwapOnVsync(_canvas);
            if (_prevRow != -1)
                _canvas.SetPixel(_prevRow, _prevCol, VistedPathColor);

            _prevRow = x;
            _prevCol = y;
            
            if (_visited[y, x])
                return false;
            
            if (!_solved)
            {
                Thread.Sleep(_tickMs);
                _canvas.SetPixel(x, y, VistedPathColor);
                _visited[y, x] = true;
            }

            bool ok = false;

            for (int i = 0; i < 4 && !ok; i++)
            {
                if (i != d)
                {
                    // check for end condition
                    //if ((x == _startPos.getCol() || x - 1 == _startPos.getCol()) && y == _startPos.getRow())
                    if (x == _startPos.getCol() && y == _startPos.getRow())
                    {
                        _solved = true;
                        ok = true;
                    }

                    _canvas = _matrix.InterfacedSwapOnVsync(_canvas);
                    switch (i)
                    {
                        case 0:
                            if ((y - 1 >= 0) && StackMaze[y - 1, x] == ' ' && !_solved)
                            {
                                if (StackMaze[y - 1, x] == ' ')
                                    _canvas.SetPixel(x, y - 1, WalkerColor);
                                ok = SolveMazeRecursively(StackMaze, x, y - 1, 2);
                            }
                            break;
                        case 1:
                            if ((x + 1 < StackMaze.GetLength(0)) && StackMaze[y, x + 1] == ' ' && !_solved)
                            {
                                if (StackMaze[y, x + 1] == ' ')
                                    _canvas.SetPixel(x + 1, y, WalkerColor);
                                ok = SolveMazeRecursively(StackMaze, x + 1, y, 3);
                            }
                            break;
                        case 2:
                            if ((y + 1 < StackMaze.GetLength(0)) && StackMaze[y + 1, x] == ' ' && !_solved)
                            {
                                if (StackMaze[y + 1, x] == ' ')
                                    _canvas.SetPixel(x, y + 1, WalkerColor);

                                ok = SolveMazeRecursively(StackMaze, x, y + 1, 0);
                            }
                            break;
                        case 3:
                            if ((x - 1 >= 0) && StackMaze[y, x - 1] == ' ' && !_solved)
                            {
                                if (StackMaze[y, x - 1] == ' ')
                                    _canvas.SetPixel(x - 1, y, WalkerColor);

                                ok = SolveMazeRecursively(StackMaze, x - 1, y, 1);
                            }
                            break;
                    }
                }
            }

            if (ok)
            {
                Thread.Sleep(_tickMs);
                _canvas = _matrix.InterfacedSwapOnVsync(_canvas);
                
                StackMaze[y, x] = '*';

                _canvas.SetPixel(_startPos.getCol(), _startPos.getRow(), WalkerSolvedRouteColor);
                _canvas.SetPixel(_startPos.getCol()-1, _startPos.getRow(), WalkerSolvedRouteColor);

                switch (d)
                {
                    case 0:
                        _canvas.SetPixel(x, y - 1, WalkerSolvedRouteColor);
                        StackMaze[y - 1, x] = '*';
                        break;
                    case 1:
                        _canvas.SetPixel(x + 1, y, WalkerSolvedRouteColor);
                        StackMaze[y, x + 1] = '*';
                        break;
                    case 2:
                        _canvas.SetPixel(x, y + 1, WalkerSolvedRouteColor);
                        StackMaze[y + 1, x] = '*';
                        break;
                    case 3:
                        _canvas.SetPixel(x - 1, y, WalkerSolvedRouteColor);
                        StackMaze[y, x - 1] = '*';
                        break;
                }
            }
            _canvas = _matrix.InterfacedSwapOnVsync(_canvas);
            return ok;
        }

        public void DrawAtEnd()
        {
            for (int i = 0; i < _maze.GetLength(0); i++)
            {
                for (int j = 0; j < _maze.GetLength(0); j++)
                {
                    if (_maze[i, j] == '*')
                    {
                        _canvas.SetPixel(j, i, WalkerSolvedRouteColor);
                    }
                    else if (_maze[i, j] == ' ')
                    {
                        if (_visited[i, j])
                            _canvas.SetPixel(j, i, VistedPathColor);
                        else
                            _canvas.SetPixel(j, i, EmptyPathColor);
                    }
                    else if (_maze[i, j] == '%')
                    {
                        _canvas.SetPixel(j, i, StartColor);
                    }
                    else if (_maze[i, j] == '$')
                    {
                        _canvas.SetPixel(j, i, WalkerSolvedRouteColor);
                    }
                    else if (_maze[i, j] == '#')
                    {
                        _canvas.SetPixel(j, i, WallColor);
                    }
                    else throw new Exception($"Unknown handling for drawing maze tile '{_maze[i,j]}'");
                }
                _canvas = _matrix.InterfacedSwapOnVsync(_canvas);
            }

            _canvas.SetPixel(_startPos.getCol(), _startPos.getRow(), WalkerSolvedRouteColor);
            _canvas.SetPixel(_startPos.getCol() - 1, _startPos.getRow(), WalkerSolvedRouteColor);
        }
    }
}
