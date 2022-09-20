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
        }

        void MainMenu() 
        {
            Console.WriteLine("Write the path of your picture");
            string path = Console.ReadLine();
            Console.Clear();
            using var image = new MagickImage(path);
            Console.WriteLine("");
        }

        
    }
}
