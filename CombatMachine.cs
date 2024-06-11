using BMP2.Weather;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BMP2
{
    public class CombatMachine
    {
        private bool doorStatus = false;
        private bool batteryStatus = false;
        private bool BcnStatus = false;
        private bool panelLighting = false;
        private int pumpStatus = 0;
        private bool SparkStatus = false; // Свеча 
        private bool EngineHeatingStatus = false; //Обогрев двигателя
        private Engine Engine = new Engine();
        internal PowerUnit PowerUnit = new PowerUnit();
        private int EngineTemperature;
        private int TemperatureWeather;

        private DispatcherTimer timerForEngineHeating;

        private static CombatMachine machine;

        public CombatMachine()
        {
            timerForEngineHeating = new DispatcherTimer();
            timerForEngineHeating.Interval = TimeSpan.FromMilliseconds(200);
            timerForEngineHeating.Tick += Timer_EngineHeating;
        }

        private void Timer_EngineHeating(object sender, EventArgs e)
        {
            if (PowerUnit.GetHeaterHatchStatus() && BcnStatus && PowerUnit.GetFuelSupplyValveStatus()
                 && batteryStatus && EngineHeatingStatus && EngineTemperature < 124)
            {
                EngineTemperature += 1;
            }
        }

        public void SetTemperatureWeatherAndEngine(int temp)
        {
            TemperatureWeather = temp;

            if (TemperatureWeather > 5)
            {
                EngineTemperature = 57;
            }
            else
            {
                EngineTemperature = 0;
            }
        }


        public bool GetStatusEngineHeat(int temp)
        {
            if (PowerUnit.GetHeaterHatchStatus() && BcnStatus && PowerUnit.GetFuelSupplyValveStatus()
                 && batteryStatus && EngineHeatingStatus)
            {
                if (temp < -19)
                {
                    return PowerUnit.GetAirFlapStatus();
                }
                else
                {
                    return true;
                }
            }
            else { return false; }
        }

        public bool StartEngine(string way, int temperature)
        {
            if (way == "стартер")
            {
                if (temperature > 5)
                {
                    EngineTemperature = 57;
                    if (batteryStatus && doorStatus && pumpStatus > 16 && BcnStatus && PowerUnit.GetEjectorFlapStatus() && PowerUnit.GetEngineProtectionValveStatus()
                        && PowerUnit.GetFuelSupplyValveStatus() && EngineTemperature > 56)
                    {
                        if (Engine.GetStatus())
                        {
                            Engine.Stop();
                            return false;
                        }
                        else
                        {
                            Engine.Start();

                            return true;
                        }
                    }
                    if (Engine.GetStatus())
                    {
                        Engine.Stop();
                        return false;
                    }
                    else
                    {
                        SoundManager.Error();
                        return false;
                    }
                }
                else
                {
                    if (batteryStatus && doorStatus && pumpStatus > 16 && BcnStatus && !PowerUnit.GetEjectorFlapStatus() && PowerUnit.GetEngineProtectionValveStatus()
                        && PowerUnit.GetFuelSupplyValveStatus() && EngineTemperature > 56)
                    {
                        if (Engine.GetStatus())
                        {
                            Engine.Stop();
                            return false;
                        }
                        else
                        {
                            Engine.Start();

                            return true;
                        }
                    }
                    else
                    {
                        if (Engine.GetStatus())
                        {
                            Engine.Stop();
                            return false;
                        }
                        else
                        {
                            SoundManager.Error();
                            return false;
                        }
                    }
                }
            }
            else if (way == "воздушный пуск")
            {
                if (temperature > 5)
                {
                    EngineTemperature = 57;
                    if (batteryStatus && doorStatus && pumpStatus > 16 && BcnStatus && PowerUnit.GetEjectorFlapStatus() && PowerUnit.GetEngineProtectionValveStatus()
                        && PowerUnit.GetFuelSupplyValveStatus() && PowerUnit.GetAirCanisterStatus() && EngineTemperature > 55)
                    {
                        if (Engine.GetStatus())
                        {
                            Engine.Stop();
                            return false;
                        }
                        else
                        {
                            Engine.Start();

                            return true;
                        }
                    }
                    if (Engine.GetStatus())
                    {
                        Engine.Stop();
                        return false;
                    }
                    else
                    {
                        SoundManager.Error();
                        return false;
                    }
                }
                else
                {
                    if (batteryStatus && doorStatus && pumpStatus > 16 && BcnStatus && !PowerUnit.GetEjectorFlapStatus() && PowerUnit.GetEngineProtectionValveStatus()
                        && PowerUnit.GetFuelSupplyValveStatus() && PowerUnit.GetAirCanisterStatus() && EngineTemperature > 55)
                    {
                        if (Engine.GetStatus())
                        {
                            Engine.Stop();
                            return false;
                        }
                        else
                        {
                            Engine.Start();

                            return true;
                        }
                    }
                        if (Engine.GetStatus())
                        {
                            Engine.Stop();
                            return false;
                        }
                        else
                        {
                            SoundManager.Error();
                            return false;
                        }
                }
            }
            else
            {
                return false;
            }

        }

        public void CloseDoor()
        {
            doorStatus = true;
        }

        public void OpenDoor()
        {
            doorStatus = false;
        }

        public void ToggleBatteryState()
        {
            if (batteryStatus)
            {
                batteryStatus = false;
            }
            else
            {
                batteryStatus = true;
            }
        }

        public void ToggleSparkState()
        {
            if (SparkStatus)
            {
                SparkStatus = false;
            }
            else
            {
                SparkStatus = true;
            }
        }

        public void ToggleEngineHeatingState(int temperature)
        {
            if (EngineHeatingStatus)
            {
                EngineHeatingStatus = false;
            }
            else
            {
                EngineHeatingStatus = true;
            }

            if (GetStatusEngineHeat(temperature) && EngineHeatingStatus && SparkStatus)
            {
                timerForEngineHeating.Start();
            }
            else if (!GetStatusEngineHeat(temperature) && EngineHeatingStatus)
            {
                SoundManager.Error();
                timerForEngineHeating.Stop();
            }
            else
            {
                timerForEngineHeating.Stop();
            }
        }

        public bool StartPump()
        {
            if (batteryStatus)
            {
                SoundManager.StartPump();
                return true;
            }
            else
            {
                SoundManager.Error();
                return false;
            }
        }


        public void StopPump(int valueMaslo)
        {
            SoundManager.StopPump();

            pumpStatus = valueMaslo;
        }


        public void ToggleBcnState()
        {
            BcnStatus = true;
        }

        public bool GetDoorStatus()
        {
            return doorStatus;
        }

        public void TooglePanelLightning()
        {
            if (panelLighting)
                panelLighting = false;
            else
                panelLighting = true;
        }

        public bool GetPanelLightningStatus()
        {
            return panelLighting;
        }

        public bool GetBatteryStatus()
        {
            return batteryStatus;
        }

        public int GetTemperaatureEngine()
        {
            return EngineTemperature;
        }

        public int GetTemperaatureWeather()
        {
            return TemperatureWeather;
        }

        public bool GetEngineStatus()
        {
            return Engine.GetStatus();
        }

        public bool GetBcnStatus()
        {
            return BcnStatus;
        }

        public int GetPumpStatus()
        {
            return pumpStatus;
        }

        public bool GetSparkStatus()
        {
            return SparkStatus;
        }

        public bool GetEngineHeatingStatus()
        {
            return EngineHeatingStatus;
        }
    }

}

