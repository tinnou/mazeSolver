using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MazeSolver2VS
{
	class MainClass
	{

		static int BLUE = 2;
		static int RED = 3;
		static int BLACK = 1;
		static int WHITE = 0;


		static void Main(string[] args)
		{
			var path = "/Users/antoine.boyer/Projects/MazeSolver2VS/MazeSolver2VS/Properties/maze1.png";
			using (var stream = File.OpenRead(path))
			using (var image = Image.FromStream(stream))
			{
				Bitmap imgBitmap = new Bitmap(image);

				// start point, will be set to one of the RED pixels
				Point start = new Point(0, 0, null);

				int[][] arr = new int[imgBitmap.Width][];
				for (int i = 0; i < imgBitmap.Width; i++)
				{
					arr[i] = new int[imgBitmap.Height];
					for (int j = 0; j < imgBitmap.Height; j++)
					{
						if (imgBitmap.GetPixel(i, j).ToArgb() == Color.White.ToArgb())
						{
							arr[i][j] = WHITE;
						}
						else if (imgBitmap.GetPixel(i, j).ToArgb() == Color.Blue.ToArgb())
						{
							arr[i][j] = BLUE; // BLUE
						}
						else if (imgBitmap.GetPixel(i, j).ToArgb() == Color.Red.ToArgb())
						{
							arr[i][j] = RED; // RED
							start.x = i;
							start.y = j;
						}
						else
							arr[i][j] = BLACK;
					}
				}

				// DFS from RED to BLUE
				Point p = getPathBFS(start.x, start.y, arr, imgBitmap);

				// walk back DFS path
				if (p != null)
				{
					while (p.parent != null)
					{
						// set pixel to green
						imgBitmap.SetPixel(p.x, p.y, Color.Green);
						p = p.parent;
					}
				}
				else
				{
					Console.WriteLine("couldn't find path");
				}

				// save
				imgBitmap.Save("/Users/antoine.boyer/Projects/MazeSolver2VS/MazeSolver2VS/Properties/maze1After.png");
			}
		}

		public static Point getPathBFS(int x, int y, int[][] arr, Bitmap imgBitmap)
		{
			Queue<Point> q = new Queue<Point>(imgBitmap.Width * imgBitmap.Height);

			q.Enqueue(new Point(x, y, null));

			while (q.Count > 0)
			{
				Point p = q.Dequeue();

				// color all the paths
				//imgBitmap.SetPixel(p.x, p.y, Color.Green);


				if (arr[p.x][p.y] == BLUE) // BLUE
				{
					Console.WriteLine("Exit is reached!");
					return p;
				}

				if (arr[p.x][p.y] == -1)
				{
					continue;
				}

				if (isFree(p.x + 1, p.y, arr))
				{
					arr[p.x][p.y] = -1;
					Point nextP = new Point(p.x + 1, p.y, p);
					q.Enqueue(nextP);
				}

				if (isFree(p.x - 1, p.y, arr))
				{
					arr[p.x][p.y] = -1;
					Point nextP = new Point(p.x - 1, p.y, p);
					q.Enqueue(nextP);
				}

				if (isFree(p.x, p.y + 1, arr))
				{
					arr[p.x][p.y] = -1;
					Point nextP = new Point(p.x, p.y + 1, p);
					q.Enqueue(nextP);
				}

				if (isFree(p.x, p.y - 1, arr))
				{
					arr[p.x][p.y] = -1;
					Point nextP = new Point(p.x, p.y - 1, p);
					q.Enqueue(nextP);
				}
			}

			return null;
		}


		public static bool isFree(int x, int y, int[][] arr)
		{
			// outside of row
			if (x < 0 || x >= arr.Length) 
			{
				return false;
			}

			// outside of columns
			if (y < 0 || y >= arr.Length)
			{
				return false;
			}

			// wall
			if (arr[x][y] == BLACK)
			{
				return false;
			}

			// already visited
			if (arr[x][y] == -1) // visited
			{
				return false;
			}

			return true;
		}
	}
}
