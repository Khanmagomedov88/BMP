using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMP2.Weather
{
    internal class Weather
    {
        private int Temperature;

        public Weather(int temp)
        {
            Temperature = temp;
        }

        public Weather()
        {
            Temperature = SetTemperature();
        }

        public Weather(string weatherName)
        {
            Temperature = SetTemperature(weatherName);
        }

        private string IdentificationSeason()
        {
            if (Temperature > 15)
            {
                return "Солнечно";
            }
            else if(Temperature <= 15 && Temperature > -1)
            {
                return "Облачно";
            }
            else
            {
                return "Снег";
            }
        }


        private int SetTemperature(string weatherName)
        {
            Random rand = new Random();
            int temp = 0;
            if (weatherName == "Солнечно")
            {
                temp = rand.Next(12, 32);
            }
            else if (weatherName == "Снег")
            {
                temp = rand.Next(-22, 2);
            }

            return temp;
        }

        private int SetTemperature()
        {
            Random rand = new Random();
            int temp = rand.Next(-22, 35);

            return temp;
        }

        public void GetWeather(out int temp, out string season)
        {
            season = IdentificationSeason();

            temp = Temperature;
        }

        public int GetTemperature()
        {
            return Temperature;
        }
    }
}
