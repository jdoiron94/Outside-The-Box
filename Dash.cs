using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    public class Dash : BasePower
    {
        private int manaCost;
        private int XPCost;
        private bool unlocked;
        private bool activated;
        private int totalCooldown;
        private int ticks;
        private InputManager inputManager;
        private int duration;
        private int durationLimit;
        private int cooldownLimit;

        public Dash(bool unlocked, bool activated)
        {
            this.unlocked = unlocked;
            this.activated = activated;
            manaCost = 5;
            XPCost = 1000;
            totalCooldown = 20;
            ticks = 0;
            duration = 15;
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
                if (duration < 15)
                {
                    Direction direction = level.getPlayer().getDirection();
                    switch(direction) {
                        case Direction.NORTH:
                            level.getPlayer().deriveY(-6);
                            break;
                        case Direction.SOUTH:
                            level.getPlayer().deriveY(6);
                            break;
                        case Direction.WEST:
                            level.getPlayer().deriveX(-6);
                            break;
                        case Direction.EAST:
                            level.getPlayer().deriveX(6);
                            break;
                        default:
                            break;
                    }   
                    updateDuration();   
                }
                else
                {
                    activatePower(false);
                }
            }
            updateCooldown(); 
        }

        public bool isCooldown()
        {
            if (totalCooldown == 20)
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
            if (duration == 15)
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
                if (duration < 15)
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
            if (totalCooldown < 20)
            {
                totalCooldown++;
            }
        }

        public void updateDuration()
        {
            if (duration < 15)
            {
                duration++;
            }
        }
    }
}
