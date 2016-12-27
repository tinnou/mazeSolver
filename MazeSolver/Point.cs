namespace MazeSolver
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point Parent { get; set; }

        public Point(int x, int y, Point parent)
        {
            this.X = x;
            this.Y = y;
            this.Parent = parent;
        }
    }
}
