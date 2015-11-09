namespace OutsideTheBox {
    
    /// <summary>
    /// Class which handles the dash ability
    /// </summary>

    public class Dash : BasePower {

        public Dash(int id, int slotId, int manaCost, int expCost, int cooldown, int duration, bool unlocked, bool activated) :
            base(id, slotId, manaCost, expCost, cooldown, duration, unlocked, activated) {
        }

        /// <summary>
        /// Returns whether or not the power's cooldown timer has been met
        /// </summary>
        /// <returns>Returns true if the power's cooldown has been met; otherwise, false</returns>
        public override bool isCooldownMet() {
            return cooldown == 20;
        }

        /// <summary>
        /// Handles how the power updates its cooldown
        /// </summary>
        public override void updateCooldown() {
            if (cooldown < 20) {
                cooldown++;
            }
        }

        /// <summary>
        /// Handles how the power updates its duration
        /// </summary>
        public override void updateDuration() {
            if (duration < 15) {
                duration++;
            }
        }

        /// <summary>
        /// Handles how the power operates
        /// </summary>
        /// <param name="level">The level the power is activating on</param>
        public override void activate(Level level) {
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
                    setActivated(false);
                }
            }
            updateCooldown();
        }
    }
}
