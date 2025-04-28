using System;
using System.Collections.Generic;

namespace astar1;

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
        // Initialize maze with all walls (false)
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                maze[x, y] = false;

        // Random starting point (odd coordinates)
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
                // Break the wall between (px, py) and (x, y)
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
