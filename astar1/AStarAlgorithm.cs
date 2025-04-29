using astar1;
using System.Collections.Generic;
using System.Linq;
using System;


namespace astar1
{
    public static class AStarAlgorithm
    {
        public static List<Cell> FindPath(Cell[,] grid, Cell start, Cell end)
        {
            var openList = new List<Cell> { start };
            var closedList = new HashSet<Cell>(); //hashset for terminating duplicates--to not use these cells from now on

            start.G = 0;
            start.H = GetHeuristic(start, end);

            while (openList.Any())
            {
                var current = openList.OrderBy(cell => cell.F).First(); //least dist from end

                if (current == end)
                {
                    return ReconstructPath(end, start);//can construct path and making it
                }

                openList.Remove(current);
                closedList.Add(current);

                foreach (var neighbor in GetNeighbors(grid, current))
                {
                    if (closedList.Contains(neighbor) || neighbor.IsObstacle) continue;

                    int tentativeG = current.G + 1;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                    else if (tentativeG >= neighbor.G)
                    {
                        continue;
                    }

                    neighbor.Previous = current;
                    neighbor.G = tentativeG;
                    neighbor.H = GetHeuristic(neighbor, end);
                }
            }

            return new List<Cell>();//couldnt find path retuning empty list
        }

        public static IEnumerable<AstarStepState> PathStepIterator(Cell[,] grid, Cell start, Cell end)
        {
            var openList = new List<Cell> { start };
            var closedList = new HashSet<Cell>();
            start.G = 0;
            start.H = GetHeuristic(start, end);

            while (openList.Any())
            {
                var current = openList.OrderBy(cell => cell.F).First();

                yield return new AstarStepState
                {
                    OpenList = new List<Cell>(openList),
                    ClosedList = new HashSet<Cell>(closedList),
                    Current = current
                };

                if (current == end)
                    yield break;

                openList.Remove(current);
                closedList.Add(current);

                foreach (var neighbor in GetNeighbors(grid, current))
                {
                    if (closedList.Contains(neighbor) || neighbor.IsObstacle) continue;

                    int tentativeG = current.G + 1;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                    else if (tentativeG >= neighbor.G)
                    {
                        continue;
                    }

                    neighbor.Previous = current;
                    neighbor.G = tentativeG;
                    neighbor.H = GetHeuristic(neighbor, end);
                }
            }
        }


        private static List<Cell> ReconstructPath(Cell end, Cell start)
        {
            var path = new List<Cell>();
            var current = end;
            while (true)    // changed  from null pointer to actual starting position check
            {
                if (current.Col == start.Col && current.Row == start.Row)
                    break;
                path.Add(current);
                current = current.Previous;
            }//adding path elements to path and reversing it so it goes from start to end
            ///path.Add(start);
            path.Reverse();
            return path;
        }

        private static IEnumerable<Cell> GetNeighbors(Cell[,] grid, Cell current)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            var directions = new (int, int)[]
            {
                (-1, 0), (1, 0), (0, -1), (0, 1)
            };

            foreach (var (dr, dc) in directions)
            {
                int newRow = current.Row + dr;
                int newCol = current.Col + dc;

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                {
                    yield return grid[newRow, newCol];
                }
            }
        }

        private static int GetHeuristic(Cell a, Cell b)
        {
            return System.Math.Abs(a.Row - b.Row) + System.Math.Abs(a.Col - b.Col);//distance formula
        }
    }

    public class AstarStepState
    {
        public List<Cell> OpenList { get; set; } = new List<Cell>();
        public HashSet<Cell> ClosedList { get; set; } = new HashSet<Cell>();
        public Cell Current { get; set; }
    }

    

    public class MazeGenerator
    {
        private int width, height;
        private bool[,] maze;
        private Random rand = new Random();

        public MazeGenerator(int width, int height)
        {
            this.width = (width % 2 == 1) ? width : width + 1;
            this.height = (height % 2 == 1) ? height : height + 1;
            maze = new bool[this.width, this.height];
        }

        public bool[,] Generate()
        {
            //walls--false
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    maze[x, y] = false;

            int startX = rand.Next(width / 2) * 2 + 1;
            int startY = rand.Next(height / 2) * 2 + 1;
            maze[startX, startY] = true;

            List<(int x, int y, int px, int py)> walls = new List<(int, int, int, int)>();

            foreach (var (nx, ny) in GetNeighbors(startX, startY))
                walls.Add((nx, ny, startX, startY));

            while (walls.Count > 0)
            {
                int index = rand.Next(walls.Count);
                var (x, y, px, py) = walls[index];
                walls.RemoveAt(index);

                if (!maze[x, y])
                {
                    maze[(x + px) / 2, (y + py) / 2] = true;
                    maze[x, y] = true;

                    foreach (var (nx, ny) in GetNeighbors(x, y))
                    {
                        if (!maze[nx, ny])
                            walls.Add((nx, ny, x, y));
                    }
                }
            }

            return maze;
        }

        private IEnumerable<(int, int)> GetNeighbors(int x, int y)
        {
            int[] dx = { -2, 2, 0, 0 };
            int[] dy = { 0, 0, -2, 2 };

            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (nx > 0 && nx < width && ny > 0 && ny < height)
                    yield return (nx, ny);
            }
        }
    }

}
