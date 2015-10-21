using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    public class SlowTime : BasePower
    {
        private int manaCost;
        private int XPCost;
        private bool unlocked;
        private bool activated;
        private int totalCooldown;
        private int ticks;
        private InputManager inputManager;
        private int duration;

        public SlowTime(bool unlocked, bool activated)
        {
            this.unlocked = unlocked;
            this.activated = activated;
            manaCost = 20;
            XPCost = 1000;
            totalCooldown = 200;
            ticks = 0;
            duration = 200;
        }

        public int getManaCost()
        {
            return manaCost;
        }
        public int getXPCost()
        {
            return XPCost;
        }

        public void doStuff(Level level)
        {
            if (activated)
            {
                if (duration == 0)
                {
                    foreach (Npc npc in level.getNpcs())
                    {
                        npc.setVelocity(1);
                    }

                    updateDuration();
                }
                else if (duration < 200)
                {
                    updateDuration();
                }
                else
                {
                    foreach (Npc npc in level.getNpcs())
                    {
                        npc.setVelocity(3);
                    }

                    activatePower(false);
                }

            }
            updateCooldown();

        }

        public bool isCooldown()
        {
            if (totalCooldown == 200)
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
            if (duration == 200)
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
            if (activate == true)
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
            if (activated)
            {
                if (duration < 100)
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
            if (totalCooldown < 200)
            {
                totalCooldown++;
            }
        }

        public void updateDuration()
        {
            if (duration < 200)
            {
                duration++;
            }
        }
    }
}
