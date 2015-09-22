using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CavemanTools.Extensions
{
    public static class Images
    {
       public static Image Resize(this Image image,Size size)
       {
           if (image == null) throw new ArgumentNullException("image");
           int sourceWidth = image.Width;
           int sourceHeight = image.Height;

           float nPercent = 0;
           float nPercentW = 0;
           float nPercentH = 0;

           nPercentW = ((float)size.Width / (float)sourceWidth);
           nPercentH = ((float)size.Height / (float)sourceHeight);

           if (nPercentH < nPercentW)
               nPercent = nPercentH;
           else
               nPercent = nPercentW;

           int destWidth = (int)(sourceWidth * nPercent);
           int destHeight = (int)(sourceHeight * nPercent);

           Bitmap b = new Bitmap(destWidth, destHeight);
           
           using (Graphics g = Graphics.FromImage(b))
           {
               g.InterpolationMode = InterpolationMode.HighQualityBicubic;
               g.DrawImage(image, 0, 0, destWidth, destHeight);
               g.Dispose();
           }           
           return b;           
       }
    }
}