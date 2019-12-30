using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WeekNotifier.Fonts
{
    public class FontSelectorViewModel
    {

        public FontSelectorViewModel()
        {
            FontFamilies = new List<FontFamily>(System.Windows.Media.Fonts.SystemFontFamilies);
            FontStyles = new List<FontStyle>
            {
                System.Windows.FontStyles.Normal, 
                System.Windows.FontStyles.Italic
            };
        }

        public FontWeight FontWeight { get; set; }

        public List<FontFamily> FontFamilies { get; set; }

        public List<FontStyle> FontStyles { get; set; }

    }
}
