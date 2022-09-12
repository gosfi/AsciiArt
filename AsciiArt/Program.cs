using System;
using System.Drawing;
using ImageMagick;
namespace AsciiArt
{
    internal class Program
    {
        static string asciiChars = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'.";
        static int newWidth, newHeight;
        static void Main(string[] args)
        {
            MagickNET.Initialize();
            char[] reversed = asciiChars.ToCharArray();
            Array.Reverse(reversed);
            asciiChars = new string(reversed);
            Console.WriteLine("Write the path of your picture");
            string path = Console.ReadLine();
            using var image = new MagickImage(path);
            newWidth = image.Width;
            newHeight = image.Height;
            using IPixelCollection<ushort> pixels = image.GetPixels();
            Color[,] pixelMatrix = PixelMatrix(pixels);
            byte[,] brightnessMatrix = new byte[newWidth, newHeight];
            brightnessMatrix = GetBrightnessMatrix(pixelMatrix, brightnessMatrix);

            PrintAsciiArt(brightnessMatrix);
        }

        static Color[,] PixelMatrix(IPixelCollection<ushort> pixels)
        {

            Color[,] colors = new Color[newWidth, newHeight];
            for (int x = 0; x < colors.GetLength(0); x++)
            {
                for (int y = 0; y < colors.GetLength(1); y++)
                {
                    var pixel = pixels.GetPixel(x, y).ToColor();
                    colors[x, y] = Color.FromArgb((byte)pixel.A, (byte)pixel.R, (byte)pixel.G, (byte)pixel.B);
                }
            }

            return colors;
        }

        static byte SetBrightness(Color color)
        {
            return (byte)(0.21f * color.R + 0.72f * color.G + 0.07f * color.B);
        }

        static byte[,] GetBrightnessMatrix(Color[,] pixelMatrix, byte[,] brightMatrix)
        {
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    brightMatrix[x, y] = SetBrightness(pixelMatrix[x, y]);
                }
            }

            return brightMatrix;
        }

        static void PrintAsciiArt(byte[,] brightnessMatrix)
        {
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    float charPos = MathF.Ceiling((brightnessMatrix[x, y] / 255f) * 64f);


                    string asciiChar = new string(asciiChars[(byte)charPos], 3);
                    Console.Write(asciiChar);

                }
                Console.WriteLine();
            }
        }
    }
}