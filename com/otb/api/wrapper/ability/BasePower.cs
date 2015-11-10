using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OutsideTheBox {

    /// <summary>
    /// Interface which holds relevant information to serve as a base for all powers
    /// </summary>

    public class BasePower {

        private int id;
        private int slotId;
        private int manaCost;
        private int expCost;
        protected int cooldown;
        protected int duration;
        protected bool unlocked;
        protected bool activated;

        private Texture2D icon;
        private SoundEffect effect;
        private Projectile projectile;

        public BasePower(int id, int slotId, int manaCost, int expCost, int cooldown, int duration, bool unlocked, bool activated, Texture2D icon) {
            this.id = id;
            this.slotId = slotId;
            this.manaCost = manaCost;
            this.expCost = expCost;
            this.cooldown = cooldown;
            this.duration = duration;
            this.unlocked = unlocked;
            this.activated = activated;
            this.icon = icon;
        }

        /// <summary>
        /// Returns the power's id
        /// </summary>
        /// <returns>Returns the power's id</returns>
        public int getId() {
            return id;
        }

        /// <summary>
        /// Returns the power's slot id
        /// </summary>
        /// <returns>Returns the power's slot id</returns>
        public int getSlotId() {
            return slotId;
        }

        /// <summary>
        /// Returns the power's mana cost
        /// </summary>
        /// <returns>Returns the power's mana cost</returns>
        public int getManaCost() {
            return manaCost;
        }

        /// <summary>
        /// Returns the power's exp cost
        /// </summary>
        /// <returns>Returns the power's exp cost</returns>
        public int getExpCost() {
            return expCost;
        }

        /// <summary>
        /// Returns the power's cooldown
        /// </summary>
        /// <returns>Returns the power's cooldown</returns>
        public int getCooldown() {
            return cooldown;
        }

        /// <summary>
        /// Returns the power's duration
        /// </summary>
        /// <returns>Returns the power's duration</returns>
        public int getDuration() {
            return duration;
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
        /// Returns the power's icon
        /// </summary>
        /// <returns>Returns the power's icon</returns>
        public Texture2D getIcon() {
            return icon;
        }

        /// <summary>
        /// Returns the power's sound effect
        /// </summary>
        /// <returns>Returns the power's sound effect</returns>
        public SoundEffect getEffect() {
            return effect;
        }

        /// <summary>
        /// Returns the power's projectile
        /// </summary>
        /// <returns>Returns the power's projectile</returns>
        public Projectile getProjectile() {
            return projectile;
        }

        /// <summary>
        /// Sets the power's unlocked status
        /// </summary>
        /// <param name="unlocked">The unlocked status to set</param>
        public void setUnlocked(bool unlocked) {
            this.unlocked = unlocked;
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
            if (isUnlocked() && !isActivated() && isCooldownMet()) {
                setActivated(true);
                playEffect();
            }
            return activated;
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
