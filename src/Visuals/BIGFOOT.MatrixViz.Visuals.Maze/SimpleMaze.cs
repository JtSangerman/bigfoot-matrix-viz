using System;
using System.Collections.Generic;

// TODO refactor class structure and the awful code in general.
namespace BIGFOOT.MatrixViz.Visuals.Maze
{
    public class SimpleMaze
    {
        private char[,] maze;
        private readonly MazeStack _mazeStack;
        
        public SimpleMaze(int rows, int cols)
        {
            //StdDraw.setCanvasSize();
            //default the maze array to be completely full
            maze = new char[rows, cols];
            for (int i = 0; i < maze.GetLength(0); i++)
                for (int j = 0; j < maze.GetLength(0); j++)
                    maze[i, j] = '#';

            //find a random starting point on the r x c maze & clear it
            Random r = new Random();
            int rRow = r.Next(rows);
            int rCol = r.Next(cols);
            maze[rRow, rCol] = ' ';

            _mazeStack = new MazeStack( rRow, rCol, 2 * rows + 1, 2 * cols + 1);

            char[] dirs; //an empty list of valid directions in which to cut
            while (!_mazeStack.isEmpty())
            {
                //while the stack is not empty
                Coordinate coord = _mazeStack.peek(); //the cornMazerent coordinate locating at the top of the stack

                if ((dirs = canMove(coord.getRow(), coord.getCol())).Length != 0)
                {
                    //if there exists a valid direction to cut, store those positions in dirs

                    char dir = dirs[r.Next(dirs.Length)]; //then choose a random direction & cut

                    //add a stack representing coordinates in the randomized, valid direction
                    switch (dir)
                    {
                        case 'U': //UP
                            _mazeStack.push(new Coordinate(coord.getRow() - 1, coord.getCol()));
                            maze[coord.getRow() - 1, coord.getCol()] = ' ';
                            break;
                        case 'R': //RIGHT
                            _mazeStack.push(new Coordinate(coord.getRow(), coord.getCol() + 1));
                            maze[coord.getRow(), coord.getCol() + 1] = ' ';
                            break;
                        case 'D': //DOWN
                            _mazeStack.push(new Coordinate(coord.getRow() + 1, coord.getCol()));
                            maze[coord.getRow() + 1, coord.getCol()] = ' ';
                            break;
                        case 'L': //LEFT
                            _mazeStack.push(new Coordinate(coord.getRow(), coord.getCol() - 1));
                            maze[coord.getRow(), coord.getCol() - 1] = ' ';
                            break;
                        default: break; //("check dir switch"); break; //(debugging)
                    }
                    _mazeStack.cut(coord, dir); //cut that position
                }
                else _mazeStack.pop();
            }
            _mazeStack.makeExits();
        }

        public string Serialize() => _mazeStack.Serialize();


        private char[] canMove(int row, int col)
        {

            char[] dirs = new char[4];

            //4 options: UP, RIGHT, DOWN, LEFT:
            if (row - 1 >= 0 && maze[row - 1, col] == '#') //UP
                dirs[0] = 'U';
            else
                dirs[0] = 'x';
            if (col + 1 < maze.GetLength(0) && maze[row, col + 1] == '#') //RIGHT
                dirs[1] = 'R';
            else
                dirs[1] = 'x';
            if (row + 1 < maze.GetLength(0) && maze[row + 1, col] == '#') //DOWN
                dirs[2] = 'D';
            else
                dirs[2] = 'x';
            if (col - 1 >= 0 && maze[row, col - 1] == '#') //LEFT
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

    public class MazeStack
    {
        private LinkedList<Coordinate> stack = new LinkedList<Coordinate>();
        private char[,] maze;

        public MazeStack( int row, int col, int cornRow, int cornCol)
        {
            maze = new char[cornRow, cornCol];
            for (int i = 0; i < maze.GetLength(0); i++)
                for (int j = 0; j < maze.GetLength(0); j++)
                    maze[i, j] = '#';

            maze[row, col] = ' ';
            stack.AddFirst(new Coordinate(row, col));
        }

        public void push(Coordinate cord)
        {
            //Pushes an item onto the top of this stack. This has exactly the same effect as stacks push()

            stack.AddFirst(cord);
        }

        public Coordinate peek()
        {
            //Looks at the object at the top of this stack without removing it from the stack.

            return stack != null && stack.Count > 0 ? stack.First.Value : null;
        }

        public Coordinate pop()
        {
            //Removes the object at the top of this stack and returns that object as the value of this function.

            Coordinate temp = stack.First.Value;
            stack.RemoveFirst();
            return temp;
        }

        public void cut(Coordinate cord, char dir)
        {
            //clears the current spot in the maze
            int row = 2 * cord.getRow() + 1;
            int col = 2 * cord.getCol() + 1;

            switch (dir)
            {
                case 'U':
                    for (int i = 0; i <= 2; i++)
                        maze[row - i, col] = ' ';
                    break;
                case 'R':
                    for (int i = 0; i <= 2; i++)
                        maze[row, col + i] = ' ';
                    break;
                case 'D':
                    for (int i = 0; i <= 2; i++)
                        maze[row + i, col] = ' ';
                    break;
                case 'L':
                    for (int i = 0; i <= 2; i++)
                        maze[row, col - i] = ' ';
                    break;
            }
        }

        public void makeExits()
        {
            //ensure all of the bounds are filled
            for (int i = 0; i < maze.GetLength(0); i++) //first row
                maze[0, i] = '#';

            for (int i = 0; i < maze.GetLength(0); i++) //last column
                maze[i, 0] = '#';

            for (int i = 0; i < maze.GetLength(0); i++) //last row
                maze[maze.GetLength(0) - 1, i] = '#';

            for (int i = 0; i < maze.GetLength(0); i++) //first column
                maze[0, i] = '#';



            int entrance = maze.GetLength(0) - 2;//r.nextInt(maze.GetLength(0)-2)+1;
            int exit = 1;//r.nextInt(maze.GetLength(0)-2)+1;


            maze[entrance, 0] = '@';
            maze[exit, maze.GetLength(0) - 1] = '$';
        }

        public String Serialize()
        {
            String retVal = "";

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(0); j++)
                {
                    retVal += maze[i, j];
                    if (j == maze.GetLength(0) - 1)
                        retVal += "\n";
                }
            }

            return retVal;
        }

        public bool isEmpty() { return stack.Count == 0; }
    }
}
