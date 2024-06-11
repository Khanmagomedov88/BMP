using BMP2.Weather;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static BMP2.Weather.Weather;

namespace BMP2
{
    public partial class MainWindow : Window
    {
        RotateTransform transDV = new RotateTransform(10);
        private DispatcherTimer timerDV;

        RotateTransform transTah = new RotateTransform(0);
        private DispatcherTimer timerTah;

        RotateTransform transVolt = new RotateTransform(0);
        private DispatcherTimer timerVolt;

        RotateTransform transTempEng = new RotateTransform(25);
        private DispatcherTimer timerTempEng;

        Weather.Weather weather;

        WeatherWindow weatherWindow;

        #region Левые тумблеры КИП
        Tumblers tumblerATD = new Tumblers();
        Tumblers tumbler2 = new Tumblers();
        Tumblers tumblerGPK = new Tumblers();
        Tumblers tumblerBCN = new Tumblers();
        Tumblers tumblerEnginCooling = new Tumblers();
        Tumblers tumblerPumpingWaterNos = new Tumblers();
        Tumblers tumblerPumpingWaterKorma = new Tumblers();
        Tumblers tumblerPAZ = new Tumblers();
        private static readonly Tumblers tumblers = new Tumblers();
        #endregion

        Tumblers tumblerDenNoch = tumblers;

        #region Правые тумлеры КИП
        Tumblers tumblerFaraSMY = new Tumblers();
        Tumblers tumblerFaraTVN = new Tumblers();
        Tumblers tumblerNagnetatel = new Tumblers();
        Tumblers tumblerGabarit = new Tumblers();
        Tumblers tumblerSvecha = new Tumblers();
        Tumblers tumblerObogrevDvigatel = new Tumblers();
        Tumblers tumblerPlav = new Tumblers();
        Tumblers tumblerOffBatareya = new Tumblers();
        #endregion

        CombatMachine BMP = new CombatMachine();

        public MainWindow(string weatherName)
        {
            weather = new Weather.Weather(weatherName);
            InitializeComponent();
            ChangeButtonColor();
            InitializeTimer();
            this.KeyDown += new KeyEventHandler(OKP);

            BMP.SetTemperatureWeatherAndEngine(weather.GetTemperature());

            if (weather.GetTemperature() > 5)
            {
                transTempEng.Angle = -8;
                StrelkaTempEng.RenderTransform = transTempEng;
            }
        }


        public MainWindow()
        {
            weather = new Weather.Weather();
            InitializeComponent();
            InitializeTimer();
            ChangeButtonColor();
            this.KeyDown += new KeyEventHandler(OKP);

            BMP.SetTemperatureWeatherAndEngine(weather.GetTemperature());

            if (weather.GetTemperature() > 5)
            {
                transTempEng.Angle = -8;
                StrelkaTempEng.RenderTransform = transTempEng;
            }
        }

        private void InitializeTimer()
        {
            weather.GetWeather(out int temp, out string season);

            weatherWindow = new WeatherWindow(temp, season, out string sourceImage);
            Weather_Image.Source = new BitmapImage(new Uri(sourceImage, UriKind.Relative));
            Weather_Label.Content = temp + "°";

            timerDV = new DispatcherTimer();
            timerDV.Interval = TimeSpan.FromMilliseconds(50);
            timerDV.Tick += Timer_DV;

            timerTah = new DispatcherTimer();
            timerTah.Interval = TimeSpan.FromMilliseconds(15);
            timerTah.Tick += Timer_Tah;

            timerVolt = new DispatcherTimer();
            timerVolt.Interval = TimeSpan.FromMilliseconds(10);
            timerVolt.Tick += Timer_Volt;

            timerTempEng = new DispatcherTimer();
            timerTempEng.Interval = TimeSpan.FromMilliseconds(450);
            timerTempEng.Tick += Timer_TempEng;
        }

        private void Timer_DV(object sender, EventArgs e)
        {
            if (BMP.GetBatteryStatus())
            {
                if (transDV.Angle > 90)
                {
                    timerDV.Stop();
                }
                else
                {
                    ProgressBar_MasloDV.Value++;
                    transDV.Angle += 1;
                    StrelkaDV.RenderTransform = transDV;
                }
            }
        }

        private void Timer_Tah(object sender, EventArgs e)
        {
            if (BMP.GetEngineStatus() && BMP.GetBatteryStatus())
            {
                if (transTah.Angle < 86)
                    transTah.Angle += 1;
                else
                {
                    transTah.Angle -= 4;
                }
            }
            else
            {
                if (transTah.Angle > 0)
                {
                    transTah.Angle -= 1;
                }
            }
            StrelkaTahom.RenderTransform = transTah;
        }

        private void Timer_Volt(object sender, EventArgs e)
        {
            if (BMP.GetBatteryStatus())
            {
                if (transVolt.Angle < 45)
                    transVolt.Angle += 2;
            }
            else
            {
                if (transVolt.Angle > 0)
                {
                    transVolt.Angle -= 2;
                }
            }
            StrelkaVolt.RenderTransform = transVolt;
        }

        private void Timer_TempEng(object sender, EventArgs e)
        {
            if (BMP.GetStatusEngineHeat(weather.GetTemperature()))
            {
                if (transTempEng.Angle > -32)
                {
                    transTempEng.Angle -= 1;
                }
            }

            StrelkaTempEng.RenderTransform = transTempEng;
        }


        private void TumblerTDA_Click(object sender, RoutedEventArgs e)
        {
            tumblerATD.Tumbler_Click(ImageTumblerTDA);
        }
        private void Tumbler2_Click(object sender, RoutedEventArgs e)
        {
            tumbler2.Tumbler_Click(ImageTumbler2);
        }

        private void TumblerGPK_Click(object sender, RoutedEventArgs e)
        {
            tumblerGPK.Tumbler_Click(ImageTumblerGPK);
        }

        private void TumblerBCN_Click(object sender, RoutedEventArgs e)
        {
            tumblerBCN.Tumbler_Click(ImageTumblerBCN);

            BMP.ToggleBcnState();
        }

        private void TumblerEnginCooling_Click(object sender, RoutedEventArgs e)
        {
            tumblerEnginCooling.Tumbler_Click(ImageTumblerEnginCooling);
        }

        private void TumblerPumpingWaterNos_Click(object sender, RoutedEventArgs e)
        {
            tumblerPumpingWaterNos.Tumbler_Click(ImageTumblerPumpingWaterNos);
        }

        private void TumblerPumpingWaterKorma_Click(object sender, RoutedEventArgs e)
        {
            tumblerPumpingWaterKorma.Tumbler_Click(ImageTumblerPumpingWaterKorma);
        }

        private void TumblerPAZ_Click(object sender, RoutedEventArgs e)
        {
            tumblerPAZ.Tumbler_Click(ImageTumblerPAZ);
        }

        private void TumblerFaraSMY_Click(object sender, RoutedEventArgs e)
        {
            tumblerFaraSMY.Tumbler_Click(ImageTumblerFaraSMY);
        }

        private void TumblerFaraTVN_Click(object sender, RoutedEventArgs e)
        {
            tumblerFaraTVN.Tumbler_Click(ImageTumblerFaraTVN);
        }

        private void TumblerNagnetatel_Click(object sender, RoutedEventArgs e)
        {
            tumblerNagnetatel.Tumbler_Click(ImageTumblerNagnetatel);
        }

        private void TumblerGabarit_Click(object sender, RoutedEventArgs e)
        {
            tumblerGabarit.Tumbler_Click(ImageTumblerGabarit);
        }

        private void TumblerSvecha_Click(object sender, RoutedEventArgs e)
        {
            tumblerSvecha.Tumbler_Click(ImageTumblerSvecha);
            BMP.ToggleSparkState();
        }

        private void TumblerObogrevDvigatel_Click(object sender, RoutedEventArgs e)
        {
            tumblerObogrevDvigatel.Tumbler_Click(ImageTumblerObogrevDvigatel);
            BMP.ToggleEngineHeatingState(weather.GetTemperature());

            if (BMP.GetStatusEngineHeat(weather.GetTemperature()) && BMP.GetSparkStatus())
            {
                timerTempEng.Start();
            }
            else
            {
                timerTempEng.Stop();
            }
        }

        private void TumblerPlav_Click(object sender, RoutedEventArgs e)
        {
            tumblerPlav.Tumbler_Click(ImageTumblerPlav);
        }

        private void TumblerOffBatareya_Click(object sender, RoutedEventArgs e)
        {
            tumblerOffBatareya.Tumbler_Click(ImageTumblerOffBatareya);

            BMP.ToggleBatteryState();

            if (BMP.GetBatteryStatus() == true && BMP.GetPanelLightningStatus())
            {
                OnCentralPanel();
            }
            else
            {
                OffCentralPanel();
            }

            if (!timerVolt.IsEnabled)
            {
                timerVolt.Start();
            }
        }


        public void OnCentralPanel()
        {
            IndOff1.Visibility = Visibility.Collapsed;
            IndOff2.Visibility = Visibility.Collapsed;
            IndOff3.Visibility = Visibility.Collapsed;
            IndOff4.Visibility = Visibility.Collapsed;

            IndOn1.Visibility = Visibility.Visible;
            IndOn2.Visibility = Visibility.Visible;
            IndOn3.Visibility = Visibility.Visible;
            IndOn4.Visibility = Visibility.Visible;

            if (!BMP.GetDoorStatus())
            {
                IndDoorOff.Visibility = Visibility.Collapsed;
                IndDoorOn.Visibility = Visibility.Visible;
            }
        }

        public void OffCentralPanel()
        {
            IndOn1.Visibility = Visibility.Collapsed;
            IndOn2.Visibility = Visibility.Collapsed;
            IndOn3.Visibility = Visibility.Collapsed;
            IndOn4.Visibility = Visibility.Collapsed;

            IndOff1.Visibility = Visibility.Visible;
            IndOff2.Visibility = Visibility.Visible;
            IndOff3.Visibility = Visibility.Visible;
            IndOff4.Visibility = Visibility.Visible;

            IndDoorOff.Visibility = Visibility.Visible;
            IndDoorOn.Visibility = Visibility.Collapsed;
        }

        private void TumblerDenNoch_Click(object sender, RoutedEventArgs e)
        {
            tumblerDenNoch.Tumbler_Click(ImageTumblerDenNoch);

            BMP.TooglePanelLightning();

            if (BMP.GetBatteryStatus() == true && BMP.GetPanelLightningStatus())
            {
                OnCentralPanel();
            }
            else
            {
                OffCentralPanel();
            }
        }

        private void DownClick_Pump(object sender, MouseButtonEventArgs e)
        {
            if (ProgressBar_MasloDV.Value < 80)
            {
                if (BMP.StartPump())
                {
                    timerDV.Start();
                }
            }
        }

        private void UpClick_Pump(object sender, MouseButtonEventArgs e)
        {
            BMP.StopPump(Convert.ToInt32(ProgressBar_MasloDV.Value));
            timerDV.Stop();
        }

        private void Click_Starter(object sender, RoutedEventArgs e)
        {
            BMP.StartEngine("стартер", weather.GetTemperature());

            if (BMP.GetEngineStatus())
            {
                timerTah.Start();
            }
        }

        private void Click_PuskVozd(object sender, RoutedEventArgs e)
        {
            BMP.StartEngine("воздушный пу ск", weather.GetTemperature());

            if (BMP.GetEngineStatus())
            {
                timerTah.Start();
            }
        }

        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Q":
                    OpenAndCloseControlPanel();
                    break;
                case "Z":
                    int temp; string season;
                    weather.GetWeather(out temp, out season);

                    weatherWindow = new WeatherWindow(temp, season);
                    weatherWindow.Show();
                    break;
                case "X":
                    Machine machine = new Machine(BMP);
                    machine.Show();
                    break;
            }
        }

        private void Click_Door(object sender, RoutedEventArgs e)
        {
            if (BMP.GetDoorStatus())
            {
                BMP.OpenDoor();
                Image_Door.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.OpenDoor, UriKind.Relative));

                if (BMP.GetBatteryStatus() && BMP.GetPanelLightningStatus())
                {
                    IndDoorOn.Visibility = Visibility.Visible;
                    IndDoorOff.Visibility = Visibility.Collapsed;
                }
            }
            else
            {

                if (BMP.GetBatteryStatus() && BMP.GetPanelLightningStatus())
                {
                    IndDoorOn.Visibility = Visibility.Collapsed;
                    IndDoorOff.Visibility = Visibility.Visible;
                }
                Image_Door.Source = new BitmapImage(new Uri(HelperImages.LinkToImages.CloseDoor, UriKind.Relative));
                BMP.CloseDoor();
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenAndCloseControlPanel();
        }

        void OpenAndCloseControlPanel()
        {
            if (Rect1.Height > 600)
            {
                Label_Razv.Foreground = Brushes.Red;
                Label_Razv.FontWeight = FontWeights.Bold;
                Label_Razv.Margin = new Thickness(1295, 555, -180, -14);
                Label_Razv.Content = "x";
                Label_Razv.FontSize = 25;
                Rect1.Height = 587;
            }
            else
            {
                Label_Razv.Foreground = Brushes.Black;
                Label_Razv.FontSize = 13;
                Label_Razv.FontWeight = FontWeights.Normal;
                Label_Razv.Margin = new Thickness(568, 587, 547, 0);
                Label_Razv.Content = "Развернуть панель управления";
                Rect1.Height = 687;
            }
        }

        /// <summary>
        /// Метод для изменения цвета обводки кнопок в зависимости от статуса различных компонентов.
        /// Если компонент в рабочем состоянии, обводка кнопки становится зеленой, иначе - красной.
        /// </summary>
        void ChangeButtonColor()
        {
            Color lightGreen = (Color)ColorConverter.ConvertFromString("#f6fff5");
            Color lightGray = (Color)ColorConverter.ConvertFromString("#e8d5d5");

            if (BMP.PowerUnit.GetAirCanisterStatus())
            {
                AirCanister_Button.Background = new SolidColorBrush(lightGreen);
                AirCanister_Button.BorderBrush = Brushes.Green;
            }
            else
            {
                AirCanister_Button.Background = new SolidColorBrush(lightGray);
                AirCanister_Button.BorderBrush = Brushes.Red;
            }

            if (BMP.PowerUnit.GetEngineProtectionValveStatus())
            {
                EngineProtectionValve_Button.Background = new SolidColorBrush(lightGreen);
                EngineProtectionValve_Button.BorderBrush = Brushes.Green;
            }
            else
            {
                EngineProtectionValve_Button.BorderBrush = Brushes.Red;
                EngineProtectionValve_Button.Background = new SolidColorBrush(lightGray);

            }

            if (BMP.PowerUnit.GetEjectorFlapStatus())
            {
                EjectorFlap_Button.BorderBrush = Brushes.Green;
                EjectorFlap_Button.Background = new SolidColorBrush(lightGreen);
            }
            else
            {
                EjectorFlap_Button.BorderBrush = Brushes.Red;
                EjectorFlap_Button.Background = new SolidColorBrush(lightGray);

            }

            if (BMP.PowerUnit.GetHeaterHatchStatus())
            {
                HeaterHatch_Button.BorderBrush = Brushes.Green;
                HeaterHatch_Button.Background = new SolidColorBrush(lightGreen);
            }
            else
            {
                HeaterHatch_Button.BorderBrush = Brushes.Red;
                HeaterHatch_Button.Background = new SolidColorBrush(lightGray);
            }

            if (BMP.PowerUnit.GetAirFlapStatus())
            {
                AirFlap_Button.BorderBrush = Brushes.Green;
                AirFlap_Button.Background = new SolidColorBrush(lightGreen);
            }
            else
            {
                AirFlap_Button.BorderBrush = Brushes.Red;
                AirFlap_Button.Background = new SolidColorBrush(lightGray);

            }

            if (BMP.PowerUnit.GetFuelSupplyValveStatus())
            {
                FuelSupplyValve_Button.BorderBrush = Brushes.Green;
                FuelSupplyValve_Button.Background = new SolidColorBrush(lightGreen);
            }
            else
            {
                FuelSupplyValve_Button.BorderBrush = Brushes.Red;
                FuelSupplyValve_Button.Background = new SolidColorBrush(lightGray);

            }

        }

        private void HeaterHatch_Button_Click(object sender, RoutedEventArgs e)
        {
            if (BMP.PowerUnit.GetHeaterHatchStatus())
            {
                BMP.PowerUnit.CloseHeaterHatch();
            }
            else
                BMP.PowerUnit.OpenHeaterHatch();

            ChangeButtonColor();
        }

        private void FuelSupplyValve_Button_Click(object sender, RoutedEventArgs e)
        {
            if (BMP.PowerUnit.GetFuelSupplyValveStatus())
            {
                BMP.PowerUnit.CloseFuelSupplyValve();
            }
            else
                BMP.PowerUnit.OpenFuelSupplyValve();

            ChangeButtonColor();

        }

        private void AirFlap_Button_Click(object sender, RoutedEventArgs e)
        {
            if (BMP.PowerUnit.GetAirFlapStatus())
            {
                BMP.PowerUnit.CloseAirFlap();
            }
            else
                BMP.PowerUnit.OpenAirFlap();

            ChangeButtonColor();
        }

        private void EngineProtectionValve_Button_Click(object sender, RoutedEventArgs e)
        {
            if (BMP.PowerUnit.GetEngineProtectionValveStatus())
            {
                BMP.PowerUnit.CloseEngineProtectionValve();
            }
            else
                BMP.PowerUnit.OpenEngineProtectionValve();

            ChangeButtonColor();

        }

        private void EjectorFlap_Button_Click(object sender, RoutedEventArgs e)
        {
            if (BMP.PowerUnit.GetEjectorFlapStatus())
            {
                BMP.PowerUnit.CloseEjectorFlap();
            }
            else
                BMP.PowerUnit.OpenEjectorFlap();

            ChangeButtonColor();

        }

        private void AirCanister_Button_Click(object sender, RoutedEventArgs e)
        {
            if (BMP.PowerUnit.GetAirCanisterStatus())
            {
                BMP.PowerUnit.CloseAirCanister();
            }
            else
                BMP.PowerUnit.OpenAirCanister();

            ChangeButtonColor();

        }
    }
}