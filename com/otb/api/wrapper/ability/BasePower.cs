using Microsoft.Xna.Framework.Audio;

namespace OutsideTheBox {

    /// <summary>
    /// Interface which holds relevant information to serve as a base for all powers
    /// </summary>

    public class BasePower {

        private int manaCost;
        protected int cooldown;
        protected int duration;
        protected bool unlocked;
        protected bool activated;

        private PlayerManager manager;
        
        private SoundEffect effect;
        private Projectile projectile;

        public BasePower(int manaCost, int cooldown, int duration, bool unlocked, bool activated) {
            this.manaCost = manaCost;
            this.cooldown = cooldown;
            this.duration = duration;
            this.unlocked = unlocked;
            this.activated = activated;
        }

        /// <summary>
        /// Sets the player manager
        /// </summary>
        /// <param name="manager">The player manager to set</param>
        public void setPlayerManager(PlayerManager manager) {
            this.manager = manager;
        }

        /// <summary>
        /// Returns the power's mana cost
        /// </summary>
        /// <returns>Returns the power's mana cost</returns>
        public int getManaCost() {
            return manaCost;
        }

        /// <summary>
        /// Returns whether or not the power is unlocked
        /// </summary>
        /// <returns>Returns true if the power is unlocked; otherwise, false</returns>
        public bool isUnlocked() {
            return unlocked;
        }

        /// <summary>
        /// Returns whether or not the power is activated
        /// </summary>
        /// <returns>Returns true if the power is activated; otherwise, false</returns>
        public bool isActivated() {
            return activated;
        }

        /// <summary>
        /// Sets the power's activated status
        /// </summary>
        /// <param name="activated">The activated status to set</param>
        public void setActivated(bool activated) {
            this.activated = activated;
            if (activated) {
                duration = 0;
            } else {
                cooldown = 0;
            }
        }

        /// <summary>
        /// Sets the power's sound effect
        /// </summary>
        /// <param name="effect">The sound effect to set</param>
        public void setEffect(SoundEffect effect) {
            this.effect = effect;
        }

        /// <summary>
        /// Sets the power's projectile
        /// </summary>
        /// <param name="projectile">The projectile to set</param>
        public void setProjectile(Projectile projectile) {
            this.projectile = projectile;
        }

        /// <summary>
        /// Plays the power's sound effect
        /// </summary>
        public void playEffect() {
            if (effect != null) {
                effect.Play();
            }
        }

        /// <summary>
        /// Returns whether or not the power is eligible to activate
        /// </summary>
        /// <returns>Returns true if the power may be activated; otherwise, false</returns>
        public bool validate() {
            if (isUnlocked() && !isActivated() && isCooldownMet() && manager.getMana() >= manaCost) {
                setActivated(true);
                playEffect();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether or not the power's cooldown timer has been met
        /// </summary>
        /// <returns>Returns true if the power's cooldown has been met; otherwise, false</returns>
        public virtual bool isCooldownMet() {
            return false;
        }

        /// <summary>
        /// Handles how the power updates its cooldown
        /// </summary>
        public virtual void updateCooldown() {
        }

        /// <summary>
        /// Handles how the power updates its duration
        /// </summary>
        public virtual void updateDuration() {
        }

        /// <summary>
        /// Handles how the power operates
        /// </summary>
        /// <param name="level">The level the power is activating on</param>
        public virtual void activate(Level level) {
        }
    }
}
