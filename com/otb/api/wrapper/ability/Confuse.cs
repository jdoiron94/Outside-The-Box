using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the confuse power
    /// </summary>

    public class Confuse : BasePower {

        private int ID;
        private int slotID;
        private bool unlocked;
        private bool activated;
        private int manaCost;
        private int expCost;
        private int totalCooldown;
        private int duration;

        private SoundEffect effect;

        public Confuse(bool unlocked, bool activated) {
            this.unlocked = unlocked;
            this.activated = activated;
            manaCost = 20;
            expCost = 1000;
            totalCooldown = 200;
            duration = 50;
            ID = 6;
            slotID = 7;
        }

        /// <summary>
        /// Returns ID for the ability
        /// </summary>
        /// <returns>Returns the ID</returns>
        public int getID()
        {
            return ID;
        }

        /// <summary>
        /// Returns the slotID for the ability
        /// </summary>
        /// <returns>Returns the slotID</returns>
        public int getSlotID()
        {
            return slotID;
        }

        /// <summary>
        /// Returns the mana cost to use the ability
        /// </summary>
        /// <returns>Returns the mana cost to use the ability</returns>
        public int getManaCost() {
            return manaCost;
        }

        /// <summary>
        /// Returns the exp cost to use the ability
        /// </summary>
        /// <returns>Returns the exp cost to use the ability</returns>
        public int getExpCost() {
            return expCost;
        }

        /// <summary>
        /// Handles how the ability works on npcs
        /// </summary>
        /// <param name="level">The level to confuse npcs on</param>
        public void doStuff(Level level) {
            if (activated) {
                if (duration == 0) {
                    foreach (Npc n in level.getNpcs()) {
                        n.setVelocity(0);
                    }
                    updateDuration();
                } else if (duration < 200) {
                    updateDuration();
                } else {
                    foreach (Npc n in level.getNpcs()) {
                        n.setVelocity(3);
                    }
                    activatePower(false);
                }
            }
            updateCooldown();
        }

        /// <summary>
        /// Returns whether or not the cooldown has been met
        /// </summary>
        /// <returns>Returns true if the cooldown has been met; otherwise, false</returns>
        public bool isCooldown() {
            return totalCooldown == 200;
        }

        /// <summary>
        /// Handles unlocking of the ability
        /// </summary>
        /// <param name="unlock">Bool for unlocking or locking the ability</param>
        public void unlockPower(bool unlock) {
            unlocked = unlock;
        }

        /// <summary>
        /// Handles activating the power
        /// </summary>
        /// <param name="activate">Bool for activating or deactivating the ability</param>
        public void activatePower(bool activate) {
            activated = activate;
            if (activate) {
                duration = 0;
            } else {
                totalCooldown = 0;
            }
        }

        /// <summary>
        /// Returns whether or not the ability is activated
        /// </summary>
        /// <returns>Returns true if the ability is activated; otherwise, false</returns>
        public bool isActivated() {
            return activated;
        }

        /// <summary>
        /// Returns whether or not the ability is unlocked
        /// </summary>
        /// <returns>Returns true if the ability is unlocked; otherwise, false</returns>
        public bool isUnlocked() {
            return unlocked;
        }

        /// <summary>
        /// Handles the ability's behavior
        /// </summary>
        /// <param name="gametime">The GameTime to respect</param>
        public void behavior(GameTime gametime) {
            if (activated) {
                if (duration < 100) {
                    updateDuration();
                } else {
                    activatePower(false);
                }
            }
            updateCooldown();
        }

        /// <summary>
        /// Handles updating the cooldown
        /// </summary>
        public void updateCooldown() {
            if (totalCooldown < 200) {
                totalCooldown++;
            }
        }

        /// <summary>
        /// Handles updating the duration
        /// </summary>
        public void updateDuration() {
            if (duration < 200) {
                duration++;
            }
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
        }
    }
}
