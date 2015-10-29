using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OutsideTheBox.com.otb.api.wrapper.ability
{
    public class Telekinesis : BasePower, DrainPower
    {
        private int ID;
        private int slotID;
        private bool unlocked;
        private bool activated;
        private int manaCost;
        private int expCost;
        private int totalCooldown;
        private int duration;
        private int drainRate;
        private int drainCooldown;

        private InputManager inputManager; 

        public Telekinesis(bool unlocked, bool activated, InputManager inputManager)
        {
            this.unlocked = unlocked;
            this.activated = activated;
            this.inputManager = inputManager; 

            ID = 1;
            slotID = 1;

            manaCost = 0;
            expCost = 0;

            totalCooldown = 0;
            duration = 0;

            drainRate = 0;
            drainCooldown = 0; 
        }

        public int getID()
        {
            return ID;
        }

        public int getSlotID()
        {
            return slotID;
        }

        public int getManaCost()
        {
            return manaCost;
        }

        public int getExpCost()
        {
            return expCost;
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

        public void behavior(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public int getDrainRate()
        {
            return drainRate;
        }

        public int getDrainCooldown()
        {
            return drainCooldown;
        }

        public void updateDrainCooldown()
        {
            throw new NotImplementedException();
        }
    }
}
