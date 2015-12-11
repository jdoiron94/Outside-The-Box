using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles lava pits
    /// </summary>

    public class LavaPit : Pit {

        private readonly Texture2D[] frames;

        private readonly int damage;
        private int index;
        private int timer;
        private int current;
        private bool forward;

        public LavaPit(Texture2D[] frames, Vector2 location, SoundEffectInstance effect, int width, int height) :
            base(frames[0], location, effect, width, height) {
            this.frames = frames;
            this.damage = 2;
            this.timer = 5;
            this.forward = true;
        }

        /// <summary>
        /// Loops through the frames
        /// </summary>
        public void updateFrame() {
            if (current >= timer) {
                if (forward) {
                    index++;
                    if (index == frames.Length - 1) {
                        forward = false;
                    }
                } else {
                    index--;
                    if (index == 0) {
                        forward = true;
                    }
                }
                current = 0;
            } else {
                current++;
            }
        }

        /// <summary>
        /// Updates the lava pit
        /// </summary>
        /// <param name="inputManager">The InputManager</param>
        public override void update(InputManager inputManager) {
            inputManager.getPlayerManager().damagePlayer(damage);
        }

        /// <summary>
        /// Draws the lava pit in its active frame
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch) {
            batch.Draw(frames[index], getSize(), Color.White);
        }
    }
}
