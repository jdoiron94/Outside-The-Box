using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the key box
    /// </summary>

    public class KeyBox : GameObject {

        private Texture2D normBox;
        private Texture2D nullBox;
        private Texture2D key;
        private bool unlocked;
        private bool nullCheck;

        public KeyBox(Texture2D[] Textures, Vector2 Location) :
            base(Textures[0], Location) {
            this.normBox = Textures[0];
            this.nullBox = Textures[1];
            this.key = Textures[2];
        }

        /// <summary>
        /// Updates the key box
        /// </summary>
        /// <param name="inputManager">The InputManager</param>
        public void update(InputManager inputManager) {
            nullCheck = true;
            foreach (Door d in inputManager.getLevel().getDoors()) {
                if (!d.isUnlocked()) {
                    nullCheck = false;
                    unlocked = false;
                }
            }
        }

        /// <summary>
        /// Draws the key box
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            if (!nullCheck) {
                batch.Draw(normBox, getLocation(), Color.White);
                if (unlocked) {
                    batch.Draw(key, getLocation(), Color.White);
                }
            } else {
                batch.Draw(nullBox, getLocation(), Color.White);
            }
        }
    }
}
