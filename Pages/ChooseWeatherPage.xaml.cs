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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMP2.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChooseWeatherPage.xaml
    /// </summary>
    public partial class ChooseWeatherPage : Page
    {
        public ChooseWeatherPage()
        {
            InitializeComponent();
        }

        private void Sunny_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow("Солнечно");
            main.Show();
        }

        private void Winter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow("Снег");
            main.Show();
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
