namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the confuse power
    /// </summary>

    public class Confuse : BasePower {

        public Confuse(int id, int slotId, int manaCost, int expCost, int cooldown, int duration, bool unlocked, bool activated) :
            base(id, slotId, manaCost, expCost, cooldown, duration, unlocked, activated) {
        }

        /// <summary>
        /// Returns whether or not the power's cooldown timer has been met
        /// </summary>
        /// <returns>Returns true if the power's cooldown has been met; otherwise, false</returns>
        public override bool isCooldownMet() {
            return cooldown == 200;
        }

        /// <summary>
        /// Handles how the power updates its cooldown
        /// </summary>
        public override void updateCooldown() {
            if (cooldown < 200) {
                cooldown++;
            }
        }

        /// <summary>
        /// Handles how the power updates its duration
        /// </summary>
        public override void updateDuration() {
            if (duration < 200) {
                duration++;
            }
        }

        /// <summary>
        /// Handles how the power operates
        /// </summary>
        /// <param name="level">The level the power is activating on</param>
        public override void activate(Level level) {
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
                    setActivated(false);
                }
            }
            updateCooldown();
        }
    }
}
