using Microsoft.Xna.Framework;

namespace OutsideTheBox {

    public class Dash : BasePower {

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
        }

        public int getManaCost() {
            return manaCost;
        }

        public int getExpCost() {
            return expCost;
        }

        public void doStuff(Level level) {
            if (activated) {
                if (duration < 15) {
                    switch (level.getPlayer().getDirection()) {
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
                } else {
                    activatePower(false);
                }
            }
            updateCooldown();
        }

        public bool isCooldown() {
            return totalCooldown == 20;
        }

        public bool getDuration() {
            return duration == 15;
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
                if (duration < 15) {
                    updateDuration();
                } else {
                    activatePower(false);
                }
            }
            updateCooldown();

        }

        public void updateCooldown() {
            if (totalCooldown < 20) {
                totalCooldown++;
            }
        }

        public void updateDuration() {
            if (duration < 15) {
                duration++;
            }
        }
    }
}
