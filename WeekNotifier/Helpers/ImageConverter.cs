using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WeekNotifier.Helpers
{
    public class ImageConverter
    {
        public static void ToIcon(ImageSource imageSource)
        {
            var ms = new MemoryStream();
            var ic = new ImageConverter();

            var bitmap = imageSource as BitmapSource;
            
        }
    }
}
