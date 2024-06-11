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
using System.Windows.Threading;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace BMP2
{
    /// <summary>
    /// Логика взаимодействия для Machine.xaml
    /// </summary>
    public partial class Machine : Window
    {

        private bool statusEngine;
        private bool doorStatus;
        private bool batteryStatus;
        private int engineTemperature;



        private DispatcherTimer timer;

        CombatMachine BMP;

        string wayStart = "Стартер";


        public Machine(CombatMachine _BMP)
        {
            BMP = _BMP;

            InitializeComponent();

            SetInformationBMP();
            SetInformationAboutStart();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetInformationBMP();
            SetInformationAboutStart();
        }

        private void SetInformationBMP()
        {
            statusEngine = BMP.GetEngineStatus();
            doorStatus = BMP.GetDoorStatus();
            batteryStatus = BMP.GetBatteryStatus();
            engineTemperature = BMP.GetTemperaatureEngine();
            Label_Weather.Content = BMP.GetTemperaatureWeather() + "° С";

            if (statusEngine)
            {
                EngineLabel.Foreground = Brushes.Green;
                EngineLabel.Text = "Заведен";
            }
            else
            {
                EngineLabel.Foreground = Brushes.Red;
                EngineLabel.Text = "Заглушен";
            }

            if (doorStatus)
            {
                Door.Foreground = Brushes.Green;
                Door.Text = "Закрыты";
            }
            else
            {
                Door.Foreground = Brushes.Red;
                Door.Text = "Открыты";
            }

            if (batteryStatus)
            {
                Battery.Foreground = Brushes.Green;
                Battery.Text = "Включен";
            }
            else
            {
                Battery.Foreground = Brushes.Red;
                Battery.Text = "Выключен";
            }

            if (engineTemperature > 55)
            {
                TempEngine.Foreground = Brushes.Green;
            }
            else
            {
                TempEngine.Foreground = Brushes.Blue;
            }

            TempEngine.Text = engineTemperature.ToString() + " °C";
        }

        public void UpdatingInformation()
        {

        }

        private void SetInformationAboutStart()
        {
            if (BMP.GetTemperaatureWeather() > 5) // Лето
            {
                switch (wayStart)
                {
                    case "Воздух":
                        SetText(BMP.PowerUnit.GetEjectorFlapStatus(), Text_1, "1. Открыть заслонку эжектора");
                        SetText(BMP.GetBatteryStatus(), Text_2, "2. Включить выключатель ВЫКЛ БАТАР");
                        SetText(BMP.PowerUnit.GetEngineProtectionValveStatus(), Text_3, "3. Открыть клапаны защиты двигателя");
                        SetText(BMP.PowerUnit.GetFuelSupplyValveStatus(), Text_4, "4. Открыть топливный кран");
                        SetText(BMP.GetBcnStatus(), Text_5, "5. Включить выключатель БЦН");
                        SetText(BMP.PowerUnit.GetAirCanisterStatus(), Text_6, "6. Открыть вентиль балона");
                        SetText(BMP.GetDoorStatus(), Text_7, "7. Закрыть люк десантного отделения");


                        if (BMP.GetPumpStatus() > 16)
                        {
                            Text_8.Inlines.Clear();
                            Text_8.Inlines.Add(new Run("8. Создать давление масла в системе смазки двигателя "));
                            Text_8.Inlines.Add(new Run("✅") { Foreground = Brushes.Green });
                        }
                        else
                        {
                            Text_8.Inlines.Clear();
                            Text_8.Inlines.Add(new Run("8. Создать давление масла в системе смазки двигателя "));
                            Text_8.Inlines.Add(new Run("❎") { Foreground = Brushes.Red });
                        }

                        Text_9.Text = "";
                        Text_10.Text = "";
                        break;
                    case "Стартер":

                        SetText(BMP.PowerUnit.GetEjectorFlapStatus(), Text_1, "1. Открыть заслонку эжектора");
                        SetText(BMP.GetBatteryStatus(), Text_2, "2. Включить выключатель ВЫКЛ БАТАР");
                        SetText(BMP.PowerUnit.GetEngineProtectionValveStatus(), Text_3, "3. Открыть клапаны защиты двигателя");
                        SetText(BMP.PowerUnit.GetFuelSupplyValveStatus(), Text_4, "4. Открыть топливный кран");
                        SetText(BMP.GetBcnStatus(), Text_5, "5. Включить выключатель БЦН");
                        SetText(BMP.GetDoorStatus(), Text_6, "6. Закрыть люк десантного отделения");

                        if (BMP.GetPumpStatus() > 16)
                        {
                            Text_7.Inlines.Clear();
                            Text_7.Inlines.Add(new Run("7. Создать давление масла в системе смазки двигателя "));
                            Text_7.Inlines.Add(new Run("✅") { Foreground = Brushes.Green });
                        }
                        else
                        {
                            Text_7.Inlines.Clear();
                            Text_7.Inlines.Add(new Run("7. Создать давление масла в системе смазки двигателя "));
                            Text_7.Inlines.Add(new Run("❎") { Foreground = Brushes.Red });
                        }

                        Text_8.Text = "";
                        Text_9.Text = "";
                        Text_10.Text = "";
                        break;
                    case "Разогрев":
                        SetText(BMP.GetBatteryStatus(), Text_1, "1. Включить выключатель ВЫКЛ БАТАР");
                        SetText(BMP.PowerUnit.GetHeaterHatchStatus(), Text_2, "2. Открыть крышку лючка");
                        SetText(BMP.GetBcnStatus(), Text_3, "3. Включить выключатель БЦН");
                        SetText(BMP.PowerUnit.GetAirFlapStatus(), Text_4, "4. Закрыть воздушную заслонку (если -20°C");
                        SetText(BMP.GetSparkStatus(), Text_5, "5. Включить выключатель свеча");
                        SetText(BMP.GetEngineHeatingStatus(), Text_6, "6. Включить обогреватель двигателя");

                        Text_7.Text = "";
                        Text_8.Text = "";
                        Text_9.Text = "";
                        Text_10.Text = "";
                        break;
                }
            }
            else // Зима
            {
                switch (wayStart)
                {
                    case "Воздух":
                        SetText(BMP.GetBatteryStatus(), Text_1, "1. Включить выключатель ВЫКЛ БАТАР");
                        SetText(BMP.PowerUnit.GetEngineProtectionValveStatus(), Text_2, "2. Открыть клапаны защиты двигателя");
                        SetText(BMP.PowerUnit.GetFuelSupplyValveStatus(), Text_3, "3. Открыть топливный кран");
                        SetText(BMP.PowerUnit.GetAirCanisterStatus(), Text_4, "4. Открыть воздушный балон");
                        SetText(BMP.GetBcnStatus(), Text_5, "5. Включить выключатель БЦН");
                        SetText(BMP.GetDoorStatus(), Text_6, "6. Закрыть люк десантного отделения");
                        SetText(!BMP.PowerUnit.GetEjectorFlapStatus(), Text_8, "8. Закрыть заслонку эжектора");

                        if (BMP.GetPumpStatus() > 16)
                        {
                            Text_7.Inlines.Clear();
                            Text_7.Inlines.Add(new Run("7. Создать давление масла в системе смазки двигателя "));
                            Text_7.Inlines.Add(new Run("✅") { Foreground = Brushes.Green });
                        }
                        else
                        {
                            Text_7.Inlines.Clear();
                            Text_7.Inlines.Add(new Run("7. Создать давление масла в системе смазки двигателя "));
                            Text_7.Inlines.Add(new Run("❎") { Foreground = Brushes.Red });
                        }

                        if (BMP.GetTemperaatureEngine() > 55)
                        {
                            Text_9.Inlines.Clear();
                            Text_9.Inlines.Add(new Run("9.Разогреть двигатель"));
                            Text_9.Inlines.Add(new Run("✅") { Foreground = Brushes.Green });
                        }
                        else
                        {
                            Text_9.Inlines.Clear();
                            Text_9.Inlines.Add(new Run("9. Разогреть двигатель"));
                            Text_9.Inlines.Add(new Run("❎") { Foreground = Brushes.Red });
                        }
                        
                        Text_10.Text = "";
                        break;
                    case "Стартер":
                        SetText(BMP.GetBatteryStatus(), Text_1, "1. Включить выключатель ВЫКЛ БАТАР");
                        SetText(BMP.PowerUnit.GetEngineProtectionValveStatus(), Text_2, "2. Открыть клапаны защиты двигателя");
                        SetText(BMP.PowerUnit.GetFuelSupplyValveStatus(), Text_3, "3. Открыть топливный кран");
                        SetText(BMP.GetBcnStatus(), Text_4, "4. Включить выключатель БЦН");
                        SetText(BMP.GetDoorStatus(), Text_5, "5. Закрыть люк десантного отделения");
                        SetText(!BMP.PowerUnit.GetEjectorFlapStatus(), Text_7, "7. Закрыть заслонку эжектора");



                        if (BMP.GetPumpStatus() > 16)
                        {
                            Text_6.Inlines.Clear();
                            Text_6.Inlines.Add(new Run("6. Создать давление масла в системе смазки двигателя"));
                            Text_6.Inlines.Add(new Run("✅") { Foreground = Brushes.Green });
                        }
                        else
                        {
                            Text_6.Inlines.Clear();
                            Text_6.Inlines.Add(new Run("6. Создать давление масла в системе смазки двигателя "));
                            Text_6.Inlines.Add(new Run("❎") { Foreground = Brushes.Red });
                        }

                        if (BMP.GetTemperaatureEngine() > 55)
                        {
                            Text_8.Inlines.Clear();
                            Text_8.Inlines.Add(new Run("8.Разогреть двигатель"));
                            Text_8.Inlines.Add(new Run("✅") { Foreground = Brushes.Green });
                        }
                        else
                        {
                            Text_8.Inlines.Clear();
                            Text_8.Inlines.Add(new Run("8. Разогреть двигатель"));
                            Text_8.Inlines.Add(new Run("❎") { Foreground = Brushes.Red });
                        }

                        Text_9.Text = "";
                        Text_10.Text = "";
                        break;
                    case "Разогрев":
                        SetText(BMP.GetBatteryStatus(), Text_1, "1. Включить выключатель ВЫКЛ БАТАР");
                        SetText(BMP.PowerUnit.GetHeaterHatchStatus(), Text_2, "2. Открыть крышку лючка");
                        SetText(BMP.PowerUnit.GetFuelSupplyValveStatus(), Text_3, "3. Открыть топливный кран");
                        SetText(BMP.GetBcnStatus(), Text_4, "4. Включить выключатель БЦН");
                        SetText(BMP.PowerUnit.GetAirFlapStatus(), Text_5, "5. Закрыть воздушную заслонку (если -20°C");
                        SetText(BMP.GetSparkStatus(), Text_6, "6. Включить выключатель свеча");
                        SetText(BMP.GetEngineHeatingStatus(), Text_7, "7. Включить обогреватель двигателя");

                        Text_8.Text = "";
                        Text_9.Text = "";
                        Text_10.Text = "";
                        break;
                }

            }
        }

        private void SetText(bool Condition, TextBlock block, string text)
        {
            if (Condition)
            {
                block.Inlines.Clear(); // Очищаем все существующие элементы
                block.Inlines.Add(new Run(text + " ")); // Добавляем текст с пробелом в конце
                block.Inlines.Add(new Run("✅") { Foreground = Brushes.Green }); // Добавляем галочку с красным цветом
            }
            else
            {
                block.Inlines.Clear(); // Очищаем все существующие элементы
                block.Inlines.Add(new Run(text + " ")); // Добавляем текст с пробелом в конце
                block.Inlines.Add(new Run("❎") { Foreground = Brushes.Red }); // Добавляем крестик
            }

        }

        private void Button_Starter_Click(object sender, RoutedEventArgs e)
        {
            wayStart = "Стартер";
            SetStyleButton();

            SetInformationAboutStart();
        }

        private void Button_Vozd_Click(object sender, RoutedEventArgs e)
        {
            wayStart = "Воздух";
            SetStyleButton();

            SetInformationAboutStart();
        }


        private void Button_Razogrev_Click(object sender, RoutedEventArgs e)
        {
            wayStart = "Разогрев";
            SetStyleButton();

            SetInformationAboutStart();
        }


        private void SetStyleButton()
        {
            if (wayStart == "Стартер")
            {
                Button_Starter.Width = 70;
                Button_Starter.Height = 23;
                Button_Starter.BorderBrush = Brushes.Green;
                Button_Starter.BorderThickness = new Thickness(2);

                Button_Vozd.Width = 70;
                Button_Vozd.Height = 20;
                Button_Vozd.BorderBrush = Brushes.Black;
                Button_Vozd.BorderThickness = new Thickness(1);

                Button_Razogrev.Width = 70;
                Button_Razogrev.Height = 20;
                Button_Razogrev.BorderBrush = Brushes.Black;
                Button_Razogrev.BorderThickness = new Thickness(1);
            }
            else if( wayStart == "Воздух")
            {
                Button_Vozd.Width = 70;
                Button_Vozd.Height = 23;
                Button_Vozd.BorderBrush = Brushes.Green;
                Button_Vozd.BorderThickness = new Thickness(2);


                Button_Starter.Width = 70;
                Button_Starter.Height = 20;
                Button_Starter.BorderBrush = Brushes.Black;
                Button_Starter.BorderThickness = new Thickness(1);

                Button_Razogrev.Width = 70;
                Button_Razogrev.Height = 20;
                Button_Razogrev.BorderBrush = Brushes.Black;
                Button_Razogrev.BorderThickness = new Thickness(1);

            }
            else
            {
                Button_Razogrev.Width = 70;
                Button_Razogrev.Height = 23;
                Button_Razogrev.BorderBrush = Brushes.Green;
                Button_Razogrev.BorderThickness = new Thickness(2);

                Button_Vozd.Width = 70;
                Button_Vozd.Height = 20;
                Button_Vozd.BorderBrush = Brushes.Black;
                Button_Vozd.BorderThickness = new Thickness(1);


                Button_Starter.Width = 70;
                Button_Starter.Height = 20;
                Button_Starter.BorderBrush = Brushes.Black;
                Button_Starter.BorderThickness = new Thickness(1);
            }
        }

    }
}
