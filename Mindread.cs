using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    class Mindread : BasePower
    {
        private int manaCost;
        private int XPCost;
        private bool unlocked;
        private bool activated;
        private Texture2D menuTexture;
        private int totalCooldown;
        private int ticks; 
        
        public Mindread(bool unlocked, bool activated, Texture2D menuTexture)
        {
            manaCost = 20;
            XPCost = 1000;
            this.unlocked = unlocked;
            this.activated = activated;
            this.menuTexture = menuTexture;
            totalCooldown = 200;
            ticks = 0; 
        }

        public Mindread(Texture2D menuTexture)
        {
            this.menuTexture = menuTexture;
            manaCost = 20;
            XPCost = 1000;
            unlocked = true;
            activated = true;
            totalCooldown = 200;
            ticks = 0;  
        }

        public int getManaCost()
        {
            return manaCost;
        }
        public int getXPCost()
        {
            return XPCost;
        }
        public void unlockPower(bool unlock)
        {
            unlocked = unlock; 
        }
        public void activatePower(bool activate)
        {
            activated = activate; 
        }
        public bool isActivated()
        {
            return activated; 
        }
        public bool isUnlocked()
        {
            return unlocked; 
        }
        public void behavior()
        {

        }

        public void updateCooldown()
        {
            ticks = (ticks + 1) % totalCooldown; 
        }
    }
}
