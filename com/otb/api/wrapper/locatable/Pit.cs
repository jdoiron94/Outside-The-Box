using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles pits
    /// </summary>

    public class Pit : GameObject {

        private Rectangle size;
        private Rectangle bounds;

        private readonly SoundEffectInstance effect;

        public Pit(Texture2D texture, Vector2 location, SoundEffectInstance effect, int width, int height) :
            base(texture, location) {
            this.effect = effect;
            this.size = new Rectangle((int) getLocation().X, (int) getLocation().Y, width, height);
            this.bounds = new Rectangle((int) getLocation().X, (int) getLocation().Y, width, height);
            setBounds(bounds);
            setDestinationBounds(bounds);
        }

        /// <summary>
        /// Returns the pit's size
        /// </summary>
        /// <returns>Returns the pit's size</returns>
        public Rectangle getSize() {
            return size;
        }

        /// <summary>
        /// Plays the pit's sound effect
        /// </summary>
        public void playEffect() {
            if (effect != null && effect.State != SoundState.Playing) {
                effect.Play();
            }
        }

        /// <summary>
        /// Updates the pit
        /// </summary>
        /// <param name="inputManager">The InputManager</param>
        public virtual void update(InputManager inputManager) {

        }

        /// <summary>
        /// Draws the pit
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public virtual void draw(SpriteBatch batch) {
            batch.Draw(getTexture(), size, Color.White);
        }
    }
}
