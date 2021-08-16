//using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Threading;

//namespace BIGFOOT.RGBMatrix.Visuals.Maze
//{
//    public class Coordinate
//    {

//        private int r, c;

//        public Coordinate(int r, int c)
//        {
//            this.r = r;
//            this.c = c;
//        }

//        public int getRow() { return r; }
//        public int getCol() { return c; }
//    }

//    public class MazeHolder
//    {

//        private char[,] maze;
//        private int tickMs;

//        public MazeHolder(int rows, int cols, int tickMs)
//        {
//            //sets our scale, size of our maze, and defaults the maze array to full
//            int scale = 1;
//            this.tickMs = tickMs;

//            //StdDraw.setCanvasSize();
//            //default the maze array to be completely full
//            maze = new char[rows, cols];
//            for (int i = 0; i < maze.GetLength(0); i++)
//                for (int j = 0; j < maze.GetLength(0); j++)
//                    maze[i, j] = '#';

//            //find a random starting point on the r x c maze & clear it
//            Random r = new Random();
//            int rRow = r.Next(rows);
//            int rCol = r.Next(cols);
//            maze[rRow, rCol] = ' ';

//            MazeStack cornMaze = new MazeStack(rRow, rCol, 2 * rows + 1, 2 * cols + 1, scale, tickMs);

//            char[] dirs; //an empty list of valid directions in which to cut
//            while (!cornMaze.isEmpty())
//            {
//                //while the stack is not empty
//                Coordinate coord = cornMaze.peek(); //the cornMazerent coordinate locating at the top of the stack

//                if ((dirs = canMove(coord.getRow(), coord.getCol())).Length != 0)
//                {
//                    //if there exists a valid direction to cut, store those positions in dirs

//                    char dir = dirs[r.Next(dirs.Length)]; //then choose a random direction & cut

//                    //add a stack representing coordinates in the randomized, valid direction
//                    switch (dir)
//                    {
//                        case 'U': //UP
//                            cornMaze.push(new Coordinate(coord.getRow() - 1, coord.getCol()));
//                            maze[coord.getRow() - 1, coord.getCol()] = ' ';
//                            break;
//                        case 'R': //RIGHT
//                            cornMaze.push(new Coordinate(coord.getRow(), coord.getCol() + 1));
//                            maze[coord.getRow(), coord.getCol() + 1] = ' ';
//                            break;
//                        case 'D': //DOWN
//                            cornMaze.push(new Coordinate(coord.getRow() + 1, coord.getCol()));
//                            maze[coord.getRow() + 1, coord.getCol()] = ' ';
//                            break;
//                        case 'L': //LEFT
//                            cornMaze.push(new Coordinate(coord.getRow(), coord.getCol() - 1));
//                            maze[coord.getRow(), coord.getCol() - 1] = ' ';
//                            break;
//                        default: break; //("check dir switch"); break; //(debugging)
//                    }
//                    cornMaze.cut(coord, dir); //cut that position
//                }
//                else cornMaze.pop();
//            }


//            cornMaze.makeExits();
//            //Console.WriteLine("Drawing maze...");
//            cornMaze.draw();
//            //Console.WriteLine("Solving maze...");
//            cornMaze.solve();
//            //Console.WriteLine("Solved!");

//            Stopwatch s = new Stopwatch();
//            s.Start();
//            while (s.ElapsedMilliseconds < 7500)
//            {
//                cornMaze.drawAtEnd();
//            }

//            s.Stop();
//            //Thread.Sleep(7500);
//            //Console.WriteLine(cornMaze.toString());


//        }

//        private char[] canMove(int row, int col)
//        {

//            char[] dirs = new char[4];

//            //4 options: UP, RIGHT, DOWN, LEFT:
//            if (row - 1 >= 0 && maze[row - 1, col] == '#') //UP
//                dirs[0] = 'U';
//            else
//                dirs[0] = 'x';
//            if (col + 1 < maze.GetLength(0) && maze[row, col + 1] == '#') //RIGHT
//                dirs[1] = 'R';
//            else
//                dirs[1] = 'x';
//            if (row + 1 < maze.GetLength(0) && maze[row + 1, col] == '#') //DOWN
//                dirs[2] = 'D';
//            else
//                dirs[2] = 'x';
//            if (col - 1 >= 0 && maze[row, col - 1] == '#') //LEFT
//                dirs[3] = 'L';
//            else
//                dirs[3] = 'x';

//            int numDir = 0;
//            for (int i = 0; i < dirs.Length; i++)
//            {
//                if (dirs[i] != 'x')
//                    numDir++;
//            }

//            char[] validDirs = new char[numDir];
//            int index = 0;
//            for (int i = 0; i < dirs.Length; i++)
//            {
//                if (dirs[i] != 'x')
//                {
//                    validDirs[index] = dirs[i];
//                    index++;
//                }
//            }

//            return validDirs;
//        }
//    }

//    public class MazeStack
//    {

//        private LinkedList<Coordinate> stack = new LinkedList<Coordinate>();
//        private char[,] maze;
//        private int scale;
//        private Coordinate start;
//        private Coordinate end;
//        private int sleepMs = 0;
//        private bool[,] visted;
//        private RGBLedCanvas canvas;
//        private RGBLedMatrix matrix;
//        private Color WallColor = new Color(125, 125, 125);
//        private Color[,] history;

//        public MazeStack(int row, int col, int cornRow, int cornCol, int scale, int tickMs)
//        {
//            sleepMs = tickMs;
//            matrix = new RGBLedMatrix(32, 1, 1);
//            canvas = matrix.CreateOffscreenCanvas();
//            this.scale = scale;
//            this.visted = new bool[cornRow, cornCol];
//            this.history = new Color[cornRow, cornCol];

//            maze = new char[cornRow, cornCol];
//            for (int i = 0; i < maze.GetLength(0); i++)
//                for (int j = 0; j < maze.GetLength(0); j++)
//                    maze[i, j] = '#';

//            maze[row, col] = ' ';
//            stack.AddFirst(new Coordinate(row, col));
//        }

//        public void push(Coordinate cord)
//        {
//            //Pushes an item onto the top of this stack. This has exactly the same effect as stacks push()

//            stack.AddFirst(cord);
//        }

//        public Coordinate peek()
//        {
//            //Looks at the object at the top of this stack without removing it from the stack.

//            return stack != null && stack.Count > 0 ? stack.First.Value : null;
//        }

//        public Coordinate pop()
//        {
//            //Removes the object at the top of this stack and returns that object as the value of this function.

//            Coordinate temp = stack.First.Value;
//            stack.RemoveFirst();
//            return temp;
//        }

//        public void cut(Coordinate cord, char dir)
//        {
//            //clears the current spot in the maze
//            int row = 2 * cord.getRow() + 1;
//            int col = 2 * cord.getCol() + 1;

//            switch (dir)
//            {
//                case 'U':
//                    for (int i = 0; i <= 2; i++)
//                        maze[row - i, col] = ' ';
//                    break;
//                case 'R':
//                    for (int i = 0; i <= 2; i++)
//                        maze[row, col + i] = ' ';
//                    break;
//                case 'D':
//                    for (int i = 0; i <= 2; i++)
//                        maze[row + i, col] = ' ';
//                    break;
//                case 'L':
//                    for (int i = 0; i <= 2; i++)
//                        maze[row, col - i] = ' ';
//                    break;
//            }
//        }

//        public String toString()
//        {

//            String retVal = "";

//            for (int i = 0; i < maze.GetLength(0); i++)
//            {
//                for (int j = 0; j < maze.GetLength(0); j++)
//                {
//                    retVal += maze[i, j];
//                    if (j == maze.GetLength(0) - 1)
//                        retVal += "\n";
//                }
//            }

//            return retVal;
//        }

//        private Coordinate last;
//        public void draw()
//        {

//            canvas = matrix.SwapOnVsync(canvas);
//            int ind = 0;
//            for (int i = 0; i < maze.GetLength(0); i++)
//            {
//                ind = 0;
//                for (int j = 0; j < maze.GetLength(0); j++)
//                {
//                    if (maze[i, j] == '#' && j + 1 <= maze.GetLength(0) - 1 && maze[i, j + 1] == '#')
//                        //if there exists a solid horizontal wall, draw it
//                        //StdDraw.line((double)ind * scale, (double)i * scale, (double)(ind + 1) * scale, (double)i * scale);
//                        canvas.DrawLine(ind, i, ind + 1, i, WallColor);
//                    if (maze[i, j] == '#' && i + 1 <= maze.GetLength(0) - 1 && maze[i + 1, j] == '#')
//                        //if there exists a solid vertical wall, draw it
//                        //StdDraw.line((double)ind * scale, (double)i * scale, (double)ind * scale, (double)(i + 1) * scale);
//                        canvas.DrawLine(ind, i, ind, i + 1, WallColor);
//                    ind++;
//                    canvas = matrix.SwapOnVsync(canvas);
//                }
//            }

//        }

//        public void makeExits()
//        {
//            //ensure all of the bounds are filled
//            for (int i = 0; i < maze.GetLength(0); i++) //first row
//                maze[0, i] = '#';

//            for (int i = 0; i < maze.GetLength(0); i++) //last column
//                maze[i, 0] = '#';

//            for (int i = 0; i < maze.GetLength(0); i++) //last row
//                maze[maze.GetLength(0) - 1, i] = '#';

//            for (int i = 0; i < maze.GetLength(0); i++) //first column
//                maze[0, i] = '#';



//            Random r = new Random();
//            int entrance = maze.GetLength(0) - 2;//r.nextInt(maze.GetLength(0)-2)+1;
//            int exit = 1;//r.nextInt(maze.GetLength(0)-2)+1;

//            start = new Coordinate(entrance, 1);
//            end = new Coordinate(exit, maze.GetLength(0) - 2);

//            //we cut two spots to make sure the exits are accessible
//            maze[entrance, 0] = ' ';
//            maze[entrance, 1] = ' ';

//            maze[exit, maze.GetLength(0) - 1] = ' ';
//            maze[exit, maze.GetLength(0) - 2] = ' ';

//        }

//        public bool isEmpty() { return stack.Count == 0; }

//        public void solve()
//        {
//            //StdDraw.setPenColor(StdDraw.BOOK_BLUE);
//            //StdDraw.filledSquare(start.getCol() - 1, start.getRow(), ((double)scale / 3));
//            //canvas.SetPixel(start.getCol() - 1, start.getRow(), new Color(0, 0, 255));

//            //StdDraw.setPenColor(StdDraw.BOOK_RED);
//            //StdDraw.filledSquare(end.getCol() + 1, end.getRow(), ((double)scale / 3));
//            //canvas.SetPixel(end.getCol() + 1, end.getRow(), new Color(255, 0, 0));

//            solveMazeRecursively(maze, end.getCol() + 1, end.getRow(), -1);

//            canvas = matrix.SwapOnVsync(canvas);

//            drawAtEnd();
//        }


//        private int lrow = -1;
//        private int lcol = -1;
//        private bool solved = false;
//        private bool solveMazeRecursively(char[,] maze, int x, int y, int d)
//        {
//            canvas = matrix.SwapOnVsync(canvas);
//            if (lrow != -1)
//            {
//                //StdDraw.setPenColor(StdDraw.GRAY);
//                //StdDraw.filledSquare(lrow, lcol, ((double)scale) / (double)1.6);
//                canvas.SetPixel(lrow, lcol, new Color(0, 0, 150));
//            }
//            lrow = x;
//            lcol = y;
//            if (visted[y, x])
//            {
//                return false;
//            }
//            if (!solved) visted[y, x] = true;
//            bool ok = false;


//            for (int i = 0; i < 4 && !ok; i++)
//            {
//                if (i != d)
//                {
//                    // check for end condition
//                    if ((x == start.getCol() || x - 1 == start.getCol()) && y == start.getRow())
//                    {
//                        //visted[y,x-1] = true;
//                        //Console.WriteLine("SOLVED!");
//                        solved = true;
//                        ok = true;
//                    }
//                    // once we have found a solution, draw it as we unwind the recursion


//                    //StdDraw.setPenColor(StdDraw.CYAN);
//                    var c = new Color(255, 0, 0);
//                    canvas = matrix.SwapOnVsync(canvas);
//                    Thread.Sleep(sleepMs);
//                    canvas = matrix.SwapOnVsync(canvas);
//                    switch (i)
//                    {
//                        case 0:
//                            if ((y - 1 >= 0) && maze[y - 1, x] == ' ' && !solved)
//                            {
//                                if (maze[y - 1, x] == ' ')
//                                    canvas.SetPixel(x, y - 1, c);
//                                ok = solveMazeRecursively(maze, x, y - 1, 2);
//                            }
//                            break;
//                        case 1:
//                            if ((x + 1 < maze.GetLength(0)) && maze[y, x + 1] == ' ' && !solved)
//                            {
//                                if (maze[y, x + 1] == ' ')
//                                    canvas.SetPixel(x + 1, y, c);
//                                ok = solveMazeRecursively(maze, x + 1, y, 3);
//                            }
//                            break;
//                        case 2:
//                            if ((y + 1 < maze.GetLength(0)) && maze[y + 1, x] == ' ' && !solved)
//                            {
//                                if (maze[y + 1, x] == ' ')
//                                    canvas.SetPixel(x, y + 1, c);

//                                ok = solveMazeRecursively(maze, x, y + 1, 0);
//                            }
//                            break;
//                        case 3:
//                            if ((x - 1 >= 0) && maze[y, x - 1] == ' ' && !solved)
//                            {
//                                if (maze[y, x - 1] == ' ')
//                                    //StdDraw.filledSquare(x - 1, y, ((double)scale) / (double)1.8);
//                                    canvas.SetPixel(x - 1, y, c);

//                                ok = solveMazeRecursively(maze, x - 1, y, 1);
//                            }
//                            break;
//                    }
//                }
//            }

//            if (ok)
//            {

//                Thread.Sleep(sleepMs);
//                canvas = matrix.SwapOnVsync(canvas);
//                var c = new Color(0, 210, 0);
//                maze[y, x] = '*';
//                //StdDraw.setPenColor(StdDraw.BLACK);
//                canvas.SetPixel(start.getCol(), start.getRow(), c);
//                //StdDraw.filledSquare(start.getCol(), start.getRow(), ((double)scale) / (double)1.6);
//                //StdDraw.setPenColor(StdDraw.BLACK);
//                canvas.SetPixel(start.getCol() - 1, start.getRow(), c);
//                //StdDraw.filledSquare(start.getCol() - 1, start.getRow(), ((double)scale) / (double)1.6);


//                switch (d)
//                {
//                    case 0:
//                        //StdDraw.filledSquare(x, y - 1, ((double)scale) / (double)1.6);
//                        canvas.SetPixel(x, y - 1, c);
//                        maze[y - 1, x] = '*';
//                        break;
//                    case 1:
//                        //StdDraw.filledSquare(x + 1, y, ((double)scale) / (double)1.6);
//                        canvas.SetPixel(x + 1, y, c);
//                        maze[y, x + 1] = '*';
//                        break;
//                    case 2:
//                        //StdDraw.filledSquare(x, y + 1, ((double)scale) / (double)1.6);
//                        canvas.SetPixel(x, y + 1, c);
//                        maze[y + 1, x] = '*';
//                        break;
//                    case 3:
//                        //StdDraw.filledSquare(x - 1, y, ((double)scale) / (double)1.6);
//                        canvas.SetPixel(x - 1, y, c);
//                        maze[y, x - 1] = '*';
//                        break;
//                }
//            }
//            canvas = matrix.SwapOnVsync(canvas);
//            return ok;
//        }


//        public void drawAtEnd()
//        {
//            var green = new Color(0, 210, 0);
//            var visColor = new Color(0, 0, 150);
//            var wall = new Color(125, 125, 125);
//            for (int i = 0; i < maze.GetLength(0); i++)
//            {
//                for (int j = 0; j < maze.GetLength(0); j++)
//                {
//                    if (this.maze[i, j] == '*')
//                    {
//                        canvas.SetPixel(j, i, green);
//                    }
//                    else if (this.maze[i, j] == ' ') 
//                    {

//                    }
//                    else if (this.maze[i, j] == '#')
//                    {
//                        canvas.SetPixel(j, i, wall);
//                    }
//                    else if (this.visted[i, j] == true)
//                    {
//                        canvas.SetPixel(j, i, visColor);
//                    }

//                }
//                canvas = matrix.SwapOnVsync(canvas);
//            }
//        }
//    }
//}
