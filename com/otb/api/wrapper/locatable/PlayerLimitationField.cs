using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles limiting the player's abilities within a pit
    /// </summary>

    public class PlayerLimitationField : Pit {

        private readonly Texture2D[] frames;

        private int index;
        private int timer;
        private int current;
        private bool forward;

        public PlayerLimitationField(Texture2D[] frames, Vector2 location, SoundEffectInstance effect, int width, int height) :
            base(frames[0], location, effect, width, height) {
            this.frames = frames;
            this.timer = 5;
            this.forward = true;
        }

        /// <summary>
        /// Updates the active frame
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
        /// Updates the limitation field
        /// </summary>
        /// <param name="inputManager">The InputManager</param>
        public override void update(InputManager inputManager) {
            inputManager.getPlayerManager().setHealthLimit(false);
            inputManager.getPlayerManager().setManaLimit(false);
        }

        /// <summary>
        /// Draws the limitation field's active frame
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch) {
            batch.Draw(frames[index], getSize(), Color.White);
        }
    }
}
