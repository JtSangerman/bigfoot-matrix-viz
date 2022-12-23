using BIGFOOT.MatrixViz.DriverInterfacing;
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

    public class MazeHolder<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private MazeStack<TMatrix, TCanvas> _randMazeGeneratorStack;
        public string SerializedMazeStr { get => _randMazeGeneratorStack.toString(); }

        public MazeHolder(TMatrix matrix, int? tickMs = null) : base(matrix, tickMs)
        {
            LoadGeneratedRandomMaze();
        }

        public MazeHolder(TMatrix matrix, string serializedMazeStr, int? tickMs = null) : base(matrix, tickMs)
        {
            LoadDeserializedMaze(serializedMazeStr);
        }
        private void LoadDeserializedMaze(string serializedMazeStr)
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

        public string SerializeMaze(char[,] maze)
        {
            string mazeStr = string.Empty;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    mazeStr += maze[i, j].ToString();
                }
                mazeStr += "\n";
            }
            return mazeStr;
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
        
        private char[,] Maze;
        private bool[,] Visited;

        private Coordinate Start;
        private Coordinate End;
        
        private Color WallColor = Color.FromKnownColor(System.Drawing.KnownColor.MidnightBlue);
        private Color VisitedColor = Color.FromKnownColor(System.Drawing.KnownColor.LightSkyBlue);
        private Color WalkerColor = Color.FromKnownColor(System.Drawing.KnownColor.Indigo);
        private Color GoalColor = Color.FromKnownColor(System.Drawing.KnownColor.Green);
        private Color StartColor = Color.FromKnownColor(System.Drawing.KnownColor.Red);
        private Color SolverWalkerColor = new Color(0, 210, 0);

        private TCanvas Canvas;
        private readonly TMatrix Matrix;

        private readonly int _tickMs;

        public MazeStack(TMatrix matrix, int tickMs, char[,] overrideMaze = null)
        {
            _tickMs = tickMs;
            Matrix = matrix;
            Init(overrideMaze);
        }

        private void Init(char[,] mazeOverride = null)
        {
            Canvas = Matrix.InterfacedGetCanvas();
            Visited = new bool[Matrix.Size, Matrix.Size];
            MazeGenRandomizerStack = new LinkedList<Coordinate>();

            if (mazeOverride != null)
            {
                Maze = mazeOverride;
                return;
            }

            Maze = InitRandomMazeGeneration();
        }

        private char[,] InitRandomMazeGeneration()
        {
            var maze = new char[Matrix.Size, Matrix.Size];
            for (int i = 0; i < maze.GetLength(0); i++)
                for (int j = 0; j < maze.GetLength(0); j++)
                    maze[i, j] = '#';

            Random r = new Random();
            int mazeGenStartRow = r.Next(Matrix.Size / 2 - 1);
            int mazeGenStartCol = r.Next(Matrix.Size / 2 - 1);
            
            MazeGenRandomizerStack.AddFirst(new Coordinate(mazeGenStartRow, mazeGenStartCol));
            
            maze[mazeGenStartRow, mazeGenStartCol] = ' ';
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
                        Maze[row - i, col] = ' ';
                    break;
                case 'R':
                    for (int i = 0; i <= 2; i++)
                        Maze[row, col + i] = ' ';
                    break;
                case 'D':
                    for (int i = 0; i <= 2; i++)
                        Maze[row + i, col] = ' ';
                    break;
                case 'L':
                    for (int i = 0; i <= 2; i++)
                        Maze[row, col - i] = ' ';
                    break;
            }
        }

        public String toString()
        {
            String retVal = "";
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(0); j++)
                {
                    retVal += Maze[i, j];
                    if (j == Maze.GetLength(0) - 1)
                        retVal += "\n";
                }
            }

            return retVal;
        }

        public void Draw()
        {
            Canvas = Matrix.InterfacedSwapOnVsync(Matrix.InterfacedGetCanvas());
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                int ind = 0;
                for (int j = 0; j < Maze.GetLength(0); j++)
                {
                    if (Maze[i, j] == '#' && j + 1 <= Maze.GetLength(0) - 1 && Maze[i, j + 1] == '#')
                        Canvas.DrawLine(ind, i, ind + 1, i, WallColor);
                    if (Maze[i, j] == '#' && i + 1 <= Maze.GetLength(0) - 1 && Maze[i + 1, j] == '#')
                        Canvas.DrawLine(ind, i, ind, i + 1, WallColor);
                    if (Maze[i, j] == '%')
                        Canvas.SetPixel(j, i, StartColor);
                    if (Maze[i, j] == '$')
                        Canvas.SetPixel(j, i, GoalColor);
                    ind++;
                }
            }

        }

        public void MakeExits()
        {
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    if (Maze[i, j] == '%')
                    {
                        Start = new Coordinate(i, j);
                    }

                    if (Maze[i, j] == '$')
                    {
                        End = new Coordinate(i, j);
                    }
                }
            }

            if (Start != null && End != null) return;

            for (int i = 0; i < Maze.GetLength(0); i++) //first row
                Maze[0, i] = '#';

            for (int i = 0; i < Maze.GetLength(0); i++) //last column
                Maze[i, 0] = '#';

            for (int i = 0; i < Maze.GetLength(0); i++) //last row
                Maze[Maze.GetLength(0) - 1, i] = '#';

            for (int i = 0; i < Maze.GetLength(0); i++) //first column
                Maze[0, i] = '#';


            int entrance = Maze.GetLength(0) - 2;
            int exit = 1;
            Start = new Coordinate(entrance, 1);
            End = new Coordinate(exit, Maze.GetLength(0) - 2);

            //we cut two spots to make sure the exits are accessible
            Maze[entrance, 0] = ' ';
            Maze[entrance, 1] = ' ';

            Maze[exit, Maze.GetLength(0) - 1] = ' ';
            Maze[exit, Maze.GetLength(0) - 2] = ' ';

        }

        public bool IsEmpty() { return MazeGenRandomizerStack.Count == 0; }

        public void Solve()
        {
            MakeExits();
            Draw();

            SolveMazeRecursively(Maze, End.getCol() + 1, End.getRow(), -1);

            Canvas = Matrix.InterfacedSwapOnVsync(Canvas);

            DrawAtEnd();
        }


        private int _prevRow = -1;
        private int _prevCol = -1;
        private bool _solved = false;
        private bool SolveMazeRecursively(char[,] StackMaze, int x, int y, int d)
        {
            Canvas = Matrix.InterfacedSwapOnVsync(Canvas);
            if (_prevRow != -1)
                Canvas.SetPixel(_prevRow, _prevCol, VisitedColor);

            _prevRow = x;
            _prevCol = y;
            
            if (Visited[y, x])
                return false;
            
            if (!_solved)
            {
                Thread.Sleep(_tickMs);
                Canvas.SetPixel(x, y, VisitedColor);
                Visited[y, x] = true;
            }

            bool ok = false;

            for (int i = 0; i < 4 && !ok; i++)
            {
                if (i != d)
                {
                    // check for end condition
                    if ((x == Start.getCol() || x - 1 == Start.getCol()) && y == Start.getRow())
                    {
                        _solved = true;
                        ok = true;
                    }

                    Canvas = Matrix.InterfacedSwapOnVsync(Canvas);
                    switch (i)
                    {
                        case 0:
                            if ((y - 1 >= 0) && StackMaze[y - 1, x] == ' ' && !_solved)
                            {
                                if (StackMaze[y - 1, x] == ' ')
                                    Canvas.SetPixel(x, y - 1, WalkerColor);
                                ok = SolveMazeRecursively(StackMaze, x, y - 1, 2);
                            }
                            break;
                        case 1:
                            if ((x + 1 < StackMaze.GetLength(0)) && StackMaze[y, x + 1] == ' ' && !_solved)
                            {
                                if (StackMaze[y, x + 1] == ' ')
                                    Canvas.SetPixel(x + 1, y, WalkerColor);
                                ok = SolveMazeRecursively(StackMaze, x + 1, y, 3);
                            }
                            break;
                        case 2:
                            if ((y + 1 < StackMaze.GetLength(0)) && StackMaze[y + 1, x] == ' ' && !_solved)
                            {
                                if (StackMaze[y + 1, x] == ' ')
                                    Canvas.SetPixel(x, y + 1, WalkerColor);

                                ok = SolveMazeRecursively(StackMaze, x, y + 1, 0);
                            }
                            break;
                        case 3:
                            if ((x - 1 >= 0) && StackMaze[y, x - 1] == ' ' && !_solved)
                            {
                                if (StackMaze[y, x - 1] == ' ')
                                    Canvas.SetPixel(x - 1, y, WalkerColor);

                                ok = SolveMazeRecursively(StackMaze, x - 1, y, 1);
                            }
                            break;
                    }
                }
            }

            if (ok)
            {
                Thread.Sleep(_tickMs);
                Canvas = Matrix.InterfacedSwapOnVsync(Canvas);
                
                StackMaze[y, x] = '*';
                
                Canvas.SetPixel(Start.getCol(), Start.getRow(), SolverWalkerColor);
                Canvas.SetPixel(Start.getCol() - 1, Start.getRow(), SolverWalkerColor);

                switch (d)
                {
                    case 0:
                        Canvas.SetPixel(x, y - 1, SolverWalkerColor);
                        StackMaze[y - 1, x] = '*';
                        break;
                    case 1:
                        Canvas.SetPixel(x + 1, y, SolverWalkerColor);
                        StackMaze[y, x + 1] = '*';
                        break;
                    case 2:
                        Canvas.SetPixel(x, y + 1, SolverWalkerColor);
                        StackMaze[y + 1, x] = '*';
                        break;
                    case 3:
                        Canvas.SetPixel(x - 1, y, SolverWalkerColor);
                        StackMaze[y, x - 1] = '*';
                        break;
                }
            }
            Canvas = Matrix.InterfacedSwapOnVsync(Canvas);
            return ok;
        }


        public void DrawAtEnd()
        {
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(0); j++)
                {
                    if (Maze[i, j] == '*')
                    {
                        Canvas.SetPixel(j, i, SolverWalkerColor);
                    }
                    else if (Maze[i, j] == ' ')
                    {

                    }
                    else if (Maze[i, j] == '%')
                    {
                        Canvas.SetPixel(j, i, StartColor);
                    }
                    else if (Maze[i, j] == '$')
                    {
                        Canvas.SetPixel(j, i, GoalColor);
                    }
                    else if (Maze[i, j] == '#')
                    {
                        Canvas.SetPixel(j, i, WallColor);
                    }
                    else if (Visited[i, j])
                    {
                        Canvas.SetPixel(j, i, VisitedColor);
                    }
                }
                Canvas = Matrix.InterfacedSwapOnVsync(Canvas);
            }
        }
    }
}
