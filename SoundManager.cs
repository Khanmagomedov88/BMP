using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace BMP2
{
    static class SoundManager
    {
        public static void Error()
        {
            Stream streamError = Properties.Resources.Error;
            SoundPlayer errorEngine = new SoundPlayer(streamError);
            errorEngine.Play();
        }

        public static void StartPump()
        {
            Stream pumpStream = Properties.Resources.Bcn;
            SoundPlayer pump = new SoundPlayer(pumpStream);
            pump.Play();
        }


        public static void StopPump()
        {
            Stream pumpStream = Properties.Resources.Bcn;
            SoundPlayer pump = new SoundPlayer(pumpStream);
            pump.Stop();
        }

        public static void EngineStart()
        {
            Stream streamStartEngine = Properties.Resources.StartBMP;
            SoundPlayer startEngine = new SoundPlayer(streamStartEngine);
            startEngine.Play();
        }

        public static void EngineStop()
        {
            Stream streamStartEngine = Properties.Resources.StartBMP;
            SoundPlayer startEngine = new SoundPlayer(streamStartEngine);
            startEngine.Stop();
        }
    }
}
