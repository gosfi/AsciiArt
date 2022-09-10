using System;
using ImageMagick;
namespace AsciiArt
{
    internal class Program
    {

        static void Main(string[] args)
        {
            MagickNET.Initialize();

            using var image = new MagickImage("C:\\Users\\Sam\\Desktop\\ascii-pineapple.jpg");
            Console.WriteLine("successfully constructed pixel matrix!");
            Console.WriteLine($"Matrix size: {image.Width} x {image.Height}");
            using IPixelCollection<ushort> pixels = image.GetPixels();
            Color?[,] pixelMatrix = PixelMatrix(image.Width, image.Height, pixels);
            Console.WriteLine("");
            
        }

        static Color?[,] PixelMatrix(int width, int height, IPixelCollection<ushort> pixels)
        {

            Color?[,] colors = new Color?[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = pixels.GetPixel(x, y).ToColor();
                    colors[x, y] = new((byte)pixel.R, (byte)pixel.G, (byte)pixel.B);
                }
            }

            return colors;
        }
    }
}