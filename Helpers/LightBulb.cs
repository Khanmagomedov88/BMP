using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace BMP2.Helpers
{
    /// <summary>
    /// Лампочки - индикаторы
    /// </summary>
    internal class LightBulb
    {
        private void ChangingColorLight(Button lightTurnedOff, Button lightTurnOn)
        {
            if (lightTurnOn.Visibility == Visibility.Collapsed)
            {
                lightTurnedOff.Visibility = Visibility.Collapsed;
                lightTurnOn.Visibility = Visibility.Visible;
            }
            else
            {
                lightTurnedOff.Visibility = Visibility.Visible;
                lightTurnOn.Visibility = Visibility.Collapsed;
            }
        }
    }
}
