using Microsoft.Xna.Framework;

namespace KineticCamp {

    public class Confuse : BasePower {

        private bool unlocked;
        private bool activated;
        private int manaCost;
        private int expCost;
        private int totalCooldown;
        private int duration;

        public Confuse(bool unlocked, bool activated) {
            this.unlocked = unlocked;
            this.activated = activated;
            manaCost = 20;
            expCost = 1000;
            totalCooldown = 200;
            duration = 50;
        }

        public int getManaCost() {
            return manaCost;
        }

        public int getExpCost() {
            return expCost;
        }

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

        public bool isCooldown() {
            return totalCooldown == 200;
        }

        public bool getDuration() {
            return duration == 200;
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
            if (duration < 200) {
                duration++;
            }
        }
    }
}
