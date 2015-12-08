using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    public class Barrier : GameObject {

        private readonly Texture2D open;
        private readonly Texture2D closed;
        private readonly SoundEffectInstance effect;

        private bool state;
        private bool defaultValue;

        public Barrier(Texture2D[] textures, Vector2 location, SoundEffectInstance effect) :
            base(textures[0], location) {
            this.open = textures[0];
            this.closed = textures[1];
            this.effect = effect;
            this.state = false;
            this.defaultValue = false;
        }

        public Barrier(Texture2D[] textures, Vector2 location, SoundEffectInstance effect, bool state) :
            this(textures, location, effect) {
            this.state = state;
            this.defaultValue = state;
        }

        public SoundEffectInstance getEffect() {
            return effect;
        }

        public void playEffect() {
            if (effect != null && effect.State != SoundState.Playing) {
                effect.Play();
            }
        }

        /// <summary>
        /// Sets the barrier's state
        /// </summary>
        /// <param name="value">The boolean to be set</param>
        public void setState(bool value) {
            if (defaultValue) {
                if (state != !value) {
                    playEffect();
                }
                state = !value;
            } else {
                if (state != value) {
                    playEffect();
                }
                state = value;
            }
        }

        /// <summary>
        /// Returns whether or not the barrier is open
        /// </summary>
        /// <returns>Returns true if the barrier is open; otherwise, false</returns>
        public bool isOpen() {
            return state;
        }

        /// <summary>
        /// Draws the barrier
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(state ? open : closed, getLocation(), Color.White);
        }
    }
}
