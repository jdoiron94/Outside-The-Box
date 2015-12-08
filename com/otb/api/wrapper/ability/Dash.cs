using Microsoft.Xna.Framework;

namespace OutsideTheBox {
    
    /// <summary>
    /// Class which handles the dash ability
    /// </summary>

    public class Dash : BasePower {

        private CollisionManager manager;

        public Dash(int manaCost, int cooldown, int duration, bool unlocked, bool activated) :
            base(manaCost, cooldown, duration, unlocked, activated) {
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
            manager = level.getCollisionManager();
            if (activated) {
                Vector2 destination;
                if (duration < 15) {
                    switch (level.getPlayer().getDirection()) {
                        case Direction.North:
                            destination = new Vector2(level.getPlayer().getLocation().X, level.getPlayer().getLocation().Y - 6);
                            level.getPlayer().setDestination(destination);
                            if (level.getPlayer().getDestination().Y >= 0 && manager.isValid(level.getPlayer(), false)) {
                                level.getPlayer().deriveY(-6);
                            }
                            break;
                        case Direction.South:
                            destination = new Vector2(level.getPlayer().getLocation().X, level.getPlayer().getLocation().Y + 6);
                            level.getPlayer().setDestination(destination);
                            if (level.getPlayer().getDestination().Y <= 416 && manager.isValid(level.getPlayer(), false)) {
                                level.getPlayer().deriveY(6);
                            }
                            break;
                        case Direction.West:
                            destination = new Vector2(level.getPlayer().getLocation().X - 6, level.getPlayer().getLocation().Y);
                            level.getPlayer().setDestination(destination);
                            if (level.getPlayer().getDestination().X >= 0 && manager.isValid(level.getPlayer(), false)) {
                                level.getPlayer().deriveX(-6);
                            }
                            break;
                        case Direction.East:
                            destination = new Vector2(level.getPlayer().getLocation().X + 6, level.getPlayer().getLocation().Y);
                            level.getPlayer().setDestination(destination);
                            if (level.getPlayer().getDestination().X <= 736 && manager.isValid(level.getPlayer(), false)) {
                                level.getPlayer().deriveX(6);
                            }
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
