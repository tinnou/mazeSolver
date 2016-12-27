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
        static int VISITED_FROM_SOURCE = -1;
        static int VISITED_FROM_DEST = -2;

		static void Main(string[] args)
		{
			var path = "C://maze1.png";
			using (var stream = File.OpenRead(path))
			using (var image = Image.FromStream(stream))
			{
				Bitmap imgBitmap = new Bitmap(image);

				// start point, will be set to one of the RED pixels
				Point source = new Point(0, 0, null);
                Point destination = new Point(0, 0, null);
                

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
							arr[i][j] = BLUE;
                            destination.x = i;
                            destination.y = j;
						}
						else if (imgBitmap.GetPixel(i, j).ToArgb() == Color.Red.ToArgb())
						{
							arr[i][j] = RED; 
							source.x = i;
							source.y = j;
						}
						else
							arr[i][j] = BLACK;
					}
				}
                /*
				// BFS from RED to BLUE
				Point p = getPathBFS(source.x, source.y, arr, imgBitmap);
               

				// walk back BFS path
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
				imgBitmap.Save("C://maze1After.png");
                */
                var sourcePath = new Queue<Point>();
                sourcePath.Enqueue(source);
                var destinationPath = new Queue<Point>();
                destinationPath.Enqueue(destination);

                var sourceVisited = new Dictionary<Point, Point>();
                sourceVisited.Add(source, source);
                var destinationVisited = new Dictionary<Point, Point>();
                destinationVisited.Add(destination, destination);

                while (sourcePath.Count > 0 && destinationPath.Count > 0)
                {
                    var p = sourcePath.Dequeue();
                    Point collision = Search(arr, p, sourcePath, VISITED_FROM_SOURCE, sourceVisited);
                    if (collision != null)
                    {
                        MergePath(collision, sourceVisited, destinationVisited, imgBitmap);
                        break;
                    }                        

                    p = destinationPath.Dequeue();
                    collision = Search(arr, p, destinationPath, VISITED_FROM_DEST, destinationVisited);
                    if(collision != null)
                    {
                        MergePath(collision, sourceVisited, destinationVisited, imgBitmap);
                        break;
                    }
                }

                imgBitmap.Save("C://maze1After.png");

            }
        }

        private static void MergePath(Point collision, Dictionary<Point, Point> sourceVisited, Dictionary<Point, Point> destinationVisited, Bitmap imgBitmap)
        {
            Point source = sourceVisited[collision];
            Point dest = destinationVisited[collision];

            while (source != null)
            {
                // set pixel to green
                imgBitmap.SetPixel(source.x, source.y, Color.Green);
                source = source.parent;
            }


            while (dest != null)
            {
                // set pixel to green
                imgBitmap.SetPixel(dest.x, dest.y, Color.Green);
                dest = dest.parent;
            }
        }

        public static Point Search(int[][] arr, Point p, Queue<Point> q, int visited, Dictionary<Point, Point> visitedSet)
        {
            int collision = visited == VISITED_FROM_SOURCE ? VISITED_FROM_DEST : VISITED_FROM_SOURCE;

            if (arr[p.x][p.y] == collision) return p;   
            if (arr[p.x][p.y] == visited) return null;
            

            //try
            //{
            //    visitedSet.Add(p, p);
            //}
            //catch(Exception ex)
            //{

            //}
            

            if (IsFree(p.x + 1, p.y, arr, visited))
            {
                arr[p.x][p.y] = visited;
                
                Point nextP = new Point(p.x + 1, p.y, p);
                if (!visitedSet.ContainsKey(nextP))
                {
                    visitedSet.Add(nextP, nextP);
                }
                q.Enqueue(nextP);
            }

            if (IsFree(p.x - 1, p.y, arr, visited))
            {
                arr[p.x][p.y] = visited;
                Point nextP = new Point(p.x - 1, p.y, p);
                if (!visitedSet.ContainsKey(nextP))
                {
                    visitedSet.Add(nextP, nextP);
                }
                q.Enqueue(nextP);
            }

            if (IsFree(p.x, p.y + 1, arr, visited))
            {
                arr[p.x][p.y] = visited;
                Point nextP = new Point(p.x, p.y + 1, p);
                if (!visitedSet.ContainsKey(nextP))
                {
                    visitedSet.Add(nextP, nextP);
                }
                q.Enqueue(nextP);
            }

            if (IsFree(p.x, p.y - 1, arr, visited))
            {
                arr[p.x][p.y] = visited;
                Point nextP = new Point(p.x, p.y - 1, p);
                if (!visitedSet.ContainsKey(nextP))
                {
                    visitedSet.Add(nextP, nextP);
                }
                q.Enqueue(nextP);
            }

            return null;
        }
		public static Point getPathBFS(int x, int y, int[][] arr, Bitmap imgBitmap)
		{
			Queue<Point> q = new Queue<Point>(imgBitmap.Width * imgBitmap.Height);

			q.Enqueue(new Point(x, y, null));

			while (q.Count > 0)
			{
				Point p = q.Dequeue();              

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


		public static bool IsFree(int x, int y, int[][] arr, int visited)
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
			if (arr[x][y] == visited) // visited
			{
				return false;
			}

			return true;
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
