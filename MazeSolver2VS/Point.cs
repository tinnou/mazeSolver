﻿using System;
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
	}
}
