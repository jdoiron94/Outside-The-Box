using Microsoft.Xna.Framework;

namespace OutsideTheBox {

    /// <summary>
    /// Class representing the slow-mo ability
    /// </summary>

    public class SlowTime : BasePower {

        private int ID;
        private int slotID; 
        private bool unlocked;
        private bool activated;
        private int manaCost;
        private int expCost;
        private int totalCooldown;
        private int duration;

        public SlowTime(bool unlocked, bool activated) {
            this.unlocked = unlocked;
            this.activated = activated;
            manaCost = 20;
            expCost = 1000;
            totalCooldown = 200;
            duration = 200;
            ID = 3;
            slotID = 2; 
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
        /// Returns the ability's mana cost
        /// </summary>
        /// <returns>Returns the mana cost</returns>
        public int getManaCost() {
            return manaCost;
        }

        /// <summary>
        /// Returns the ability's exp cost
        /// </summary>
        /// <returns>Returns the exp cost</returns>
        public int getExpCost() {
            return expCost;
        }

        /// <summary>
        /// Controls the ability's npc slowing
        /// </summary>
        /// <param name="level">The level to freeze npcs in</param>
        public void doStuff(Level level) {
            if (activated) {
                if (duration == 0) {
                    foreach (Npc npc in level.getNpcs()) {
                        npc.setVelocity(1);
                    }
                    updateDuration();
                } else if (duration < 200) {
                    updateDuration();
                } else {
                    foreach (Npc npc in level.getNpcs()) {
                        npc.setVelocity(3);
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
        /// Handles unlocking of the power
        /// </summary>
        /// <param name="unlock">The unlock status to set</param>
        public void unlockPower(bool unlock) {
            unlocked = unlock;
        }

        /// <summary>
        /// Handles activating the power
        /// </summary>
        /// <param name="activate">The activate status to set</param>
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
        /// Updates the ability's cooldown
        /// </summary>
        public void updateCooldown() {
            if (totalCooldown < 200) {
                totalCooldown++;
            }
        }

        /// <summary>
        /// Updates the ability's duration
        /// </summary>
        public void updateDuration() {
            if (duration < 200) {
                duration++;
            }
        }
    }
}
