using astar1;
using System.Collections.Generic;
using System.Linq;


namespace astar1
{
    public static class AStarAlgorithm
    {
        public static List<Cell> FindPath(Cell[,] grid, Cell start, Cell end)
        {
            var openList = new List<Cell> { start };
            var closedList = new HashSet<Cell>();

            start.G = 0;
            start.H = GetHeuristic(start, end);

            while (openList.Any())
            {
                var current = openList.OrderBy(cell => cell.F).First();

                if (current == end)
                {
                    return ReconstructPath(end, start);
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

            return new List<Cell>();
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
            }
            //path.Add(start);
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
            return System.Math.Abs(a.Row - b.Row) + System.Math.Abs(a.Col - b.Col);
        }
    }
}
