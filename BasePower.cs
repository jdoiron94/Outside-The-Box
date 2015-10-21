using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    interface BasePower
    {
        int getManaCost();
        int getXPCost();
        void unlockPower(bool unlock);
        void activatePower(bool activate);
        bool isActivated();
        bool isUnlocked();
        void behavior(GameTime gametime); 
    }
}
