using System;
namespace MazeSolver2VS
{
	public class Point
	{
		public int x;
		public int y;
		public Point parent;

		public Point(int x, int y, Point parent)
		{
			this.x = x;
			this.y = y;
			this.parent = parent;
		}

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var other = obj as Point;
            return (x == other.x && y == other.y);
        }
        public override int GetHashCode()
        {
            return x ^ y;
        }
    }
}
