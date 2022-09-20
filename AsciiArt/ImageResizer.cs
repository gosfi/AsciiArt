using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class ImageResizer
    {
        public static ImageResizer instance = new ImageResizer();

        private ImageResizer() { }
        public static ImageResizer GetInstance()
        {
            return instance;
        }

        void ResizeImage(MagickImage image, string path, int width, int height)
        {
            MagickGeometry size = new MagickGeometry(width, height);
            size.IgnoreAspectRatio = true;

            image.Resize(size);
            image.Write(path);
        }
    }
}
