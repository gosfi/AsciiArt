using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class AsciiProcessing
    {
        public static string asciiChars = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'.";
        public static AsciiProcessing instance = new AsciiProcessing();

        private AsciiProcessing() { }
        public static AsciiProcessing GetInstance()
        {
            return instance;
        }

        public Color[,] PixelMatrix(IPixelCollection<ushort> pixels, int width, int height)
        {

            Color[,] colors = new Color[width, height];
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

        public byte SetBrightness(Color color)
        {
            return (byte)(0.21f * color.R + 0.72f * color.G + 0.07f * color.B);
        }

        public byte[,] GetBrightnessMatrix(Color[,] pixelMatrix, byte[,] brightMatrix, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    brightMatrix[x, y] = SetBrightness(pixelMatrix[x, y]);
                }
            }

            return brightMatrix;
        }

        public void PrintAsciiArt(byte[,] brightnessMatrix, int width, int height)
        {
            asciiChars = GetAsciiString();
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    float charPos = MathF.Ceiling((brightnessMatrix[x, y] / 255f) * 64f);


                    string asciiChar = new string(asciiChars[(byte)charPos], 3);
                    Console.Write(asciiChar);

                }
                Console.WriteLine();
            }
        }

        public string GetAsciiString()
        {
            char[] reversed = asciiChars.ToCharArray();
            Array.Reverse(reversed);
            asciiChars = new string(reversed);
            return asciiChars;
        }
    }
}
