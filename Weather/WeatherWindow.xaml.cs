using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BMP2.Weather
{
    /// <summary>
    /// Логика взаимодействия для Weather.xaml
    /// </summary>
    public partial class WeatherWindow : Window
    {
        public WeatherWindow()
        {
            InitializeComponent();

            Weather weather = new Weather();

            int temp;
            string season;

            weather.GetWeather(out temp, out season);


            Label_Name.Content = season;
            Label_Temp.Content = Convert.ToString(temp) + "°";

            if (season == "Солнечно")
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Sunny, UriKind.Relative));
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackSunny, UriKind.Relative));
            }
            else if (season == "Облачно")
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Cloudy, UriKind.Relative));
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackCloudy, UriKind.Relative));
            }
            else
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Winter, UriKind.Relative));
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackWinter, UriKind.Relative));
            }
        }

        public WeatherWindow(int temp, string season)
        {
            InitializeComponent();

            Label_Name.Content = season;
            Label_Temp.Content = Convert.ToString(temp) + "°";

            if (season == "Солнечно")
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Sunny, UriKind.Relative));
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackSunny, UriKind.Relative));
            }
            else if (season == "Облачно")
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Cloudy, UriKind.Relative));
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackCloudy, UriKind.Relative));
            }
            else
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Winter, UriKind.Relative));
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackWinter, UriKind.Relative));
            }
        }

        public WeatherWindow(int temp, string season, out string sourceImage)
        {
            InitializeComponent();

            Label_Name.Content = season;
            Label_Temp.Content = Convert.ToString(temp) + "°";

            if (season == "Солнечно")
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Sunny, UriKind.Relative));
                sourceImage = HelperImages.LinkToImages.Sunny;

                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackSunny, UriKind.Relative));
            }
            else if (season == "Облачно")
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Cloudy, UriKind.Relative));
                sourceImage = HelperImages.LinkToImages.Cloudy;
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackCloudy, UriKind.Relative));
            }
            else
            {
                Icon_Weather.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.Winter, UriKind.Relative));
                sourceImage = HelperImages.LinkToImages.Winter;
                Icon_Back.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.BackWinter, UriKind.Relative));
            }
        }
    }
}
