using System;
using System.Drawing;
using System.IO;

namespace MazeSolver
{
    class Program
    {
        static int BLUE = 2;
        static int RED = 3;
        static int BLACK = 1;
        static int WHITE = 0;

        static void Main(string[] args)
        {
            var path = "C://maze2.png";
            using (var stream = File.OpenRead(path))
            using (var image = Image.FromStream(stream))
            {
                Bitmap imgBitmap = new Bitmap(image);
                int[][] arr = BuildMatrix(imgBitmap);

                // start point, will be set to one of the RED pixels
                Point source = MazeSolver.FindPointByColor(arr, RED);                
                
                // BFS from RED to BLUE
                Point p = MazeSolver.GetPathBFS(source, arr, BLUE, BLACK);
                DrawPath(p, imgBitmap);                

                // save
                imgBitmap.Save("C://maze2After.png");
            }
        }        

        private static int[][] BuildMatrix(Bitmap imgBitmap)
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
                    }
                    else if (imgBitmap.GetPixel(i, j).ToArgb() == Color.Red.ToArgb())
                    {
                        arr[i][j] = RED; 
                    }
                    else
                        arr[i][j] = BLACK;
                }
            }
            return arr;
        }

        private static void DrawPath(Point p, Bitmap imgBitmap)
        {
            // walk back BFS path
            if (p != null)
            {
                while (p.Parent != null)
                {
                    // set pixel to green
                    imgBitmap.SetPixel(p.X, p.Y, Color.Green);
                    p = p.Parent;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find path");
            }
        }
    }
}
