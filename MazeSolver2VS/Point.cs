using System;
namespace MazeSolver2VS
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
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var other = obj as Point;
            return (X == other.X && Y == other.Y);
        }
        public override int GetHashCode()
        {
            return X ^ Y;
        }
        
    }
}
