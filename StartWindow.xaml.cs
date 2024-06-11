using BMP2.Pages;
using BMP2.Weather;
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

namespace BMP2
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        int pashalka = 0;
        public StartWindow()
        {
            InitializeComponent();

            Closing += MainWindow_Closing;
        }

        private void Button_StartEmulator(object sender, RoutedEventArgs e)
        {
            NoVisible();
            button_home.Visibility = Visibility.Visible;
            Grid_Information.Content = new ChooseWeatherPage();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            if (pashalka == 6)
            {
                RectGreen.Visibility = Visibility.Visible;
                RectRed.Visibility = Visibility.Visible;
                RectBlue.Visibility = Visibility.Visible;
                pashalka = 0;
            }
            else
            {
                this.Close();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Обработчик события Closing
            // Здесь можно выполнить необходимые действия перед закрытием окна

            // Вызываем метод Shutdown для завершения работы приложения
            Application.Current.Shutdown();
        }


        private void Button_Click_Info(object sender, RoutedEventArgs e)
        {
            NoVisible();
            button_home.Visibility = Visibility.Visible;
            Grid_Information.Content = new Information();
        }

        private void Button_Click_Setting(object sender, RoutedEventArgs e)
        {
            if (pashalka == 4)
                pashalka = 5;
            else if(pashalka == 5)
            {
                NoVisible();
                button_home.Visibility = Visibility.Visible;
                Grid_Information.Content = new Setting();
            }
            else
            {
                NoVisible();
                pashalka = 0;
                button_home.Visibility = Visibility.Visible;
                Grid_Information.Content = new Setting();
            }
        }

        private void button_home_Click(object sender, RoutedEventArgs e)
        {
            if (pashalka == 5)
                pashalka = 6;

            Visible();
            Grid_Information.Content = "";
            button_home.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pashalka == 0)
                pashalka = 1;
            else if (pashalka == 3)
                pashalka = 4;
            else
                pashalka = 0;
        }

        private void Rectangle_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (pashalka == 1)
                pashalka = 2;
            else
                pashalka = 0;
        }

        private void Rectangle_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (pashalka == 2)
                pashalka = 3;
            else
                pashalka = 0;
        }

        void Visible()
        {
            FirstRect.Visibility = Visibility.Visible;
            SecondRect.Visibility = Visibility.Visible;
            Gerb.Visibility = Visibility.Visible;
        }

        void NoVisible()
        {
            FirstRect.Visibility = Visibility.Collapsed;
            SecondRect.Visibility = Visibility.Collapsed;
            Gerb.Visibility = Visibility.Collapsed;
        }
    }
}
