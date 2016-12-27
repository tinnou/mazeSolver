using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver2VS
{
    public class BFSData
    {
        public Queue<Point> Path { get; set; }
        public Dictionary<Point, Point> Visited { get; set; }

        public BFSData(Point p)
        {
            Path = new Queue<Point>();
            Path.Enqueue(p);
            Visited = new Dictionary<Point, Point>();
            Visited.Add(p, p);
        }

        public bool IsFinished()
        {
            return Path.Count > 0 ? false : true;
        }
    }
}
