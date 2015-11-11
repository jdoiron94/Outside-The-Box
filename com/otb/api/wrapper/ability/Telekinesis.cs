using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox.com.otb.api.wrapper.ability
{
    public class Telekinesis : BasePower
    {
        /*private int ID;
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
        private SoundEffect effect;

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
        }*/

        public Telekinesis(int id, int slotId, int manaCost, int expCost, int cooldown, int duration, bool unlocked, bool activated, Texture2D icon) :
            base(id, slotId, manaCost, expCost, cooldown, duration, unlocked, activated, icon) {
        }

        /*public int getID()
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

        /// <summary>
        /// Sets the ability's sound effect
        /// </summary>
        /// <param name="effect">The effect to set</param>
        public void setSoundEffect(SoundEffect effect) {
            this.effect = effect;
        }

        /// <summary>
        /// Returns the ability's sound effect
        /// </summary>
        /// <returns>Returns the ability's sound effect</returns>
        public SoundEffect getSoundEffect() {
            return effect;
        }*/
    }
}
