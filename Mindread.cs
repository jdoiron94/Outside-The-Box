using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineticCamp {

    public class Mindread : BasePower {

        private bool unlocked;
        private bool activated;
        private int manaCost;
        private int expCost;
        private int totalCooldown;
        private int duration;
        
        private Texture2D menuTexture;
        private InputManager inputManager;

        public Mindread(bool unlocked, bool activated, Texture2D menuTexture, InputManager inputManager) {
            this.unlocked = unlocked;
            this.activated = activated;
            this.menuTexture = menuTexture;
            this.inputManager = inputManager;
            manaCost = 20;
            expCost = 1000;
            totalCooldown = 200;
            duration = 100;
        }

        public Mindread(Texture2D menuTexture) {
            this.menuTexture = menuTexture;
            manaCost = 20;
            expCost = 1000;
            unlocked = true;
            activated = true;
            totalCooldown = 200;
            duration = 100;
        }

        public int getManaCost() {
            return manaCost;
        }
        public int getExpCost() {
            return expCost;
        }

        public bool isCooldown() {
            return totalCooldown == 200;
        }

        public bool getDuration() {
            return duration == 100;
        }

        public void unlockPower(bool unlock) {
            unlocked = unlock;
        }

        public void activatePower(bool activate) {
            activated = activate;
            if (activate) {
                duration = 0;
            } else {
                totalCooldown = 0;
            }
        }

        public bool isActivated() {
            return activated;
        }

        public bool isUnlocked() {
            return unlocked;
        }

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

        public void updateCooldown() {
            if (totalCooldown < 200) {
                totalCooldown++;
            }
        }

        public void updateDuration() {
            if (duration < 100) {
                duration++;
            }
        }
    }
}
