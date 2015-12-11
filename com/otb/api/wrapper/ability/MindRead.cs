using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents the mind reading ability
    /// </summary>

    public class MindRead : BasePower {

        public MindRead(int manaCost, int cooldown, int duration) :
            base(manaCost, cooldown, duration) {
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
            if (duration < 100) {
                duration++;
            }
        }

        /// <summary>
        /// Handles how the power operates
        /// </summary>
        /// <param name="level">The level the power is activating on</param>
        public override void activate(Level level) {
            if (activated) {
                if (duration < 100) {
                    updateDuration();
                } else {
                    setActivated(false);
                }
            }
            updateCooldown();
        }
    }
}
