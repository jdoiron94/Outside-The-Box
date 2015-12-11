using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles pressable buttons
    /// </summary>

    public class PressButton : GameObject {

        private Texture2D textureOn;
        private Texture2D textureOff;
        private Texture2D textureDeactivated;
        private readonly SoundEffectInstance effect;

        private bool deactivated;
        private bool pushed;

        public PressButton(Texture2D[] texture, Vector2 location, SoundEffectInstance effect, bool deactivated, bool pushed) :
            base(texture[0], location) {
            this.textureOn = texture[0];
            this.textureOff = texture[1];
            this.textureDeactivated = texture[2];
            this.effect = effect;
            this.deactivated = deactivated;
            this.pushed = pushed;
        }

        /// <summary>
        /// Sets the button's pushed status
        /// </summary>
        /// <param name="pushed">The status to set</param>
        public void setPushed(bool pushed) {
            if (this.pushed != pushed && pushed) {
                playEffect();
            }
            this.pushed = pushed;
        }

        /// <summary>
        /// Returns if the game object is pushed
        /// </summary>
        /// <returns>Returns true if the game object is pushed; otherwise, false</returns>
        public bool isPushed() {
            return pushed;
        }

        /// <summary>
        /// Sets the button's deactivated status
        /// </summary>
        /// <param name="deactivated">The status to set</param>
        public void setDeactivated(bool deactivated) {
            this.deactivated = deactivated;
        }

        /// <summary>
        /// Returns if the game object can be pushable
        /// </summary>
        /// <returns>Returns true if the game object is pushable; otherwise, false</returns>
        public bool isDeactivated() {
            return deactivated;
        }

        /// <summary>
        /// Plays the button's sound effect
        /// </summary>
        public void playEffect() {
            if (effect != null && effect.State != SoundState.Playing) {
                effect.Play();
            }
        }

        /// <summary>
        /// Updates the button
        /// </summary>
        public virtual void update() {

        }

        /// <summary>
        /// Draws Press Button
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            if (deactivated) {
                batch.Draw(textureDeactivated, getLocation(), Color.White);
            } else {
                batch.Draw(pushed ? textureOn : textureOff, getLocation(), Color.White);
            }
        }
    }
}
