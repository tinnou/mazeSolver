using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    public class MazeSolver
    {
        static int[] X_MOVE = { -1, 1, 0, 0 };
        static int[] Y_MOVE = { 0, 0, 1, -1 };
        static int VISITED = -1;

        public static Point FindPointByColor(int[][] arr, int color)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == color)
                    {
                        var p = new Point(i, j, null);
                        return p;
                    }
                }
            }
            return null;
        }

        public static Point GetPathBFS(Point source, int[][] arr, Bitmap imgBitmap, int dest, int wall)
        {
            Queue<Point> q = new Queue<Point>();

            q.Enqueue(source);

            while (q.Count > 0)
            {
                Point p = q.Dequeue();

                // path found
                if (arr[source.X][source.Y] == dest)
                {
                    return p;
                }

                if (arr[source.X][source.X] == VISITED) continue;

                for (int i = 0; i < 4; i++)
                {
                    int nextX = p.X + X_MOVE[i];
                    int nextY = p.Y + Y_MOVE[i];
                    if (IsValid(nextX, nextY, arr, wall))
                    {
                        arr[p.X][p.Y] = VISITED;
                        Point nextP = new Point(nextX, nextY, p);
                        q.Enqueue(nextP);
                    }
                }
            }

            return null;
        }

        public static bool IsValid(int x, int y, int[][] arr, int wall)
        {
            // outside of row
            if (x < 0 || x >= arr.Length) return false;

            // outside of columns
            if (y < 0 || y >= arr.Length) return false;

            // wall
            if (arr[x][y] == wall) return false;

            // already visited
            if (arr[x][y] == VISITED) return false;

            return true;
        }
    }
}
