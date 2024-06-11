using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace BMP2
{
    class Engine
    {
        private bool StatusEngine = false;

        public bool GetStatus()
        {
            return StatusEngine;
        }


        public void Start()
        {
            SoundManager.EngineStart();
            StatusEngine = true;
        }

        public void Stop()
        {
            SoundManager.EngineStop();
            StatusEngine = false;
        }
    }
}
