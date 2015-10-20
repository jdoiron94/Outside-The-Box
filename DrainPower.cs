using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    interface DrainPower
    {
        int getDrainRate();
        int getDrainCooldown();
        void updateDrainCooldown(); 
    }
}
