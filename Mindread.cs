using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    public class Mindread : BasePower
    {
        private int manaCost;
        private int XPCost;
        private bool unlocked;
        private bool activated;
        private Texture2D menuTexture;
        private int totalCooldown;
        private int ticks;
        private InputManager inputManager;
        private int duration;
        
        public Mindread(bool unlocked, bool activated, Texture2D menuTexture, InputManager inputManager)
        {
            this.unlocked = unlocked;
            this.activated = activated;
            this.menuTexture = menuTexture;
            this.inputManager = inputManager;
            manaCost = 20;
            XPCost = 1000;
            totalCooldown = 200;
            ticks = 0;
            duration = 100; 
        }

        public Mindread(Texture2D menuTexture)
        {
            this.menuTexture = menuTexture;
            manaCost = 20;
            XPCost = 1000;
            unlocked = true;
            activated = true;
            totalCooldown = 200;
            duration = 100; 
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

        public bool isCooldown()
        {
            if(totalCooldown==200)
            {
                return true;
            }
            else
            {
                return false; 
            } 
        }

        public bool getDuration()
        {
            if (duration == 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void unlockPower(bool unlock)
        {
            unlocked = unlock; 
        }
        public void activatePower(bool activate)
        {
            activated = activate;
            if (activate==true)
                duration = 0;
            else 
                totalCooldown = 0;  
        }
        public bool isActivated()
        {
            return activated; 
        }
        public bool isUnlocked()
        {
            return unlocked; 
        }
        public void behavior(GameTime gametime)
        {
            if(activated)
            {
                if(duration<100)
                {
                    updateDuration();
                }
                else
                {
                    activatePower(false);
                }
                
            }
            updateCooldown();

        }

        public void updateCooldown()
        {
            if(totalCooldown < 200)
            {
                totalCooldown++;
            }
        }

        public void updateDuration()
        {
            if(duration < 100)
            {
                duration++; 
            }
        }
    }
}
