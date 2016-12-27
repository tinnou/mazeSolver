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
			var imgPath = "C://maze2.png";
			using (var stream = File.OpenRead(imgPath))
			using (var image = Image.FromStream(stream))
			{
				Bitmap imgBitmap = new Bitmap(image);
                
				Point source = new Point(0, 0, null);
                Point destination = new Point(0, 0, null);  				
                int[][] arr = BuildMetrix(imgBitmap, ref source, ref destination);
                var path = MazeSolver.FindPath(arr, source, destination);
                if (path == null)
                {
                    Console.WriteLine("Couldn't find a path");
                    return;
                }
                DrawPath(imgBitmap, path);
                imgBitmap.Save("C://maze2After.png");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgBitmap"></param>
        /// <param name="path"></param>
        private static void DrawPath(Bitmap imgBitmap, LinkedList<Point> path)
        {
            foreach (var p in path)
                imgBitmap.SetPixel(p.X, p.Y, Color.Green);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgBitmap"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static int[][] BuildMetrix(Bitmap imgBitmap, ref Point source, ref Point destination)
        {
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
                        destination.X = i;
                        destination.Y = j;
                    }
                    else if (imgBitmap.GetPixel(i, j).ToArgb() == Color.Red.ToArgb())
                    {
                        arr[i][j] = RED;
                        source.X = i;
                        source.Y = j;
                    }
                    else
                    {
                        arr[i][j] = BLACK;
                    }                        
                }
            }
            return arr;
        }
                
    }
}
