using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMP2
{
    internal class Tumblers
    {
        private bool statusSwitch = false;

        private void ChangingSwithPosition(Image replaceableImage)
        {
            if (statusSwitch)
            {
                replaceableImage.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.TumblerOff, UriKind.Relative));
                statusSwitch = false;
            }
            else
            {
                replaceableImage.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.TumblerOn, UriKind.Relative));
                statusSwitch = true;
            }
        }

        public void Tumbler_Click(Image replaceableImage)
        {
            ChangingSwithPosition(replaceableImage);
        }
    }
}
