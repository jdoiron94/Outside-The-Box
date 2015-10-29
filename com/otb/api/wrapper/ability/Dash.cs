using Microsoft.Xna.Framework;

namespace OutsideTheBox {
    
    /// <summary>
    /// Class which handles the dash ability
    /// </summary>

    public class Dash : BasePower {

        private int ID;
        private int slotID;
        private bool unlocked;
        private bool activated;
        private int manaCost;
        private int expCost;
        private int totalCooldown;
        private int duration;

        public Dash(bool unlocked, bool activated) {
            this.unlocked = unlocked;
            this.activated = activated;
            manaCost = 5;
            expCost = 1000;
            totalCooldown = 20;
            duration = 15;
            ID = 4;
            slotID = 4; 
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
        /// Handles how the ability works
        /// </summary>
        /// <param name="level">The level to respect</param>
        public void doStuff(Level level) {
            if (activated) {
                if (duration < 15) {
                    switch (level.getPlayer().getDirection()) {
                        case Direction.North:
                            level.getPlayer().deriveY(-6);
                            break;
                        case Direction.South:
                            level.getPlayer().deriveY(6);
                            break;
                        case Direction.West:
                            level.getPlayer().deriveX(-6);
                            break;
                        case Direction.East:
                            level.getPlayer().deriveX(6);
                            break;
                        default:
                            break;
                    }
                    updateDuration();
                } else {
                    activatePower(false);
                }
            }
            updateCooldown();
        }

        /// <summary>
        /// Returns whether or not the ability has cooled down
        /// </summary>
        /// <returns>Returns true if the cooldown has been met; otherwise, false</returns>
        public bool isCooldown() {
            return totalCooldown == 20;
        }

        /// <summary>
        /// Handles unlocking the ability
        /// </summary>
        /// <param name="unlock">Bool to set as unlocked or locked</param>
        public void unlockPower(bool unlock) {
            unlocked = unlock;
        }

        /// <summary>
        /// Handles activating the ability
        /// </summary>
        /// <param name="activate">Bool to activate or deactivate</param>
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
        /// Returns whether or not the ability us unlocked
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
                if (duration < 15) {
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
            if (totalCooldown < 20) {
                totalCooldown++;
            }
        }

        /// <summary>
        /// Handles updating the duration
        /// </summary>
        public void updateDuration() {
            if (duration < 15) {
                duration++;
            }
        }
    }
}
