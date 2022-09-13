using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class Main
    {

        public void Initialize()
        {
            MagickNET.Initialize();
            Console.WriteLine("Write the path of your picture");
            string path = Console.ReadLine();
            using var image = new MagickImage(path);
            using IPixelCollection<ushort> pixels = image.GetPixels();
            Color[,] pixelMatrix = AsciiProcessing.instance.PixelMatrix(pixels, image.Width, image.Height);
            byte[,] brightnessMatrix = new byte[image.Width, image.Height];
            brightnessMatrix = AsciiProcessing.instance.GetBrightnessMatrix(pixelMatrix, brightnessMatrix, image.Width, image.Height);
            AsciiProcessing.instance.PrintAsciiArt(brightnessMatrix, image.Width, image.Height);
        }
    }
}
