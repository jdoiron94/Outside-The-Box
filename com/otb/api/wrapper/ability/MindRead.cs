using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents the mind reading ability
    /// </summary>

    public class MindRead : BasePower {

        private bool unlocked;
        private bool activated;
        private int manaCost;
        private int expCost;
        private int totalCooldown;
        private int duration;
        
        private Texture2D menuTexture;
        private InputManager inputManager;

        public MindRead(bool unlocked, bool activated, Texture2D menuTexture, InputManager inputManager) {
            this.unlocked = unlocked;
            this.activated = activated;
            this.menuTexture = menuTexture;
            this.inputManager = inputManager;
            manaCost = 20;
            expCost = 1000;
            totalCooldown = 200;
            duration = 100;
        }

        public MindRead(Texture2D menuTexture) {
            this.menuTexture = menuTexture;
            manaCost = 20;
            expCost = 1000;
            unlocked = true;
            activated = true;
            totalCooldown = 200;
            duration = 100;
        }

        /// <summary>
        /// Returns the mana cost for the ability
        /// </summary>
        /// <returns>Returns the mana cost</returns>
        public int getManaCost() {
            return manaCost;
        }

        /// <summary>
        /// Returns the exp cost for the ability
        /// </summary>
        /// <returns>Returns the exp cost</returns>
        public int getExpCost() {
            return expCost;
        }

        /// <summary>
        /// Returns whether or not the ability has cooled down
        /// </summary>
        /// <returns>Returns true if the ability has met its cooldown; otherwise, false</returns>
        public bool isCooldown() {
            return totalCooldown == 200;
        }

        /// <summary>
        /// Handles unlocking of the power
        /// </summary>
        /// <param name="unlock">Whether or not to unlock the ability</param>
        public void unlockPower(bool unlock) {
            unlocked = unlock;
        }

        /// <summary>
        /// Handles activating the ability
        /// </summary>
        /// <param name="activate">Whether or not to activate the ability</param>
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
        /// Handles updating of the cooldown
        /// </summary>
        public void updateCooldown() {
            if (totalCooldown < 200) {
                totalCooldown++;
            }
        }

        /// <summary>
        /// Handles updating of the duration
        /// </summary>
        public void updateDuration() {
            if (duration < 100) {
                duration++;
            }
        }
    }
}
