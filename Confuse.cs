using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    public class Confuse : BasePower
    {
        private int manaCost;
        private int XPCost;
        private bool unlocked;
        private bool activated;
        private int totalCooldown;
        private int ticks;
        private InputManager inputManager;
        private int duration;
        private List<Direction> prev_directions;

        public Confuse(bool unlocked, bool activated)
        {
            this.unlocked = unlocked;
            this.activated = activated;
            manaCost = 20;
            XPCost = 1000;
            totalCooldown = 200;
            ticks = 0;
            duration = 50;
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
                    prev_directions = new List<Direction>(level.getNpcs().Count);
                    List<Npc> npcs = level.getNpcs();
                    for(int i = 0; i < npcs.Count; i++)
                    {
                        prev_directions.Add(npcs[i].getDirection());
                        npcs[i].setVelocity(0);
                        npcs[i].setDirection(Direction.NONE);
                    }
                    updateDuration();
                }
                else if (duration < 200)
                {
                    updateDuration();
                }
                else
                {
                    List<Npc> npcs = level.getNpcs();
                    for(int i = 0; i < npcs.Count; i++)
                    {
                        npcs[i].setVelocity(3);
                        npcs[i].setDirection(prev_directions[i]);
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
