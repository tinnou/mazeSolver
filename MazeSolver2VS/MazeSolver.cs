using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver2VS
{
    
    public class MazeSolver
    {
        static int[] X_Move = { -1, 1, 0, 0 };
        static int[] Y_Move = { 0, 0, 1, -1 };
        static int BLACK = 1;

        /// <summary>
        /// Bidirectional breadth-first search
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static LinkedList<Point> FindPath(int[][] arr, Point source, Point destination)
        {
            var sourceData = new BFSData(source);
            var destData = new BFSData(destination);

            while (!sourceData.IsFinished() && !destData.IsFinished())
            {
                var p = sourceData.Path.Dequeue();
                Point collision = Search(arr, p, sourceData, destData);
                if (collision != null)
                    return MergePath(collision, sourceData, destData);

                p = destData.Path.Dequeue();
                collision = Search(arr, p, destData, sourceData);
                if (collision != null)
                    return MergePath(collision, sourceData, destData);
            }

            return null;
        }
        /// <summary>
        /// Merge paths from source to collision, collision to destination
        /// </summary>
        /// <param name="collision"></param>
        /// <param name="sourceData"></param>
        /// <param name="destData"></param>
        /// <returns></returns>
        public static LinkedList<Point> MergePath(Point collision, BFSData sourceData, BFSData destData)
        {
            var path = new LinkedList<Point>();
            Point source = sourceData.Visited[collision];
            Point dest = destData.Visited[collision];

            while (source != null)
            {
                path.AddFirst(source);
                source = source.Parent;
            }

            while (dest != null)
            {
                path.AddFirst(dest);
                dest = dest.Parent;
            }
            return path;
        }

        /// <summary>
        /// One level search by (x-1, y), (x+1, y), (x, y+1), (x, y-1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="p"></param>
        /// <param name="data"></param>
        /// <param name="visited"></param>
        /// <returns></returns>
        public static Point Search(int[][] arr, Point p, BFSData data, BFSData other)
        {            
            if (other.Visited.ContainsKey(p)) return p;

            for (int i = 0; i < 4; i++)
            {
                Point nextP = new Point(p.X + X_Move[i], p.Y + Y_Move[i], p);
                if (IsValid(nextP, arr, data))
                {                    
                    if (!data.Visited.ContainsKey(nextP))
                        data.Visited.Add(nextP, nextP);

                    data.Path.Enqueue(nextP);
                }
            }
            return null;
        }
        /// <summary>
        /// Check if a path is valid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="arr"></param>
        /// <param name="visited"></param>
        /// <returns></returns>
        public static bool IsValid(Point p, int[][] arr, BFSData data)
        {
            // outside of row
            if (p.X < 0 || p.X >= arr.Length) return false;

            // outside of columns
            if (p.Y < 0 || p.Y >= arr.Length) return false;

            // wall
            if (arr[p.X][p.Y] == BLACK) return false;

            // already visited
            if (data.Visited.ContainsKey(p)) return false;

            return true;
        }
    }
}
