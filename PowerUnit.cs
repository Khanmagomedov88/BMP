using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMP2
{
    internal class PowerUnit
    {
        // False - Закрыто / Выключено
        // True - Открыто / Включено

        private bool AirFlap = false; // Состояние воздушной заслонки
        private bool HeaterHatch = false; // Состояние лючка подогревателя
        private bool FuelSupplyValve = false; // Состояние крана питания топливом
        private bool EngineProtectionValve = false; // Состояние клапана защиты двигателя
        private bool EjectorFlap = false; // Состояние заслонки эжектора
        private bool AirCanister = false; // Состояние воздушного баллона


        #region AirFlap (Воздушная заслонка)
        /// <summary>
        /// Открыть воздушную заслонку
        /// </summary>
        public void OpenAirFlap()
        {
            AirFlap = true;
        }

        /// <summary>
        /// Закрыть воздушную заслонку
        /// </summary>
        public void CloseAirFlap()
        {
            AirFlap = false;
        }

        /// <summary>
        /// Возвращает состояние воздушной заслонки
        /// </summary>
        /// <returns>
        /// False - Закрыто / Выключено. True - Открыто / Включено
        /// </returns>
        public bool GetAirFlapStatus()
        {
            return AirFlap;
        }

        #endregion


        #region HeaterHatch (Лючок подогревателя)
        /// <summary>
        /// Открыть лючок подогревателя
        /// </summary>
        public void OpenHeaterHatch()
        {
            HeaterHatch = true;
        }

        /// <summary>
        /// Закрыть лючок подогревателя
        /// </summary>
        public void CloseHeaterHatch()
        {
            HeaterHatch = false;
        }

        /// <summary>
        /// Возвращает состояние лючка подогревателя
        /// </summary>
        public bool GetHeaterHatchStatus()
        {
            return HeaterHatch;
        }

        #endregion


        #region FuelSupplyValve (Кран питания топливом)
        /// <summary>
        /// Открыть кран питания топливом
        /// </summary>
        public void OpenFuelSupplyValve()
        {
            FuelSupplyValve = true;
        }

        /// <summary>
        /// Закрыть кран питания топливом
        /// </summary>
        public void CloseFuelSupplyValve()
        {
            FuelSupplyValve = false;
        }

        /// <summary>
        /// Возвращает состояние крана питания топливом
        /// </summary>
        ///<returns>
        /// False - Закрыто / Выключено. True - Открыто / Включено
        /// </returns>
        public bool GetFuelSupplyValveStatus()
        {
            return FuelSupplyValve; 
        }

        #endregion


        #region EngineProtectionValve (Клапан защиты двигателя)

        /// <summary>
        /// Открыть клапан защиты двигателя
        /// </summary>
        public void OpenEngineProtectionValve()
        {
            EngineProtectionValve = true;
        }

        /// <summary>
        /// Закрыть клапан защиты двигателя
        /// </summary>
        public void CloseEngineProtectionValve()
        {
            EngineProtectionValve = false;
        }

        /// <summary>
        /// Возвращает состояние клапана защиты двигателя
        /// </summary>
        /// <returns>
        /// False - Закрыто / Выключено. True - Открыто / Включено
        /// </returns>
        public bool GetEngineProtectionValveStatus()
        {
            return EngineProtectionValve;
        }

        #endregion


        #region EjectorFlap (Заслонки эжектора)

        /// <summary>
        /// Открыть заслонки эжектора
        /// </summary>
        public void OpenEjectorFlap()
        {
            EjectorFlap = true;
        }

        /// <summary>
        /// Закрыть заслонки эжектора
        /// </summary>
        public void CloseEjectorFlap()
        {
            EjectorFlap = false;
        }

        /// <summary>
        /// Возвращает состояние заслонок эжектора
        /// </summary>
        /// <returns>
        /// False - Закрыто / Выключено. True - Открыто / Включено
        /// </returns>
        public bool GetEjectorFlapStatus()
        {
            return EjectorFlap;
        }

        #endregion


        #region AirCanister (Воздушный баллона)

        /// <summary>
        /// Открыть воздушный балон
        /// </summary>
        public void OpenAirCanister()
        {
            AirCanister = true;
        }

        /// <summary>
        /// Закрыть воздушный балон
        /// </summary>
        public void CloseAirCanister()
        {
            AirCanister = false;
        }

        /// <summary>
        /// Возвращает состояние воздушного балона
        /// </summary>
        /// <returns>
        /// False - Закрыто / Выключено. True - Открыто / Включено
        /// </returns>
        public bool GetAirCanisterStatus()
        {
            return AirCanister;
        }

        #endregion


    }
}
